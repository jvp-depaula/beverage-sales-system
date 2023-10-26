using Microsoft.CodeAnalysis.FlowAnalysis;
using Sistema.Models;
using System.Data.SqlClient;

namespace Sistema.DAO
{
    public class DAOParcela : DAO
    {
        public List<Parcela> GetParcelas()
        {
            try
            {
                var sql = this.Search(null, null);
                OpenConnection();
                SqlQuery = new SqlCommand(sql, con);
                reader = SqlQuery.ExecuteReader();
                var list = new List<Parcela>();

                while (reader.Read())
                {
                    var Parcelas = new Parcela
                    {
                        idCondicaoPgto = Convert.ToInt32(reader["idCondicaoPgto"]),
                        nrParcela = Convert.ToInt32(reader["nrParcela"]),
                        dias = Convert.ToString(reader["dias"]),
                        idFormaPgto = Convert.ToInt32(reader["idFormaPgto"]),
                        txPercentParcela = Convert.ToDecimal(reader["txPercentParcela"]),
                        dtCadastro = Convert.ToDateTime(reader["dtCadastro"]),
                        dtUltAlteracao = Convert.ToDateTime(reader["dtUltalteracao"])
                    };

                    list.Add(Parcelas);
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

        public void Insert(Models.Parcela Parcela)
        {
            try
            {
                var sql = string.Format("INSERT INTO tbParcelas (idCondicaoPgto, nrParcela, dias, idFormaPgto, txPercentParcela dtUltAlteracao) " +
                    "VALUES ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}')",
                    Convert.ToInt32(Parcela.idCondicaoPgto),
                    Convert.ToInt32(Parcela.nrParcela),
                    Convert.ToInt32(Parcela.dias),
                    Convert.ToInt32(Parcela.idFormaPgto),
                    Convert.ToDecimal(Parcela.txPercentParcela),
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

        public void Update(Models.Parcela Parcela)
        {
            try
            {
                var sql = string.Format("UPDATE tbParcelas SET dias = '{0}', idFormaPgto = '{1}', " +
                    "txPercentParcela = '{2}', dtUltAlteracao = '{3}' WHERE idCondicaoPgto = '{4}', nrParcela = '{5}'",
                    Convert.ToInt32(Parcela.dias),
                    Convert.ToInt32(Parcela.idFormaPgto),
                    Convert.ToDecimal(Parcela.txPercentParcela),
                    Util.Util.FormatDate(DateTime.Now),
                    Convert.ToInt32(Parcela.idCondicaoPgto),
                    Convert.ToInt32(Parcela.nrParcela));
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

        public Parcela GetParcelas(int? idCondicaoPgto, int? nrParcela)
        {
            try
            {
                var model = new Models.Parcela();

                if (idCondicaoPgto != null && nrParcela != null)
                {
                    OpenConnection();
                    var sql = this.Search(idCondicaoPgto, nrParcela);
                    SqlQuery = new SqlCommand(sql, con);
                    reader = SqlQuery.ExecuteReader();

                    while (reader.Read())
                    {
                        model.idCondicaoPgto = Convert.ToInt32(reader["idCondicaoPgto"]);
                        model.nrParcela = Convert.ToInt32(reader["nrParcela"]);
                        model.dias = Convert.ToString(reader["dias"]);
                        model.idFormaPgto = Convert.ToInt32(reader["idFormaPgto"]);
                        model.txPercentParcela = Convert.ToDecimal(reader["txPercentParcela"]);
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

        public void Delete(int? idCondicaoPgto, int? nrParcela)
        {
            try
            {
                string sql = "DELETE FROM tbParcelas WHERE idCondicaoPgto = " + idCondicaoPgto + " AND nrParcela = " + nrParcela + ";";
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

        private string Search(int? idCondicaoPgto, int? nrParcela)
        {
            var sql = string.Empty;
            var swhere = string.Empty;
            if (idCondicaoPgto != null && nrParcela != null)
            {
                swhere = " WHERE idCondicaoPgto = " + idCondicaoPgto + " AND nrParcela = " + nrParcela;
            }
            sql = @"
                    SELECT
                        tbParcela.idCondicaoPgto AS idCondicaoPgto,
                        tbParcela.nrParcela AS nrParcela,
                        tbParcela.idFormaPgto AS idFormaPgto,
                        tbFormaPgto.dsFormaPgto AS dsFormaPgto,
                        tbParcela.dias AS dias,
                        tbParcela.txPercentParcela AS txPercentParcela,
                        tbParcela.dtCadastro AS dtCadastro,
                        tbParcela.dtUltAlteracao AS dtUltAlteracao
                    INNER JOIN tbFormaPgto ON tbParcela.idFormaPgto = tbFormaPgto.idFormaPgto
                    FROM tbParcela" + swhere;
            return sql;
        }
    }
}
