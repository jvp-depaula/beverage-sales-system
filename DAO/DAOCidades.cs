using Sistema.Models;
using System.Data.SqlClient;

namespace Sistema.DAO
{
    public class DAOCidades : DAO
    {

        public List<Cidades> GetCidades()
        {
            try
            {
                var sql = this.Search(null, null);
                OpenConnection();
                SqlQuery = new SqlCommand(sql, con);
                reader = SqlQuery.ExecuteReader();
                var list = new List<Cidades>();

                while (reader.Read())
                {
                    var cidade = new Cidades
                    {
                        idCidade = Convert.ToInt32(reader["idCidade"]),
                        nmCidade = Convert.ToString(reader["nmCidade"]),
                        DDD = Convert.ToString(reader["DDD"]),
                        idEstado = Convert.ToInt32(reader["idEstado"]),
                        nmEstado = Convert.ToString(reader["nmEstado"]),     
                        nmPais = Convert.ToString(reader["nmPais"]),
                        dtCadastro = Convert.ToDateTime(reader["dtCadastro"]),
                        dtUltAlteracao = Convert.ToDateTime(reader["dtUltAlteracao"])
                    };

                    list.Add(cidade);
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

        public void Insert(Cidades cidade)
        {
            try
            {
                var sql = string.Format("INSERT INTO tbCidades ( nmCidade, DDD, idEstado, dtCadastro, dtUltAlteracao) VALUES ('{0}', '{1}', '{2}', {3}, '{4}')",
                    cidade.nmCidade,
                    cidade.DDD,
                    cidade.idEstado,
                    DateTime.Now.ToString("dd/MM/yyyy"),
                    DateTime.Now.ToString("dd/MM/yyyy")
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

        public void Update(Cidades cidade)
        {
            try
            {
                string sql = "UPDATE tbCidades SET nmCidade = '" + cidade.nmCidade + "'," +
                    " DDD = '" + cidade.DDD + "'," +
                    " dtUltAlteracao = '" + DateTime.Now.ToString("dd/MM/yyyy") + "'," +
                    " idEstado = " + cidade.idEstado +
                    " WHERE idCidade = " + cidade.idCidade;
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

        public Cidades GetCidade(int? idCidade, string nmCidade)
        {
            try
            {
                var model = new Cidades();
                if (idCidade != null || nmCidade != null)
                {
                    OpenConnection();
                    var sql = this.Search(idCidade, nmCidade);
                    SqlQuery = new SqlCommand(sql, con);
                    reader = SqlQuery.ExecuteReader();
                    while (reader.Read())
                    {
                        model.idCidade = Convert.ToInt32(reader["idCidade"]);
                        model.nmCidade = Convert.ToString(reader["nmCidade"]);
                        model.DDD = Convert.ToString(reader["DDD"]);
                        model.dtCadastro = Convert.ToDateTime(reader["dtCadastro"]);
                        model.dtUltAlteracao = Convert.ToDateTime(reader["dtUltAlteracao"]);
                        model.idEstado = Convert.ToInt32(reader["idEstado"]);
                        model.nmEstado = Convert.ToString(reader["nmEstado"]);
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

        public void Delete(int? idCidade)
        {
            try
            {
                string sql = "DELETE FROM tbCidades WHERE idCidade = " + idCidade;
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

            if (id != null && !string.IsNullOrEmpty(filter))
            {
                swhere = " WHERE idCidade = " + id + " OR " + "tbCidades.nmCidade LIKE'%" + filter + "%'";
            } 
            else if (id != null)
            {
                swhere = " WHERE idCidade = " + id;
            }
            else if (!string.IsNullOrEmpty(filter))
            {
                swhere += " WHERE tbCidades.nmCidade LIKE'%" + filter + "%'";
            }
            sql = @"
                SELECT
                    tbcidades.idCidade AS idCidade,
                    tbcidades.nmCidade AS nmCidade,
                    tbcidades.DDD AS DDD,
                    tbcidades.dtCadastro AS dtCadastro,
                    tbcidades.dtUltAlteracao AS dtUltAlteracao,
                    tbestados.idestado AS idEstado,
                    tbestados.nmEstado AS nmEstado,
                    tbpaises.nmPais AS nmPais
                FROM tbCidades
                INNER JOIN tbEstados on tbCidades.idEstado = tbEstados.idEstado 
                INNER JOIN tbPaises on tbEstados.idPais = tbPaises.idPais" + swhere;
            return sql;
        }
    }
}