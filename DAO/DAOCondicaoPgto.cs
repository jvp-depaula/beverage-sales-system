using Sistema.Models;
using System.Data.SqlClient;

namespace Sistema.DAO
{
    public class DAOCondicaoPgto : DAO
    {
        public List<CondicaoPgto> GetCondicoesPgto()
        {
            try
            {
                var sql = this.Search(null, null);
                OpenConnection();
                SqlQuery = new SqlCommand(sql, con);
                reader = SqlQuery.ExecuteReader();
                var list = new List<CondicaoPgto>();

                while (reader.Read())
                {
                    var condicaoPgto = new CondicaoPgto
                    {
                        idCondicaoPgto = Convert.ToInt32(reader["idCondicaoPgto"]),
                        dsCondicaoPgto = Convert.ToString(reader["dsCondicaoPgto"]),
                        vlJuros = Convert.ToDecimal(reader["vlJuros"]),
                        vlMulta = Convert.ToDecimal(reader["vlMulta"]),
                        vlDesconto = Convert.ToDecimal(reader["vlDesconto"]),
                        dtCadastro = Convert.ToDateTime(reader["dtCadastro"]),
                        dtUltAlteracao = Convert.ToDateTime(reader["dtUltAlteracao"])
                    };

                    list.Add(condicaoPgto);
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

        public void Insert(Models.CondicaoPgto condicaoPgto)
        {
            try
            {
                var sql = string.Format("INSERT INTO tbCondicaoPgto (dsCondicaoPgto, vlMulta, vlJuros, vlDesconto, dtCadastro, dtUltAlteracao) VALUES ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}')",
                    condicaoPgto.dsCondicaoPgto,
                    condicaoPgto.vlMulta,
                    condicaoPgto.vlJuros,
                    condicaoPgto.vlDesconto,
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

        public void Update(Models.CondicaoPgto condicaoPgto)
        {
            try
            {
                var sql = String.Format("UPDATE tbCondicaoPgto SET dsCondicaoPgto = '{0}', vlMulta = '{1}', vlDesconto = '{2}', vlJuros = '{3}', dtUltAlteracao = '{4}' WHERE idCondicaoPgto = '{5}'",
                    condicaoPgto.dsCondicaoPgto,
                    condicaoPgto.vlMulta,
                    condicaoPgto.vlDesconto,
                    condicaoPgto.vlJuros,
                    Util.Util.FormatDate(DateTime.Now),
                    condicaoPgto.idCondicaoPgto
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

        public CondicaoPgto GetCondicaoPgto(int? idcondicaoPgto)
        {
            try
            {
                var model = new Models.CondicaoPgto();

                if (idcondicaoPgto != null)
                {
                    OpenConnection();
                    var sql = this.Search(idcondicaoPgto, null);
                    SqlQuery = new SqlCommand(sql, con);
                    reader = SqlQuery.ExecuteReader();

                    while (reader.Read())
                    {
                        model.idCondicaoPgto = Convert.ToInt32(reader["idCondicaoPgto"]);
                        model.dsCondicaoPgto = Convert.ToString(reader["dsCondicaoPgto"]);
                        model.vlMulta = Convert.ToDecimal(reader["vlMulta"]);
                        model.vlDesconto = Convert.ToDecimal(reader["vlDesconto"]);
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

        public void Delete(int? idcondicaoPgto)
        {
            try
            {
                string sql = "DELETE FROM tbCondicaoPgto WHERE idCondicaoPgto = " + idcondicaoPgto;
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
                swhere = " WHERE idcondicaoPgto = " + id;
            }
            if (!string.IsNullOrEmpty(filter))
            {
                var filterQ = filter.Split(' ');
                foreach (var word in filterQ)
                {
                    swhere += " OR tbCondicaoPgto.dsCondicaoPgto LIKE '%" + word + "%'";
                }
                swhere = " WHERE " + swhere.Remove(0, 3);
            }
            sql = @"
                    SELECT
                        tbCondicaoPgto.idcondicaoPgto AS idcondicaoPgto,
                        tbCondicaoPgto.dsCondicaoPgto AS dsCondicaoPgto,
                        tbCondicaoPgto.vlMulta AS vlMulta,
                        tbCondicaoPgto.vlDesconto AS vlDesconto,
                        tbCondicaoPgto.vlJuros AS vlJuros,
                        tbCondicaoPgto.dtCadastro AS dtCadastro,
                        tbCondicaoPgto.dtUltAlteracao AS dtUltAlteracao
                    FROM tbCondicaoPgto" + swhere;
            return sql;
        }
    }
}
