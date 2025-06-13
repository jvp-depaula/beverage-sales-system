using Sistema.Models;
using System.Data.SqlClient;
using System.Drawing;

namespace Sistema.DAO
{
    public class DAOPaises : DAO
    {
        public List<Paises> GetPaises()
        {
            try
            {
                var sql = this.Search(null, null);
                OpenConnection();
                SqlQuery = new SqlCommand(sql, con);
                reader = SqlQuery.ExecuteReader();
                var list = new List<Paises>();

                while (reader.Read())
                {
                    var pais = new Paises
                    {
                        idPais = Convert.ToInt32(reader["idPais"]),
                        nmPais = Convert.ToString(reader["nmPais"]),
                        sigla = Convert.ToString(reader["sigla"]),
                        DDI = Convert.ToString(reader["DDI"]),
                        dtCadastro = Convert.ToDateTime(reader["dtCadastro"]),
                        dtUltAlteracao = Convert.ToDateTime(reader["dtUltAlteracao"])
                    };

                    list.Add(pais);
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
        public void Insert(Models.Paises pais)
        {
            try
            {
                var sql = string.Format("INSERT INTO tbPaises (nmPais, DDI, sigla, dtCadastro, dtUltAlteracao) VALUES ('{0}', '{1}', '{2}', '{3}', '{4}')",
                    pais.nmPais,
                    pais.DDI,
                    pais.sigla,
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

        public void Update(Models.Paises pais)
        {
            try
            {
                string sql = String.Format("UPDATE tbPaises SET nmPais = '{0}', DDI = '{1}', sigla = '{2}', dtUltAlteracao = '{3}' WHERE idPais = '{4}'",
                    pais.nmPais,
                    pais.DDI,
                    pais.sigla,
                    DateTime.Now.ToString("dd/MM/yyyy"),
                    pais.idPais);

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

        public Paises GetPais(int? idPais)
        {
            try
            {
                var model = new Models.Paises();
                if (idPais != null)
                {
                    OpenConnection();
                    var sql = this.Search(idPais, null);
                    SqlQuery = new SqlCommand(sql, con);
                    reader = SqlQuery.ExecuteReader();
                    while (reader.Read())
                    {
                        model.idPais = Convert.ToInt32(reader["idPais"]);
                        model.nmPais = Convert.ToString(reader["nmPais"]);
                        model.DDI = Convert.ToString(reader["DDI"]);
                        model.sigla = Convert.ToString(reader["sigla"]);
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

        public void Delete(int? idPais)
        {
            try
            {
                string sql = "DELETE FROM tbPaises WHERE idPais = " + idPais;
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
                swhere = " WHERE idPais = " + id;
            }
            if (!string.IsNullOrEmpty(filter))
            {
                var filterQ = filter.Split(' ');
                foreach (var word in filterQ)
                {
                    swhere += " OR tbPaises.nmPais LIKE'%" + word + "%'";
                }
                swhere = " WHERE " + swhere.Remove(0, 3);
            }
            sql = @"
                    SELECT
                        idPais AS idPais,
                        nmPais AS nmPais,
                        DDI AS DDI,
                        sigla AS sigla,
                        dtCadastro AS dtCadastro,
                        dtUltAlteracao AS dtUltAlteracao
                    FROM tbPaises" + swhere;
            return sql;
        }
    }
}
