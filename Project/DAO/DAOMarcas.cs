using Sistema.Models;
using System.Data.SqlClient;

namespace Sistema.DAO
{
    public class DAOMarcas : DAO
    {
        public List<Marcas> GetMarcas()
        {
            try
            {
                var sql = this.Search(null, null);
                OpenConnection();
                SqlQuery = new SqlCommand(sql, con);
                reader = SqlQuery.ExecuteReader();
                var list = new List<Marcas>();

                while (reader.Read())
                {
                    var marca = new Marcas
                    {
                        idMarca = Convert.ToInt32(reader["idMarca"]),
                        nmMarca = Convert.ToString(reader["nmMarca"]),
                        dtCadastro = Convert.ToDateTime(reader["dtCadastro"]),
                        dtUltAlteracao = Convert.ToDateTime(reader["dtUltAlteracao"])
                    };

                    list.Add(marca);
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

        public void Insert(Models.Marcas marca)
        {
            try
            {
                var sql = string.Format("INSERT INTO tbMarcas (nmMarca, dtCadastro, dtUltAlteracao) VALUES ('{0}', '{1}', '{2}')",
                    marca.nmMarca,
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

        public void Update(Models.Marcas marca)
        {
            try
            {

                var sql = string.Format("UPDATE tbMarcas SET nmMarca = '{0}', dtUltAlteracao = '{1}' WHERE idMarca = '{2}'",
                    marca.nmMarca,
                    Util.Util.FormatDate(DateTime.Now),
                    marca.idMarca);
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

        public Marcas GetMarca(int? idMarca)
        {
            try
            {
                var model = new Models.Marcas();

                if (idMarca != null)
                {
                    OpenConnection();
                    var sql = this.Search(idMarca, null);
                    SqlQuery = new SqlCommand(sql, con);
                    reader = SqlQuery.ExecuteReader();

                    while (reader.Read())
                    {
                        model.idMarca = Convert.ToInt32(reader["idMarca"]);
                        model.nmMarca = Convert.ToString(reader["nmMarca"]);
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

        public void Delete(int? idMarca)
        {
            try
            {
                string sql = "DELETE FROM tbMarcas WHERE idMarca = " + idMarca;
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
                swhere = " WHERE idMarca = " + id;
            }
            if (!string.IsNullOrEmpty(filter))
            {
                var filterQ = filter.Split(' ');
                foreach (var word in filterQ)
                {
                    swhere += " OR tbMarcas.nmMarca LIKE'%" + word + "%'";
                }
                swhere = " WHERE " + swhere.Remove(0, 3);
            }
            sql = @"
                    SELECT
                        idMarca AS idMarca,
                        nmMarca AS nmMarca,
                        dtCadastro AS dtCadastro,
                        dtUltAlteracao AS dtUltAlteracao
                    FROM tbMarcas" + swhere;
            return sql;
        }
    }
}
