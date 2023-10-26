using Microsoft.CodeAnalysis.FlowAnalysis;
using Sistema.Models;
using System.Data.SqlClient;

namespace Sistema.DAO
{
    public class DAOItemNFSaida : DAO
    {
        public List<ItemNFSaida> GetItensNFSaida()
        {
            try
            {
                var sql = this.Search(null, null, null, null, null);
                OpenConnection();
                SqlQuery = new SqlCommand(sql, con);
                reader = SqlQuery.ExecuteReader();
                var list = new List<ItemNFSaida>();

                while (reader.Read())
                {
                    var ItensNFSaida = new ItemNFSaida
                    {
                        idProduto = Convert.ToInt32(reader["idProduto"]),
                        idCliente = Convert.ToInt32(reader["idCliente"]),
                        nrModelo = Convert.ToString(reader["nrModelo"]),
                        nrSerie = Convert.ToString(reader["nrSerie"]),
                        nrNota = Convert.ToInt32(reader["nrNota"]),
                        qtdItem = Convert.ToDecimal(reader["qtdItem"]),
                        vlVenda = Convert.ToDecimal(reader["vlVenda"]),
                        dtCadastro = Convert.ToDateTime(reader["dtCadastro"]),
                        dtUltAlteracao = Convert.ToDateTime(reader["dtUltalteracao"])
                    };

                    list.Add(ItensNFSaida);
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

        public void Insert(Models.ItemNFSaida ItemNFSaida)
        {
            try
            {
                var sql = string.Format("INSERT INTO tbItemNFSaida (idProduto, idCliente, nrModelo, nrSerie, nrNota, qtdItem, " +
                    "vlVenda, dtCadastro, dtUltAlteracao) " +
                    "VALUES ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}')",
                    Convert.ToInt32(ItemNFSaida.idProduto),
                    Convert.ToInt32(ItemNFSaida.idCliente),
                    ItemNFSaida.nrModelo,
                    ItemNFSaida.nrSerie,
                    ItemNFSaida.nrNota,
                    Convert.ToDecimal(ItemNFSaida.qtdItem),
                    Convert.ToDecimal(ItemNFSaida.vlVenda),
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

        public void Update(Models.ItemNFSaida ItemNFSaida)
        {
            try
            {
                var sql = String.Format("UPDATE tbItemNFSaida SET qtdItem = '{0}', vlVenda = '{1}', dtUltAlteracao = '{2}'" +
                    "WHERE idProduto = '{3}', idCliente = '{4}', nrModelo = '{5}', nrSerie = '{6}', nrNota ='{7}')," +
                    Convert.ToDecimal(ItemNFSaida.qtdItem),
                    Convert.ToDecimal(ItemNFSaida.vlVenda),
                    Util.Util.FormatDate(DateTime.Now),
                    Convert.ToInt32(ItemNFSaida.idProduto),
                    Convert.ToInt32(ItemNFSaida.idCliente),
                    ItemNFSaida.nrModelo,
                    ItemNFSaida.nrSerie,
                    ItemNFSaida.nrNota);                
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

        public ItemNFSaida GetItemNFSaida(int? idProduto, int? idCliente, string nrModelo, string nrSerie, string nrNota)
        {
            try
            {
                var model = new Models.ItemNFSaida();

                if (idProduto != null && idCliente != null && !String.IsNullOrEmpty(nrModelo) && 
                    !String.IsNullOrEmpty(nrSerie) && !String.IsNullOrEmpty(nrNota))
                {
                    OpenConnection();
                    var sql = this.Search(idProduto, idCliente, nrModelo, nrSerie, nrNota);
                    SqlQuery = new SqlCommand(sql, con);
                    reader = SqlQuery.ExecuteReader();

                    while (reader.Read())
                    {
                        model.idProduto = Convert.ToInt32(reader["idCliente"]);
                        model.idCliente = Convert.ToInt32(reader["idCliente"]);
                        model.nrModelo = Convert.ToString(reader["nrModelo"]);
                        model.nrSerie = Convert.ToString(reader["nrSerie"]);
                        model.nrNota = Convert.ToInt32(reader["nrNota"]);
                        model.qtdItem = Convert.ToDecimal(reader["qtdItem"]);
                        model.vlVenda = Convert.ToDecimal(reader["vlVenda"]);                        
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

        public void Delete(int? idProduto, int? idCliente, string nrModelo, string nrSerie, string nrNota)
        {
            try
            {
                string sql = "DELETE FROM tbItemNFSaida WHERE idProduto = " + idProduto + " AND idCliente = " + idCliente + " AND nrModelo = " + nrModelo +
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

        private string Search(int? idProduto, int? idCliente, string filter, string filter2, string filter3)
        {
            var sql = string.Empty;
            var swhere = string.Empty;
            if (idProduto != null && idCliente != null && !String.IsNullOrEmpty(filter) &&
                   !String.IsNullOrEmpty(filter2) && !String.IsNullOrEmpty(filter3))
            {
                swhere = " WHERE idProduto = " + idProduto + " AND idCliente = " + idCliente + " AND nrModelo = " + filter + " AND nrSerie = " + filter2 + " AND nrNota = " + filter3;
            }
            sql = @"
                    SELECT
                        tbItemNFSaida.idProduto AS idProduto,
                        tbProdutos.nmProduto AS nmProduto,
                        tbItemNFSaida.idCliente AS idCliente,
                        tbFornecedores.nmCliente AS nmCliente,
                        tbItemNFSaida.nrModelo AS nrModelo,
                        tbItemNFSaida.nrSerie AS nrSerie,
                        tbItemNFSaida.nrNota AS nrNota,
                        tbItemNFSaida.qtdItem AS qtdItem,
                        tbItemNFSaida.vlVenda AS vlVenda,
                        tbItemNFSaida.dtCadastro AS dtCadastro,
                        tbItemNFSaida.dtUltAlteracao AS dtUltAlteracao
                    FROM tbItemNFSaida
                    INNER JOIN tbProdutos ON tbItemNFSaida.idProduto = tbProdutos.idProduto
                    INNER JOIN tbClientes ON tbItemNFSaida.idCliente = tbFornecedores.idCliente " + swhere;
            return sql;
        }
    }
}
