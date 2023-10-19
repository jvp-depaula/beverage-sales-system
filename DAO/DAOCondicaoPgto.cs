using Sistema.Models;
using System.Data.SqlClient;

namespace Sistema.DAO
{
    public class DAOCondicaoPgto : DAO
    {
        public List<CondicaoPgto> GetCondicaoPgto()
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
                        dsCondicaoPgto = Convert.ToString(reader["nomeCondicao"]),
                        idFormaPgto = Convert.ToInt32(reader["idFormaPgto"]),
                        txJuros = Convert.ToDecimal(reader["txJuros"]),
                        txPercentual = Convert.ToDecimal(reader["txPercentual"]),
                        qtdDias = Convert.ToInt32(reader["qtdDias"]),
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
                var sql = string.Format("INSERT INTO tbCondicaoPgto (dsCondicaoPgto, idFormaPgto, txJuros, txPercentual, qtdDias, vlMulta, vlDesconto, dtCadastro, dtUltAlteracao) VALUES ('{0}', '{1}', '{2}', '{3}', '{4}'," +
                    "'{5}', '{6}', '{7}', '{8}')",
                    condicaoPgto.dsCondicaoPgto,
                    condicaoPgto.idFormaPgto,
                    condicaoPgto.txJuros,
                    condicaoPgto.txPercentual,
                    condicaoPgto.qtdDias,
                    condicaoPgto.vlMulta,
                    condicaoPgto.vlDesconto,
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

        public void Update(Models.CondicaoPgto condicaoPgto)
        {
            try
            {
                string sql = "UPDATE tbCondicaoPgto SET dsCondicaoPgto = '"
                             + condicaoPgto.dsCondicaoPgto + "',"
                             + condicaoPgto.idFormaPgto + "',"
                             + condicaoPgto.txJuros + "',"
                             + condicaoPgto.txPercentual + "',"
                             + condicaoPgto.qtdDias + "',"
                             + condicaoPgto.vlMulta + "',"
                             + condicaoPgto.vlDesconto + "',"
                             + " dtUltAlteracao = '" + DateTime.Now.ToString("dd/MM/yyyy")
                             + "' WHERE idcondicaoPgto = " + condicaoPgto.idCondicaoPgto;
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

        public CondicaoPgto GetcondicaoPgto(int? idcondicaoPgto)
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
                        model.idFormaPgto = Convert.ToInt32(reader["idFormaPgto"]);
                        model.txJuros = Convert.ToDecimal(reader["txJuros"]);
                        model.txPercentual = Convert.ToDecimal(reader["txPercentual"]);
                        model.qtdDias = Convert.ToInt32(reader["qtdDias"]);
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
                        tbCondicaoPgto.idFormaPgto AS idFormaPgto,
                        tbFormaPgto.dsFormaPgto AS dsFormaPgto,
                        tbCondicaoPgto.txJuros AS txJuros,
                        tbCondicaoPgto.txPercentual AS txPercentual,
                        tbCondicaoPgto.qtdDias AS qtdDias,
                        tbCondicaoPgto.multa AS multa,
                        tbCondicaoPgto.desconto AS desconto,
                        tbCondicaoPgto.dtCadastro AS dtCadastro,
                        tbCondicaoPgto.dtUltAlteracao AS dtUltAlteracao
                    FROM tbCondicaoPgto
                    INNER JOIN tbFormaPgto ON tbCondicaoPGto.idFormaPgto = tbformaPgto.idFormaPgto " + swhere;
            return sql;
        }
    }
}
