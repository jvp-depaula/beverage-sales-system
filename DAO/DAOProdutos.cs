using Sistema.Models;
using System.Data.SqlClient;

namespace Sistema.DAO
{
    public class DAOProdutos : DAO
    {
        public List<Produtos> GetProdutos()
        {
            try
            {
                var sql = this.Search(null, null);
                OpenConnection();
                SqlQuery = new SqlCommand(sql, con);
                reader = SqlQuery.ExecuteReader();
                var list = new List<Produtos>();

                while (reader.Read())
                {
                    var produto = new Produtos
                    {
                        idProduto = Convert.ToInt32(reader["idProduto"]),
                        dsProduto = Convert.ToString(reader["dsProduto"]),
                        idFornecedor = Convert.ToInt32(reader["idFornecedor"]),
                        idCategoria = Convert.ToInt32(reader["idCategoria"]),
                        idUnidade = Convert.ToInt32(reader["idUnidade"]),
                        idMarca = Convert.ToInt32(reader["idMarca"]),
                        cdNCM = Convert.ToString(reader["cdNCM"]),
                        vlVenda = Convert.ToDecimal(reader["vlVenda"]),
                        observacao = Convert.ToString(reader["observacao"]),
                        dtCadastro = Convert.ToDateTime(reader["dtCadastro"]),
                        dtUltAlteracao = Convert.ToDateTime(reader["dtUltAlteracao"])
                    };

                    list.Add(produto);
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

        public void Insert(Models.Produtos produto)
        {
            try
            {
                var sql = string.Format("INSERT INTO tbProdutos (dsProduto, idFornecedor, idCategoria, idUnidade, idMarca, cdNCM, vlVenda, observacao, dtCadastro, dtUltAlteracao) VALUES ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}')",
                                        produto.dsProduto,
                                        produto.idFornecedor,
                                        Convert.ToInt32(produto.idCategoria),
                                        Convert.ToInt32(produto.idUnidade),
                                        Convert.ToInt32(produto.idMarca),
                                        produto.cdNCM,
                                        Convert.ToDecimal(produto.vlVenda),
                                        produto.observacao,
                                        Util.Util.FormatDate(DateTime.Now),
                                        Util.Util.FormatDate(DateTime.Now));
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

        public void Update(Models.Produtos produto)
        {
            try
            {
                var sql = string.Format("UPDATE tbProdutos SET dsProduto = '{0}', idFornecedor = '{1}', idCategoria = '{2}', " +
                    "idUnidade = '{3}', idMarca = '{4}', cdNCM = '{5}', vlVenda = '{6}', observacao = '{7}', dtUltAlteracao = '{8}' WHERE idProduto = '{9}'",
                     produto.dsProduto,
                     produto.idFornecedor,
                     Convert.ToInt32(produto.idCategoria),
                     Convert.ToInt32(produto.idUnidade),
                     Convert.ToInt32(produto.idMarca),
                     produto.cdNCM,
                     Convert.ToDecimal(produto.vlVenda),
                     produto.observacao,
                     Util.Util.FormatDate(DateTime.Now),
                     produto.idProduto);
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

        public Produtos GetProduto(int? idProduto)
        {
            try
            {
                var model = new Models.Produtos();
                if (idProduto != null)
                {
                    OpenConnection();
                    var sql = this.Search(idProduto, null);
                    SqlQuery = new SqlCommand(sql, con);
                    reader = SqlQuery.ExecuteReader();
                    while (reader.Read())
                    {
                        model.idProduto = Convert.ToInt32(reader["idProduto"]);
                        model.dsProduto = Convert.ToString(reader["dsProduto"]);
                        model.idFornecedor = Convert.ToInt32(reader["idFornecedor"]);
                        model.idCategoria = Convert.ToInt32(reader["idCategoria"]);
                        model.idUnidade = Convert.ToInt32(reader["idUnidade"]);
                        model.idMarca = Convert.ToInt32(reader["idMarca"]);
                        model.cdNCM = Convert.ToString(reader["cdNCM"]);
                        model.vlVenda = Convert.ToDecimal(reader["vlVenda"]);
                        model.observacao = Convert.ToString(reader["observacao"]);
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

        public void Delete(int? idProduto)
        {
            try
            {
                string sql = "DELETE FROM tbProdutos WHERE idProduto = " + idProduto;
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

        private string Search(int? id, string filter)
        {
            var sql = string.Empty;
            var swhere = string.Empty;
            if (id != null)
            {
                swhere = " WHERE idProduto = " + id;
            }
            if (!string.IsNullOrEmpty(filter))
            {
                var filterQ = filter.Split(' ');
                foreach (var word in filterQ)
                {
                    swhere += " OR tbProdutos.dsProduto LIKE'%" + word + "%'";
                }
                swhere = " WHERE " + swhere.Remove(0, 3);
            }
            sql = @"
                    SELECT
                        tbProdutos.idProduto AS idProduto,
                        tbProdutos.dsProduto AS dsProduto,
                        tbProdutos.idFornecedor AS idFornecedor,
                        tbFornecedores.nmFornecedor AS nmFornecedor,
                        tbProdutos.idCategoria AS idCategoria,
                        tbCategorias.nmCategoria AS nmCategoria,
                        tbProdutos.idUnidade AS idUnidade,
                        tbUnidades.dsUnidade AS dsUnidade,
                        tbProdutos.idMarca AS idMarca,
                        tbMarcas.nmMarca AS nmMarca,
                        tbProdutos.cdNCM AS cdNCM,
                        tbProdutos.vlVenda AS vlVenda,
                        tbProdutos.observacao AS observacao,
                        tbProdutos.dtCadastro AS dtCadastro,
                        tbProdutos.dtUltAlteracao AS dtUltAlteracao
                        FROM tbProdutos 
                        JOIN tbFornecedores ON tbProdutos.idFornecedor = tbFornecedores.idFornecedor
                        JOIN tbCategorias ON tbProdutos.idCategoria = tbCategorias.idCategoria
                        JOIN tbUnidades ON tbProdutos.idUnidade = tbUnidades.idUnidade
                        JOIN tbMarcas ON tbProdutos.idMarca = tbMarcas.idMarca" + swhere;
            return sql;
        }
    }
}
