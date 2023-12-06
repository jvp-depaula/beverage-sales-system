using Microsoft.CodeAnalysis.FlowAnalysis;
using Sistema.Models;
using System.Data.SqlClient;

namespace Sistema.DAO
{
    public class DAOVendas : DAO
    {
        public List<Vendas> GetVendas(string nrModelo)
        {
            try
            {
                var sql = this.Search(null, null, nrModelo);
                OpenConnection();
                SqlQuery = new SqlCommand(sql, con);
                reader = SqlQuery.ExecuteReader();
                var list = new List<Vendas>();

                while (reader.Read())
                {
                    var Venda = new Vendas
                    {
                        idVenda = Convert.ToInt32(reader["idVenda"]),
                        flSituacao = Util.FormatFlag.Situacao(Convert.ToString(reader["flSituacao"])),
                        nrModelo = Convert.ToString(reader["nrModelo"]),
                        dtVenda = Convert.ToDateTime(reader["dtVenda"]),
                        idFuncionario = Convert.ToInt32(reader["idFuncionario"]),
                        idCliente = Convert.ToInt32(reader["idCliente"]),
                        nmCliente = Convert.ToString(reader["nmCliente"]),
                        idCondicaoPgto = Convert.ToInt32(reader["idCondicaoPgto"])
                    };
                    list.Add(Venda);
                }
                return list;
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

        public void Insert(Vendas venda)
        {
            try
            {
                var sql = string.Format("INSERT INTO tbVendas (flSituacao, dtVenda, idFuncionario, idCliente, idCondicaoPgto, nrModelo) VALUES ( '{0}', '{1}', {2}, {3}, {4}, '{5}'); SELECT SCOPE_IDENTITY()",
                    "N",
                    DateTime.Now,
                    venda.idFuncionario,
                    venda.idCliente,
                    venda.idCondicaoPgto,
                    "55"
                    );
                string sqlProduto = "INSERT INTO tbProdutosVenda (idVenda, idProduto, idUnidade, qtdProduto, vlProduto, txDesconto) VALUES ({0}, {1}, {2}, {3}, {4}, {5})";
                string sqlParcela = "INSERT INTO tbContasReceber (idVenda, idFormaPgto, nrParcela, vlParcela, dtVencimento, flSituacao, idCliente, txJuros, txMulta, txDesconto) VALUES ({0}, {1}, {2}, {3}, '{4}', '{5}', {6}, {7}, {8}, {9} )";
                string sqlProdutoEstoque = "UPDATE tbProdutos set qtdEstoque -= {0} WHERE tbProdutos.idProduto = {1}";
                using (con)
                {
                    OpenConnection();

                    SqlTransaction sqlTrans = con.BeginTransaction();
                    SqlCommand command = con.CreateCommand();
                    command.Transaction = sqlTrans;
                    try
                    {
                        command.CommandText = sql;
                        var idVenda = Convert.ToInt32(command.ExecuteScalar());

                        foreach (var item in venda.ProdutosVenda)
                        {
                            var produto = string.Format(sqlProduto, idVenda, item.idProduto, item.idUnidade, this.FormatDecimal(item.qtdProduto), this.FormatDecimal(item.vlVenda), this.FormatDecimal(item.txDesconto));
                            command.CommandText = produto;
                            command.ExecuteNonQuery();

                            //VERIFICAR SE EXISTE A QUANTIDADE DO ITEM EM ESTOQUE ANTES DE VENDER

                            var prodEstoque = string.Format(sqlProdutoEstoque, this.FormatDecimal(item.qtdProduto), item.idProduto);
                            command.CommandText = prodEstoque;
                            command.ExecuteNonQuery();
                        }
                        foreach (var item in venda.ParcelasVenda)
                        {
                            var parcela = string.Format(sqlParcela, idVenda, item.idFormaPgto, item.nrParcela, this.FormatDecimal(item.vlParcela), item.dtVencimento, "P", venda.idCliente, this.FormatDecimal(venda.txJuros), this.FormatDecimal(venda.txMulta), this.FormatDecimal(venda.txDesconto));
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

        public void CancelarVenda(int? idVenda)
        {
            throw new Exception("Não implementado");
        }

        public Vendas GetVenda(int idVenda, string nrModelo)
        {
            try
            {
                var model = new Models.Vendas();
                OpenConnection();
                var sql = this.Search(idVenda, null, nrModelo);
                var sqlProdutos = this.SearchProdutos(idVenda);
                var sqlParcelas = this.SearchParcelas(idVenda);
                var listProdutos = new List<Vendas.ProdutosVM>();
                var listParcelas = new List<Parcelas>();

                reader = SqlQuery.ExecuteReader();
                while (reader.Read())
                {
                    model.idVenda = Convert.ToInt32(reader["idVenda"]);
                    model.flSituacao = Convert.ToString(reader["flSituacao"]);
                    model.dtVenda = Convert.ToDateTime(reader["dtVenda"]);
                    model.idCondicaoPgto = Convert.ToInt32(reader["idCondicaoPgto"]);
                    model.dsCondicaoPgto = Convert.ToString(reader["dsCondicaoPgto"]);
                    model.idCliente = Convert.ToInt32(reader["idCliente"]);
                    model.nmCliente = Convert.ToString(reader["nmCliente"]);
                    model.idFuncionario = Convert.ToInt32(reader["idFuncionario"]);
                    model.nmFuncionario = Convert.ToString(reader["nmFuncionario"]);
                    model.nrModelo = Convert.ToString(reader["nrModelo"]);
                };
                
                if (reader.NextResult())
                {
                    while (reader.Read())
                    {
                        var produto = new Vendas.ProdutosVM
                        {
                            idProduto = Convert.ToInt32(reader["idProduto"]),
                            dsProduto = Convert.ToString(reader["dsProduto"]),
                            idUnidade = Convert.ToInt32(reader["idUnidade"]),
                            dsUnidade = Convert.ToString(reader["dsUnidade"]),
                            qtdProduto = Convert.ToDecimal(reader["qtdProduto"]),
                            vlVenda = Convert.ToDecimal(reader["vlVenda"]),
                            txDesconto = Convert.ToDecimal(reader["txDesconto"])
                        };
                        if (produto.txDesconto != null && produto.txDesconto != 0)
                        {
                            decimal txDesc;
                            txDesc = (produto.vlVenda * produto.txDesconto.GetValueOrDefault()) / 100;
                            produto.vlVenda = produto.vlVenda - txDesc;
                        }
                        produto.vlTotal = produto.vlVenda * produto.qtdProduto;
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
                        };
                        listParcelas.Add(parcela);
                    }
                }
                model.ProdutosVenda = listProdutos;
                model.ParcelasVenda = listParcelas;
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

        private string Search(int? idVenda, string filter, string nrModelo)
        {
            var sql = string.Empty;
            var swhere = string.Empty;
            if (idVenda != null)
            {
                swhere = " AND (tbVendas.idVenda = " + idVenda + ") ";
            }
            if (!string.IsNullOrEmpty(nrModelo))
            {
                swhere += " AND (tbVendas.nrModelo = '" + nrModelo + "') ";
            }
            if (!string.IsNullOrEmpty(filter))
            {
                var filterQ = filter.Split(' ');
                foreach (var word in filterQ)
                {
                    swhere += " OR tbClientes.nmCliente LIKE'%" + word + "%'";
                }
            }
            if (!string.IsNullOrEmpty(swhere))
                swhere = " WHERE " + swhere.Remove(0, 4);
            sql = @"
                    SELECT
	                    tbVendas.idVenda AS idVenda,
	                    tbVendas.flSituacao AS flSituacao,
	                    tbVendas.dtVenda AS dtVenda,
	                    tbVendas.idFuncionario AS idFuncionario,
	                    tbFuncionarios.nmFuncionario AS nmFuncionario,
	                    tbVendas.idCliente AS idCliente,
	                    tbClientes.nmCliente AS nmCliente,
	                    tbVendas.idCondicaoPgto AS idCondicaoPgto,
	                    tbCondicaoPgto.dsCondicaoPgto AS dsCondicaoPgto,
                        tbVendas.nrModelo AS nrModelo
                    FROM tbvendas
                    INNER JOIN tbFuncionarios ON tbVendas.idFuncionario = tbFuncionarios.idFuncionario
                    INNER JOIN tbClientes ON tbVendas.idCliente = tbClientes.idCliente
                    INNER JOIN tbCondicaoPgto ON tbVendas.idCondicaoPgto = tbCondicaoPgto.idCondicaoPgto
            " + swhere + ";";
            return sql;
        }

        private string SearchProdutos(int? idVenda)
        {
            var sql = string.Empty;

            sql = @"
                    SELECT
	                    tbProdutosVenda.idVenda AS idVenda,
	                    tbProdutosVenda.idProduto AS idProduto,
	                    tbProdutos.dsProduto AS dsProduto,
	                    tbProdutosVenda.idUnidade AS idUnidade,
                        tbUnidades.dsUnidade AS dsUnidade,
	                    tbProdutosVenda.qtdProduto AS qtdProduto,
	                    tbProdutosVenda.vlProduto AS vlProduto,
	                    tbProdutosVenda.txDesconto AS txDesconto
                    FROM tbProdutosVenda
                    INNER JOIN tbProdutos ON tbProdutosVenda.idProduto = tbProdutos.idProduto
                    INNER JOIN tbUnidades ON tbProdutosVenda.idUnidade = tbUnidades.idUnidade
                    WHERE tbProdutosVenda.idVenda = " + idVenda + ";"
            ;
            return sql;
        }

        private string SearchParcelas(int? idVenda)
        {
            var sql = string.Empty;

            sql = @"
                    SELECT
	                    tbContasReceber.idContaReceber AS idContaReceber,
	                    tbContasReceber.nrParcela AS nrParcela,
	                    tbContasReceber.vlParcela AS vlParcela,
	                    tbContasReceber.dtVencimento AS dtVencimento,
	                    tbContasReceber.flSituacao AS flSituacao,
	                    tbContasReceber.dtPagamento AS dtPagamento,
	                    tbContasReceber.idFormaPgto AS idFormaPgto,
	                    tbFormaPgto.dsFormaPgto AS dsFormaPgto,
	                    tbContasReceber.idVenda AS idVenda
                    FROM tbContasReceber
                    INNER JOIN tbformapagamento ON tbContasReceber.idFormaPgto = tbFormaPgto.idFormaPgto
                    WHERE tbContasReceber.idVenda = " + idVenda
            ;
            return sql;
        }
    }
}
