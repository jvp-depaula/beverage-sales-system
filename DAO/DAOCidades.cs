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
                        idEstado = Convert.ToInt32(reader["idEstado"])
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

        public bool Insert(Cidades cidade)
        {
            try
            {
                var sql = string.Format("INSERT INTO tbCidades ( nmCidade, DDD, idEstado, dtCadastro, dtUltAlteracao) VALUES ('{0}', '{1}', '{2}', {3}, '{4}')",
                    this.FormatString(cidade.nmCidade),
                    this.FormatString(cidade.DDD),
                    cidade.idEstado,
                    DateTime.Now.ToString("yyyy-MM-dd"),
                    DateTime.Now.ToString("yyyy-MM-dd")
                    );
                OpenConnection();
                SqlQuery = new SqlCommand(sql, con);
                int i = SqlQuery.ExecuteNonQuery();

                if (i > 1)
                {
                    return true;
                }
                else
                {
                    return false;
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

        public bool Update(Cidades cidade)
        {
            try
            {
                string sql = "UPDATE tbCidades SET nmCidade = '" + this.FormatString(cidade.nmCidade) + "'," +
                    " DDD = '" + this.FormatString(cidade.DDD) + "'," +
                    " dtUltAlteracao = '" + DateTime.Now.ToString("yyyy-MM-dd") + "'," +
                    " idEstado = " + cidade.idEstado +
                    " WHERE idCidade = " + cidade.idCidade;
                OpenConnection();
                SqlQuery = new SqlCommand(sql, con);

                int i = SqlQuery.ExecuteNonQuery();

                if (i > 1)
                {
                    return true;
                }
                else
                {
                    return false;
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

        public Cidades GetCidade(int? idCidade)
        {
            try
            {
                var model = new Cidades();
                if (idCidade != null)
                {
                    OpenConnection();
                    var sql = this.Search(idCidade, null);
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

        public bool Delete(int? idCidade)
        {
            try
            {
                string sql = "DELETE FROM tbCidades WHERE idCidade = " + idCidade;
                OpenConnection();
                SqlQuery = new SqlCommand(sql, con);

                int i = SqlQuery.ExecuteNonQuery();

                if (i > 1)
                {
                    return true;
                }
                else
                {
                    return false;
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

        private string Search(int? id, string filter)
        {
            var sql = string.Empty;
            var swhere = string.Empty;
            if (id != null)
            {
                swhere = " WHERE idCidade = " + id;
            }
            if (!string.IsNullOrEmpty(filter))
            {
                var filterQ = filter.Split(' ');
                foreach (var word in filterQ)
                {
                    swhere += " OR tbCidades.nmCidade LIKE'%" + word + "%'";
                }
                swhere = " WHERE " + swhere.Remove(0, 3);
            }
            sql = @"
                SELECT
                    tbcidades.idCidade AS idCidade,
                    tbcidades.nmCidade AS nmCidade,
                    tbcidades.DDD AS DDD,
                    tbcidades.dtCadastro AS dtCadastro,
                    tbcidades.dtUltAlteracao AS dtUltAlteracao,
                    tbestados.idestado AS idEstado,
                    tbestados.nmEstado AS nmEstado
                FROM tbCidades
                INNER JOIN tbEstados on tbCidades.idEstado = tbEstados.idEstado " + swhere;
            return sql;
        }


    }
}