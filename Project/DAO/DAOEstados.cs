using Sistema.Models;
using System.Data.SqlClient;

namespace Sistema.DAO
{
    public class DAOEstados : Sistema.DAO.DAO
    {

        public List<Estados> GetEstados()
        {
            try
            {
                var sql = this.Search(null, null);
                OpenConnection();
                SqlQuery = new SqlCommand(sql, con);
                reader = SqlQuery.ExecuteReader();
                var list = new List<Estados>();

                while (reader.Read())
                {
                    var estado = new Estados
                    {
                        idEstado = Convert.ToInt32(reader["idEstado"]),
                        nmEstado = Convert.ToString(reader["nmEstado"]),
                        flUF = Convert.ToString(reader["flUF"]),
                        idPais = Convert.ToInt32(reader["idPais"]),
                        nmPais = Convert.ToString(reader["nmPais"]),
                        dtCadastro = Convert.ToDateTime(reader["dtCadastro"]),
                        dtUltAlteracao = Convert.ToDateTime(reader["dtUltAlteracao"])
                    };

                    list.Add(estado);
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

        public void Insert(Models.Estados estado)
        {
            try
            {
                var sql = string.Format("INSERT INTO tbEstados ( nmEstado, flUF, idPais, dtCadastro, dtUltAlteracao) VALUES ('{0}', '{1}', {2}, '{3}', '{4}')",
                    estado.nmEstado,
                    estado.flUF,
                    Convert.ToInt32(estado.idPais),
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

        public void Update(Models.Estados estado)
        {
            try
            {
                var sql = String.Format("UPDATE tbEstados SET nmEstado = '{0}', flUF = '{1}', dtUltAlteracao = '{2}', idPais = '{3}' WHERE idEstado = '{4}'",
                    estado.nmEstado,
                    estado.flUF,
                    Util.Util.FormatDate(DateTime.Now),
                    estado.idPais,
                    estado.idEstado);
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

        public Estados GetEstado(int? idEstado)
        {
            try
            {
                var model = new Models.Estados();
                if (idEstado != null)
                {
                    OpenConnection();
                    var sql = this.Search(idEstado, null);
                    SqlQuery = new SqlCommand(sql, con);
                    reader = SqlQuery.ExecuteReader();
                    while (reader.Read())
                    {
                        model.idEstado = Convert.ToInt32(reader["idEstado"]);
                        model.nmEstado = Convert.ToString(reader["nmEstado"]);
                        model.flUF = Convert.ToString(reader["flUF"]);
                        model.dtCadastro = Convert.ToDateTime(reader["dtCadastro"]);
                        model.dtUltAlteracao = Convert.ToDateTime(reader["dtUltAlteracao"]);
                        model.idPais = Convert.ToInt32(reader["idPais"]);
                        model.nmPais = Convert.ToString(reader["nmPais"]);
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

        public void Delete(int? idEstado)
        {
            try
            {
                string sql = "DELETE FROM tbEstados WHERE idEstado = " + idEstado;
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
                swhere = " WHERE idEstado = " + id;
            }
            if (!string.IsNullOrEmpty(filter))
            {
                var filterQ = filter.Split(' ');
                foreach (var word in filterQ)
                {
                    swhere += " OR tbEstados.nmEstado LIKE'%" + word + "%'";
                }
                swhere = " WHERE " + swhere.Remove(0, 3);
            }
            sql = @"
                SELECT
                    tbEstados.idEstado AS idEstado,
                    tbEstados.nmEstado AS nmEstado,
                    tbEstados.flUF AS flUF,
                    tbEstados.dtCadastro AS dtCadastro,
                    tbEstados.dtUltAlteracao AS dtUltAlteracao,
                    tbEstados.idPais AS idPais,
                    tbPaises.nmPais AS nmPais
                FROM tbEstados
                INNER JOIN tbPaises on tbEstados.idPais = tbPaises.idPais " + swhere;
            return sql;
        }
    }
}