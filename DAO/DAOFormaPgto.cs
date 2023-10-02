using Sistema.Models;
using System.Data.SqlClient;

namespace Sistema.DAO
{
    public class DAOFormaPgto : DAO
    {
        public List<FormaPgto> GetFormaPgtos()
        {
            try
            {
                var sql = this.Search(null, null);
                OpenConnection();
                SqlQuery = new SqlCommand(sql, con);
                reader = SqlQuery.ExecuteReader();
                var list = new List<FormaPgto>();

                while (reader.Read())
                {
                    var FormaPgto = new FormaPgto
                    {
                        idFormaPgto = Convert.ToInt32(reader["idFormaPgto"]),
                        nomeForma = Convert.ToString(reader["nomeForma"]),
                        dtCadastro = Convert.ToDateTime(reader["dtCadastro"]),
                        dtUltAlteracao = Convert.ToDateTime(reader["dtUltAlteracao"])
                    };

                    list.Add(FormaPgto);
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

        public void Insert(Models.FormaPgto FormaPgto)
        {
            try
            {
                var sql = string.Format("INSERT INTO tbFormaPgto (nomeForma, dtCadastro, dtUltAlteracao) VALUES ('{0}', '{1}', '{2}')",
                    FormaPgto.nomeForma,
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

        public void Update(Models.FormaPgto FormaPgto)
        {
            try
            {
                string sql = "UPDATE tbFormaPgto SET nomeForma = '"
                             + FormaPgto.nomeForma + "',"
                             + " dtUltAlteracao = '" + DateTime.Now.ToString("dd/MM/yyyy")
                             + "' WHERE idFormaPgto = " + FormaPgto.idFormaPgto;
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

        public FormaPgto GetFormaPgto(int? idFormaPgto)
        {
            try
            {
                var model = new Models.FormaPgto();

                if (idFormaPgto != null)
                {
                    OpenConnection();
                    var sql = this.Search(idFormaPgto, null);
                    SqlQuery = new SqlCommand(sql, con);
                    reader = SqlQuery.ExecuteReader();

                    while (reader.Read())
                    {
                        model.idFormaPgto = Convert.ToInt32(reader["idFormaPgto"]);
                        model.nomeForma = Convert.ToString(reader["nomeForma"]);
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

        public void Delete(int? idFormaPgto)
        {
            try
            {
                string sql = "DELETE FROM tbFormaPgto WHERE idFormaPgto = " + idFormaPgto;
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
                swhere = " WHERE idFormaPgto = " + id;
            }
            if (!string.IsNullOrEmpty(filter))
            {
                var filterQ = filter.Split(' ');
                foreach (var word in filterQ)
                {
                    swhere += " OR tbFormaPgto.nomeForma LIKE'%" + word + "%'";
                }
                swhere = " WHERE " + swhere.Remove(0, 3);
            }
            sql = @"
                    SELECT
                        idFormaPgto AS idFormaPgto,
                        nomeForma AS nomeForma,
                        dtCadastro AS dtCadastro,
                        dtUltAlteracao AS dtUltAlteracao
                    FROM tbFormaPgto" + swhere;
            return sql;
        }
    }
}
