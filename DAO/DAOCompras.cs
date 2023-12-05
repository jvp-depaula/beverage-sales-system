using Sistema.Models;
using System.Data.SqlClient;

namespace Sistema.DAO
{
    public class DAOCompras : DAO
    {
        public List<Compras> GetCompras()
        {
            var list = new List<Compras>();
            var sql = this.Search(null, null, null, null, null);

            using (con)
            {
                OpenConnection();
                SqlTransaction sqlTrans = con.BeginTransaction();
                SqlCommand command = con.CreateCommand();
                command.Transaction = sqlTrans;
                try
                {
                    command.CommandText = sql;
                    reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        var compra = new Compras
                        {
                            flSituacao = Util.FormatFlag.Situacao(Convert.ToString(reader["flSituacao"])),
                            idFornecedor = Convert.ToInt32(reader["idFornecedor"]),
                            nmFornecedor = Convert.ToString(reader["nmFornecedor"]),
                            nrModelo = Convert.ToString(reader["nrModelo"]),
                            nrSerie = Convert.ToString(reader["nrSerie"]),
                            nrNota = Convert.ToInt32(reader["nrNota"]),
                            dtEmissao = Convert.ToDateTime(reader["dtEmissao"]),
                            dtEntrega = Convert.ToDateTime(reader["dtEntrega"]),
                            dtCadastro = Convert.ToDateTime(reader["dtCadastro"]),
                        };
                        list.Add(compra);
                    }
                    con.Close();
                    OpenConnection();
                    foreach (var item in list)
                    {
                        var listComp = new List<Parcelas>();
                        using (var details = new SqlCommand(this.SearchParcelas(item.idFornecedor, item.nrModelo, item.nrSerie, item.nrNota), con))
                        {
                            using (var detReader = details.ExecuteReader())
                            {
                                while (detReader.Read())
                                {
                                    var parcela = new Parcelas
                                    {
                                        idFormaPgto = Convert.ToInt32(detReader["idFormaPgto"]),
                                        dsFormaPgto = Convert.ToString(detReader["dsFormaPgto"]),
                                        nrParcela = Convert.ToDouble(detReader["nrParcela"]),
                                        vlParcela = Convert.ToDecimal(detReader["vlParcela"]),
                                        dtVencimento = Convert.ToDateTime(detReader["dtVencimento"]),
                                        flSituacao = Convert.ToString(detReader["flSituacao"])
                                    };
                                    listComp.Add(parcela);
                                }
                            }
                        }
                        item.ParcelasCompra = listComp;
                    }
                    con.Close();
                }
                catch (Exception ex)
                {
                    sqlTrans.Rollback();
                    throw new Exception(ex.Message);
                }
            }
            return list;
        }

        public void Insert(Compras compra)
        {
            try
            {
                var sql = string.Format("INSERT INTO tbCompras ( idFornecedor, nrModelo, nrSerie, nrNota, dtEmissao, dtEntrega, chaveNFe, observacao, dtCadastro, flSituacao, idCondicaoPgto, vlFrete, vlSeguro, vlDespesas ) VALUES ({0}, '{1}', '{2}', {3}, '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', {10}, {11}, {12}, {13} ); ",
                    compra.idFornecedor,
                    compra.nrModelo,
                    compra.nrSerie,
                    compra.nrNota,
                    compra.dtEmissao != null ? Convert.ToDateTime(compra.dtEmissao) : null,
                    compra.dtEntrega != null ? Convert.ToDateTime(compra.dtEntrega) : null,
                    compra.chaveNFe,
                    compra.observacao != null ? compra.observacao.Trim() : null,
                    DateTime.Now,
                    "N",
                    compra.idCondicaoPgto,
                    compra.vlFrete != null ? this.FormatDecimal(compra.vlFrete).ToString() : null,
                    compra.vlSeguro != null ? this.FormatDecimal(compra.vlSeguro).ToString() : null,
                    compra.vlDespesas != null ? this.FormatDecimal(compra.vlDespesas).ToString() : null
                    );
                string sqlProduto = "INSERT INTO tbProdutosCompra (idProduto, nrModelo, nrSerie, nrNota, idFornecedor, qtdProduto, vlCompra, txDesconto, vlVenda, idUnidade) VALUES ( {0}, '{1}', '{2}', {3}, {4}, {5}, {6}, {7}, {8}, {9})";
                string sqlParcela = "INSERT INTO tbContasPagar (idFornecedor, nrModelo, nrSerie, nrNota, nrParcela, dtEmissao, dtVencimento, vlParcela, idFormaPgto, vlPago, dtPgto, flSituacao, txJuros, txMulta, txDesconto) VALUES ({0}, '{1}', '{2}', {3}, {4}, '{5}', '{6}', {7}, {8}, {9}, '{10}', '{11}', {12}, {13}, {14})";
                string sqlUpdateProduto = "UPDATE tbProdutos set qtdEstoque += {0}, vlUltCompra = {1}, dtUltAlteracao = '{2}' WHERE idProduto = {3}";
                using (con)
                {
                    OpenConnection();

                    SqlTransaction sqlTrans = con.BeginTransaction();
                    SqlCommand command = con.CreateCommand();
                    command.Transaction = sqlTrans;
                    try
                    {
                        command.CommandText = sql;
                        command.ExecuteNonQuery();
                        foreach (var item in compra.ProdutosCompra)
                        {
                            var Item = string.Format(sqlProduto, item.idProduto, compra.nrModelo, compra.nrSerie, compra.nrNota, compra.idFornecedor, this.FormatDecimal(item.qtdProduto), this.FormatDecimal(item.vlCompra), this.FormatDecimal(item.txDesconto), this.FormatDecimal(item.vlVenda), Convert.ToInt32(item.idUnidade));
                            command.CommandText = Item;
                            command.ExecuteNonQuery();

                            var upProd = string.Format(sqlUpdateProduto, this.FormatDecimal(item.qtdProduto), this.FormatDecimal(item.vlCompra), DateTime.Now, item.idProduto);
                            command.CommandText = upProd;
                            command.ExecuteNonQuery();
                        }

                        foreach (var item in compra.ParcelasCompra)
                        {
                            var parcela = string.Format(sqlParcela, compra.idFornecedor, compra.nrModelo, compra.nrSerie, compra.nrNota, item.nrParcela, compra.dtEmissao, item.dtVencimento, this.FormatDecimal(item.vlParcela), item.idFormaPgto, Convert.ToDecimal(0), item.dtPagamento, "P", compra.CondicaoPagamento_txJuros, compra.CondicaoPagamento_txMulta, compra.CondicaoPagamento_txDesconto);
                            command.CommandText = parcela;
                            command.ExecuteNonQuery();
                        }
                        sqlTrans.Commit();
                    }
                    catch (Exception ex)
                    {
                        sqlTrans.Rollback();
                        throw new Exception(ex.Message);
                    }
                    finally
                    {
                        con.Close();
                    }
                }
            }
            catch (Exception error)
            {
                throw new Exception(error.Message);
            }
            finally
            {
                CloseConnection();
            }
        }

        public void CancelarCompra(int idFornecedor, string nrModelo, string nrSerie, int nrNota)
        {
            throw new Exception("Não implementado!");
            /*try
            {
                var sqlContasPagar = "DELETE FROM tbContasPagar WHERE idFornecedor = " + idFornecedor + " nrModelo = " + nrModelo + " nrSerie = " + nrSerie + " nrNota = " + nrNota + ";";
                var sqlProdutosCompra = "Delete FROM tbProdutosCompra WHERE  idFornecedor = " + idFornecedor + " nrModelo = " + nrModelo + " nrSerie = " + nrSerie + " nrNota = " + nrNota + ";";
                var sqlCompra = "DELETE FROM tbCompras WHERE idFornecedor = " + idFornecedor + " nrModelo = " + nrModelo + " nrSerie = " + nrSerie + " nrNota = " + nrNota + ";";
                var sqlProduto = "UPDATE tbProdutos SET "
            }
            catch (Exception error)
            {
                throw new Exception(error.Message);
            }
            finally
            {
                CloseConnection();
            }*/
        }

        public Compras GetCompra(string filter, int idFornecedor, string nrModelo, string nrSerie, int nrNota)
        {
            try
            {
                var model = new Models.Compras();
                OpenConnection();
                var sql = this.Search(filter, idFornecedor, nrModelo, nrSerie, nrNota);
                var sqlProdutos = this.SearchProdutos(idFornecedor, nrModelo, nrSerie, nrNota);
                var sqlParcelas = this.SearchParcelas(idFornecedor, nrModelo, nrSerie, nrNota);
                var listProdutos = new List<Compras.ProdutosVM>();
                var listParcelas = new List<Parcelas>();

                SqlQuery = new SqlCommand(sql + sqlProdutos + sqlParcelas, con);
                reader = SqlQuery.ExecuteReader();
                while (reader.Read())
                {
                    model.flSituacao = Sistema.Util.FormatFlag.Situacao(Convert.ToString(reader["flSituacao"]));
                    model.idFornecedor = Convert.ToInt32(reader["idFornecedor"]);
                    model.nmFornecedor = Convert.ToString(reader["nmFornecedor"]);
                    model.nrModelo = Convert.ToString(reader["nrModelo"]);
                    model.nrSerie = Convert.ToString(reader["nrSerie"]);
                    model.nrNota = Convert.ToInt32(reader["nrNota"]);
                    model.dtEmissao = Convert.ToDateTime(reader["dtEmissao"]);
                    model.dtEntrega = Convert.ToDateTime(reader["dtEntrega"]);
                    model.dtCadastro = Convert.ToDateTime(reader["dtCadastro"]);
                    model.chaveNFe = Convert.ToString(reader["chaveNFe"]);
                    model.observacao = Convert.ToString(reader["observacao"]);
                    model.vlFrete = !string.IsNullOrEmpty(reader["vlFrete"].ToString()) ? Convert.ToDecimal(reader["vlFrete"]) : (decimal?)null;
                    model.vlSeguro = !string.IsNullOrEmpty(reader["vlSeguro"].ToString()) ? Convert.ToDecimal(reader["vlSeguro"]) : (decimal?)null;
                    model.vlDespesas = !string.IsNullOrEmpty(reader["vlDespesas"].ToString()) ? Convert.ToDecimal(reader["vlDespesas"]) : (decimal?)null;
                    model.idCondicaoPgto = Convert.ToInt32(reader["idCondicaoPgto"]);
                    model.dsCondicaoPgto = Convert.ToString(reader["dsCondicaoPgto"]);
                };

                if (reader.NextResult())
                {
                    while (reader.Read())
                    {
                        var produto = new Compras.ProdutosVM
                        {
                            idProduto = Convert.ToInt32(reader["idProduto"]),
                            dsProduto = Convert.ToString(reader["dsProduto"]),
                            idUnidade = Convert.ToInt32(reader["idUnidade"]),
                            dsUnidade = Convert.ToString(reader["dsUnidade"]),
                            qtdProduto = Convert.ToDecimal(reader["qtdProduto"]),
                            vlCompra = Convert.ToDecimal(reader["vlCompra"]),
                            txDesconto = Convert.ToDecimal(reader["txDesconto"]),
                            vlVenda = Convert.ToDecimal(reader["vlVenda"]),
                        };
                        var txDesc = (produto.vlCompra * produto.txDesconto) / 100;
                        var vlTotal = (produto.vlCompra - txDesc) * produto.qtdProduto;
                        produto.vlTotal = vlTotal;
                        listProdutos.Add(produto);
                    }
                }

                if (reader.NextResult())
                {
                    while (reader.Read())
                    {
                        var parcela = new Parcelas
                        {
                            idFormaPgto = Convert.ToInt32(reader["idFormaPgto"]),
                            dsFormaPgto = Convert.ToString(reader["dsFormaPgto"]),
                            nrParcela = Convert.ToDouble(reader["nrParcela"]),
                            vlParcela = Convert.ToDecimal(reader["vlParcela"]),
                            dtVencimento = Convert.ToDateTime(reader["dtVencimento"]),
                            flSituacao = Util.FormatFlag.Situacao(Convert.ToString(reader["flSituacao"]))
                        };
                        listParcelas.Add(parcela);
                    }
                }
                model.ProdutosCompra = listProdutos;
                model.ParcelasCompra = listParcelas;
                return model;
            }
            catch (Exception error)
            {
                throw new Exception(error.Message);
            }
            finally
            {
                CloseConnection();
            }
        }

        private string Search(string filter, int? idFornecedor, string nrModelo, string nrSerie, int? nrNota)
        {
            var sql = string.Empty;
            var swhere = string.Empty;
            if (!string.IsNullOrEmpty(nrModelo))
            {
                swhere += " AND tbCompras.nrModelo = '" + nrModelo + "'";
            }
            if (!string.IsNullOrEmpty(nrSerie))
            {
                swhere += " AND tbCompras.nrSerie = '" + nrSerie + "'";
            }
            if (nrNota != null)
            {
                swhere += " AND tbCompras.nrNota = " + nrNota;
            }
            if (idFornecedor != null)
            {
                swhere += " AND tbCompras.idFornecedor = " + idFornecedor;
            }

            if (!string.IsNullOrEmpty(filter))
            {
                var filterQ = filter.Split(' ');
                foreach (var word in filterQ)
                {
                    swhere += " OR tbFornecedores.nmFornecedor LIKE '%" + word + "%'";
                }
            }

            if (!string.IsNullOrEmpty(swhere))
                swhere = " WHERE " + swhere.Remove(0, 4);
            sql = @"
                    SELECT
	                    tbCompras.flSituacao AS flSituacao,
	                    tbCompras.nrModelo AS nrModelo,
	                    tbCompras.nrSerie AS nrSerie,
	                    tbCompras.nrNota AS nrNota,
	                    tbCompras.dtEmissao AS dtEmissao,
	                    tbCompras.dtEntrega AS dtEntrega,
                        tbCompras.chaveNFe AS chaveNFe,
	                    tbCompras.observacao AS observacao,
	                    tbCompras.dtCadastro AS dtCadastro,
	                    tbCompras.vlFrete AS vlFrete,
	                    tbCompras.vlSeguro AS vlSeguro,
	                    tbCompras.vlDespesas AS vlDespesas,
	                    tbCompras.idFornecedor AS idFornecedor,
	                    tbFornecedores.nmFornecedor AS nmFornecedor,
	                    tbCompras.idCondicaoPgto AS idCondicaoPgto,
	                    tbCondicaoPgto.dsCondicaoPgto AS dsCondicaoPgto
                    FROM tbCompras 
                    INNER JOIN tbFornecedores on tbCompras.idFornecedor = tbFornecedores.idFornecedor
                    INNER JOIN tbCondicaoPgto on tbCompras.idCondicaoPgto = tbCondicaoPgto.idCondicaoPgto
                " + swhere + ";";
            return sql;
        }

        private string SearchProdutos(int idFornecedor, string nrModelo, string nrSerie, int nrNota)
        {
            var sql = string.Empty;

            sql = @"
                    SELECT
	                    tbProdutosCompra.nrModelo AS nrModelo,
	                    tbProdutosCompra.nrSerie AS nrSerie,
	                    tbProdutosCompra.nrNota AS nrNota,
	                    tbProdutosCompra.idFornecedor AS idFornecedor,
	                    tbProdutosCompra.idProduto AS idProduto,
	                    tbProdutos.dsProduto AS dsProduto,
	                    tbProdutosCompra.idUnidade AS idUnidade,
                        tbUnidades.dsUnidade AS dsUnidade,
	                    tbProdutosCompra.qtdProduto AS qtdProduto,
	                    tbProdutosCompra.vlCompra AS vlCompra,
	                    tbProdutosCompra.txDesconto AS txDesconto,
	                    tbProdutosCompra.vlVenda AS vlVenda
                    FROM tbProdutosCompra
                    INNER JOIN tbProdutos on tbProdutosCompra.idProduto = tbProdutos.idProduto
                    INNER JOIN tbUnidades on tbProdutosCompra.idUnidade = tbUnidades.idUnidade
                    WHERE tbProdutosCompra.nrModelo = '" + nrModelo + "' AND tbProdutosCompra.nrSerie = '" + nrSerie + "' AND tbProdutosCompra.nrNota = " + nrNota + " AND tbProdutosCompra.idFornecedor = " + idFornecedor + ";"
            ;
            return sql;
        }

        private string SearchParcelas(int idFornecedor, string nrModelo, string nrSerie, int nrNota)
        {
            var sql = string.Empty;
            sql = @"
                    SELECT
                        tbContasPagar.idFornecedor AS idFornecedor,
	                    tbContasPagar.nrModelo AS nrModelo,
	                    tbContasPagar.nrSerie AS nrSerie,
	                    tbContasPagar.nrNota AS nrNota,	                    
	                    tbContasPagar.idFormaPgto AS idFormaPgto,
	                    tbFormaPgto.dsFormaPgto AS dsFormaPgto,
	                    tbContasPagar.nrParcela AS nrParcela,
	                    tbContasPagar.vlParcela AS vlParcela,
	                    tbContasPagar.dtVencimento AS dtVencimento,
                        tbContasPagar.flSituacao AS flSituacao
                    FROM tbContasPagar
                    INNER JOIN tbFormaPgto on tbContasPagar.idFormaPgto = tbFormaPgto.idFormaPgto
                    WHERE tbContasPagar.nrModelo = '" + nrModelo + "' AND tbContasPagar.nrSerie = '" + nrSerie + "' AND tbContasPagar.nrNota = " + nrNota + " AND tbContasPagar.idFornecedor = " + idFornecedor;
            return sql;
        }

        public bool validNota(int idFornecedor, string nrModelo, string nrSerie, int nrNota)
        {
            string sql = "SELECT * FROM tbCompras WHERE nrModelo = '" + nrModelo + "' AND nrSerie = '" + nrSerie + "' AND nrNota = " + nrNota + " AND idFornecedor = " + idFornecedor;
            OpenConnection();
            SqlQuery = new SqlCommand(sql, con);
            var exist = SqlQuery.ExecuteScalar();
            CloseConnection();
            if (exist == null)
                return true;
            else
                return false;
        }
    }
}
