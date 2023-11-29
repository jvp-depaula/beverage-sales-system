using Microsoft.CodeAnalysis.FlowAnalysis;
using Sistema.Models;
using System.Data.SqlClient;

namespace Sistema.DAO
{
    public class DAOItemNFEntrada : DAO
    {
        public List<ItemNFEntrada> GetItensNFEntrada()
        {
            try
            {
                var sql = this.Search(null, null, null, null, null);
                OpenConnection();
                SqlQuery = new SqlCommand(sql, con);
                reader = SqlQuery.ExecuteReader();
                var list = new List<ItemNFEntrada>();

                while (reader.Read())
                {
                    var ItensNFEntrada = new ItemNFEntrada
                    {
                        idProduto = Convert.ToInt32(reader["idProduto"]),
                        idFornecedor = Convert.ToInt32(reader["idFornecedor"]),
                        nrModelo = Convert.ToString(reader["nrModelo"]),
                        nrSerie = Convert.ToString(reader["nrSerie"]),
                        nrNota = Convert.ToInt32(reader["nrNota"]),
                        qtdItem = Convert.ToDecimal(reader["qtdItem"]),
                        vlFrete = Convert.ToDecimal(reader["vlFrete"]),
                        vlDesconto = Convert.ToDecimal(reader["vlDesconto"]),
                        vlSeguro = Convert.ToDecimal(reader["vlSeguro"]),
                        vlOutrasDespesas = Convert.ToDecimal(reader["vlOutrasDespesas"]),
                        vlCusto = Convert.ToDecimal(reader["vlCusto"]),
                        dtCadastro = Convert.ToDateTime(reader["dtCadastro"]),
                        dtUltAlteracao = Convert.ToDateTime(reader["dtUltalteracao"])
                    };

                    list.Add(ItensNFEntrada);
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

        public void Insert(Models.ItemNFEntrada ItemNFEntrada)
        {
            try
            {
                var sql = string.Format("INSERT INTO tbItemNFEntrada (idProduto, idFornecedor, nrModelo, nrSerie, nrNota, qtdItem, " +
                    "vlFrete, vlDesconto, vlSeguro, vlOutrasDespesas, vlCusto, dtCadastro, dtUltAlteracao) " +
                    "VALUES ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}', '{11}', '{12}')",
                    Convert.ToInt32(ItemNFEntrada.idProduto),
                    Convert.ToInt32(ItemNFEntrada.idFornecedor),
                    ItemNFEntrada.nrModelo,
                    ItemNFEntrada.nrSerie,
                    ItemNFEntrada.nrNota,
                    Convert.ToDecimal(ItemNFEntrada.qtdItem),
                    Convert.ToDecimal(ItemNFEntrada.vlFrete),
                    Convert.ToDecimal(ItemNFEntrada.vlDesconto),
                    Convert.ToDecimal(ItemNFEntrada.vlSeguro),
                    Convert.ToDecimal(ItemNFEntrada.vlOutrasDespesas),
                    Convert.ToDecimal(ItemNFEntrada.vlCusto),
                    Util.Util.FormatDate(DateTime.Now),
                    Util.Util.FormatDate(DateTime.Now)
                );

                OpenConnection();
                SqlQuery = new SqlCommand(sql, con);
                SqlQuery.ExecuteNonQuery();
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

        public void Update(Models.ItemNFEntrada ItemNFEntrada)
        {
            try
            {
                var sql = String.Format("UPDATE tbItemNFEntrada SET qtdItem = '{0}', vlFrete = '{1}', vlDesconto = '{2}', " +
                    "vlSeguro = '{3}', vlOutrasDespesas = '{4}', vlCusto = '{5}', dtUltAlteracao = '{6}' WHERE idProduto = '{7}'" +
                    "AND idFornecedor = '{8}' AND nrModelo = '{9}' AND nrSerie = '{10}' AND nrNota = '{11}'",
                    ItemNFEntrada.qtdItem,
                    ItemNFEntrada.vlFrete,
                    ItemNFEntrada.vlDesconto,
                    ItemNFEntrada.vlSeguro,
                    ItemNFEntrada.vlOutrasDespesas,
                    ItemNFEntrada.vlCusto,
                    Util.Util.FormatDate(DateTime.Now),
                    ItemNFEntrada.idProduto,
                    ItemNFEntrada.idFornecedor,
                    ItemNFEntrada.nrModelo,
                    ItemNFEntrada.nrSerie,
                    ItemNFEntrada.nrNota
                    );
                OpenConnection();
                SqlQuery = new SqlCommand(sql, con);
                SqlQuery.ExecuteNonQuery();

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

        public ItemNFEntrada GetItemNFEntrada(int? idProduto, int? idFornecedor, string nrModelo, string nrSerie, string nrNota)
        {
            try
            {
                var model = new Models.ItemNFEntrada();

                if (idProduto != null && idFornecedor != null && !String.IsNullOrEmpty(nrModelo) &&
                    !String.IsNullOrEmpty(nrSerie) && !String.IsNullOrEmpty(nrNota))
                {
                    OpenConnection();
                    var sql = this.Search(idProduto, idFornecedor, nrModelo, nrSerie, nrNota);
                    SqlQuery = new SqlCommand(sql, con);
                    reader = SqlQuery.ExecuteReader();

                    while (reader.Read())
                    {
                        model.idProduto = Convert.ToInt32(reader["idCliente"]);
                        model.idFornecedor = Convert.ToInt32(reader["idFornecedor"]);
                        model.nrModelo = Convert.ToString(reader["nrModelo"]);
                        model.nrSerie = Convert.ToString(reader["nrSerie"]);
                        model.nrNota = Convert.ToInt32(reader["nrNota"]);
                        model.qtdItem = Convert.ToDecimal(reader["qtdItem"]);
                        model.vlFrete = Convert.ToDecimal(reader["vlFrete"]);
                        model.vlDesconto = Convert.ToDecimal(reader["vlDesconto"]);
                        model.vlSeguro = Convert.ToDecimal(reader["vlSeguro"]);
                        model.vlOutrasDespesas = Convert.ToDecimal(reader["vlOutrasDespesas"]);
                        model.vlCusto = Convert.ToDecimal(reader["vlCusto"]);
                        model.dtCadastro = Convert.ToDateTime(reader["dtCadastro"]);
                        model.dtUltAlteracao = Convert.ToDateTime(reader["dtUltAlteracao"]);
                    }
                }
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

        public void Delete(int? idProduto, int? idFornecedor, string nrModelo, string nrSerie, string nrNota)
        {
            try
            {
                string sql = "DELETE FROM tbItemNFEntrada WHERE idProduto = " + idProduto + " AND idFornecedor = " + idFornecedor + " AND nrModelo = " + nrModelo +
                             " AND nrSerie = " + nrSerie + " AND nrNota = " + nrNota + ";";
                OpenConnection();
                SqlQuery = new SqlCommand(sql, con);
                SqlQuery.ExecuteNonQuery();

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

        private string Search(int? idProduto, int? idFornecedor, string filter, string filter2, string filter3)
        {
            var sql = string.Empty;
            var swhere = string.Empty;
            if (idProduto != null && idFornecedor != null && !String.IsNullOrEmpty(filter) &&
                   !String.IsNullOrEmpty(filter2) && !String.IsNullOrEmpty(filter3))
            {
                swhere = " WHERE idProduto = " + idProduto + " AND idFornecedor = " + idFornecedor + " AND nrModelo = " + filter + " AND nrSerie = " + filter2 + " AND nrNota = " + filter3;
            }
            sql = @"
                    SELECT
                        tbItemNFEntrada.idProduto AS idProduto,
                        tbProdutos.nmProduto AS nmProduto,
                        tbItemNFEntrada.idFornecedor AS idFornecedor,
                        tbFornecedores.nmFornecedor AS nmFornecedor,
                        tbItemNFEntrada.nrModelo AS nrModelo,
                        tbItemNFEntrada.nrSerie AS nrSerie,
                        tbItemNFEntrada.nrNota AS nrNota,
                        tbItemNFEntrada.qtdItem AS qtdItem,
                        tbItemNFEntrada.vlFrete AS vlFrete,
                        tbItemNFEntrada.vlDesconto AS vlDesconto,
                        tbItemNFEntrada.vlSeguro AS vlSeguro,
                        tbItemNFEntrada.vlOutrasDespesas AS vlOutrasDespesas,
                        tbItemNFEntrada.vlCusto AS vlCusto,
                        tbItemNFEntrada.dtCadastro AS dtCadastro,
                        tbItemNFEntrada.dtUltAlteracao AS dtUltAlteracao
                    FROM tbItemNFEntrada
                    INNER JOIN tbProdutos ON tbItemNFEntrada.idProduto = tbProdutos.idProduto
                    INNER JOIN tbFornecedores ON tbItemNFEntrada.idFornecedor = tbFornecedores.idFornecedor " + swhere;
            return sql;
        }
    }
}
