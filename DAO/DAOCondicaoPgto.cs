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
                        nomeCondicao = Convert.ToString(reader["nomeCondicao"]),
                        idFormaPgto = Convert.ToInt32(reader["idFormaPgto"]),
                        txJuros = Convert.ToDecimal(reader["txJuros"]),
                        txPercentual = Convert.ToDecimal(reader["txPercentual"]),
                        qtdDias = Convert.ToInt32(reader["qtdDias"]),
                        multa = Convert.ToDecimal(reader["multa"]),
                        desconto = Convert.ToDecimal(reader["desconto"]),
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
                var sql = string.Format("INSERT INTO tbCondicaoPgto (nomeCondicao, idFormaPgto, txJuros, txPercentual, qtdDias, multa, desconto, dtCadastro, dtUltAlteracao) VALUES ('{0}', '{1}', '{2}', '{3}', '{4}'," +
                    "'{5}', '{6}', '{7}', '{8}')",
                    condicaoPgto.nomeCondicao,
                    condicaoPgto.idFormaPgto,
                    condicaoPgto.txJuros,
                    condicaoPgto.txPercentual,
                    condicaoPgto.qtdDias,
                    condicaoPgto.multa,
                    condicaoPgto.desconto,
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
                string sql = "UPDATE tbCondicaoPgto SET nomeCondicao = '"
                             + condicaoPgto.nomeCondicao + "',"
                             + condicaoPgto.idFormaPgto + "',"
                             + condicaoPgto.txJuros + "',"
                             + condicaoPgto.txPercentual + "',"
                             + condicaoPgto.qtdDias + "',"
                             + condicaoPgto.multa + "',"
                             + condicaoPgto.desconto + "',"
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
                        model.nomeCondicao= Convert.ToString(reader["nomeCondicao"]);
                        model.idFormaPgto = Convert.ToInt32(reader["idFormaPgto"]);
                        model.txJuros = Convert.ToDecimal(reader["txJuros"]);
                        model.txPercentual = Convert.ToDecimal(reader["txPercentual"]);
                        model.qtdDias = Convert.ToInt32(reader["qtdDias"]);
                        model.multa = Convert.ToDecimal(reader["multa"]);
                        model.desconto = Convert.ToDecimal(reader["desconto"]);
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
                    swhere += " OR tbCondicaoPgto.nomeCondicao LIKE '%" + word + "%'";
                }
                swhere = " WHERE " + swhere.Remove(0, 3);
            }
            sql = @"
                    SELECT
                        idcondicaoPgto AS idcondicaoPgto,
                        nomeCondicao AS nomeCondicao,
                        idFormaPgto AS idFormaPgto,
                        txJuros AS txJuros,
                        txPercentual AS txPercentual,
                        qtdDias AS qtdDias,
                        multa AS multa,
                        desconto AS desconto,
                        dtCadastro AS dtCadastro,
                        dtUltAlteracao AS dtUltAlteracao
                    FROM tbCondicaoPgto" + swhere;
            return sql;
        }
    }
}
