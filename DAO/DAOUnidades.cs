using Sistema.Models;
using System.Data.SqlClient;

namespace Sistema.DAO
{
    public class DAOUnidades : DAO
    {
        public List<Unidades> GetUnidades()
        {
            try
            {
                var sql = this.Search(null, null);
                OpenConnection();
                SqlQuery = new SqlCommand(sql, con);
                reader = SqlQuery.ExecuteReader();
                var list = new List<Unidades>();

                while (reader.Read())
                {
                    var unidade = new Unidades
                    {
                        idUnidade = Convert.ToInt32(reader["idUnidade"]),
                        dsUnidade = Convert.ToString(reader["dsUnidade"]),
                        dtCadastro = Convert.ToDateTime(reader["dtCadastro"]),
                        dtUltAlteracao = Convert.ToDateTime(reader["dtUltAlteracao"])
                    };

                    list.Add(unidade);
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

        public void Insert(Models.Unidades unidade)
        {
            try
            {
                var sql = string.Format("INSERT INTO tbUnidades (dsUnidade, dtCadastro, dtUltAlteracao) VALUES ('{0}', '{1}', '{2}')",
                    unidade.dsUnidade,
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

        public void Update(Models.Unidades unidade)
        {
            try
            {
                string sql = "UPDATE tbUnidades SET dsUnidade = '"
                             + unidade.dsUnidade + "',"
                             + " dtUltAlteracao = '" + DateTime.Now.ToString("dd/MM/yyyy")
                             + "' WHERE idUnidade = " + unidade.idUnidade;
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

        public Unidades GetUnidade(int? idUnidade)
        {
            try
            {
                var model = new Models.Unidades();

                if (idUnidade != null)
                {
                    OpenConnection();
                    var sql = this.Search(idUnidade, null);
                    SqlQuery = new SqlCommand(sql, con);
                    reader = SqlQuery.ExecuteReader();

                    while (reader.Read())
                    {
                        model.idUnidade = Convert.ToInt32(reader["idUnidade"]);
                        model.dsUnidade = Convert.ToString(reader["dsUnidade"]);
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

        public void Delete(int? idUnidade)
        {
            try
            {
                string sql = "DELETE FROM tbUnidades WHERE idUnidade = " + idUnidade;
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
                swhere = " WHERE idUnidade = " + id;
            }
            if (!string.IsNullOrEmpty(filter))
            {
                var filterQ = filter.Split(' ');
                foreach (var word in filterQ)
                {
                    swhere += " OR tbUnidades.dsUnidade LIKE'%" + word + "%'";
                }
                swhere = " WHERE " + swhere.Remove(0, 3);
            }
            sql = @"
                    SELECT
                        idUnidade AS idUnidade,
                        dsUnidade AS dsUnidade,
                        dtCadastro AS dtCadastro,
                        dtUltAlteracao AS dtUltAlteracao
                    FROM tbUnidades" + swhere;
            return sql;
        }
    }
}
