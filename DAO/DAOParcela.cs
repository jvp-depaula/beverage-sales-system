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
                var sql = this.Search(null, null, null, null, null);
                OpenConnection();
                SqlQuery = new SqlCommand(sql, con);
                reader = SqlQuery.ExecuteReader();
                var list = new List<Parcela>();

                while (reader.Read())
                {
                    var Parcelas = new Parcela
                    {
                        idCondicaoPgto = Convert.ToInt32(reader["idCondicaoPgto"]),
                        idFormaPgto = Convert.ToInt32(reader["idFormaPgto"]),
                        dias = Convert.ToString(reader["dias"]),
                        txPercentParcela = Convert.ToDecimal(reader["txPercentParcela"]),
                        txPercentJuros = Convert.ToDecimal(reader["txPercentJuros"]),
                        txPercentMulta = Convert.ToDecimal(reader["txPercentMulta"]),
                        txPercentDesconto = Convert.ToDecimal(reader["txPercentDesconto"]),
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
                var sql = string.Format("INSERT INTO tbParcelas (idCondicaoPgto, idFormaPgto, dias, txPercentParcela " +
                    "txPercentJuros, txPercentMulta, txPercentDesconto, dtCadastro, dtUltAlteracao) " +
                    "VALUES ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}')",
                    Convert.ToInt32(Parcela.idCondicaoPgto),
                    Convert.ToInt32(Parcela.idFormaPgto),
                    Parcela.dias,
                    Convert.ToDecimal(Parcela.txPercentParcela),
                    Convert.ToDecimal(Parcela.txPercentJuros),
                    Convert.ToDecimal(Parcela.txPercentMulta),
                    Convert.ToDecimal(Parcela.txPercentDesconto),
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

        public void Update(Models.Parcela Parcela)
        {
            try
            {
                string sql = "UPDATE tbParcela SET dias = '" + Parcela.dias + "',"
                             + " txPercentParcela = '" + Parcela.txPercentParcela + "',"
                             + " txPercentJuros = '" + Parcela.txPercentJuros + "',"
                             + " txPercentMulta = '" + Parcela.txPercentMulta + "',"
                             + " txPercentDesconto = '" + Parcela.txPercentDesconto + "',"
                             + " dtUltAlteracao = '" + DateTime.Now.ToString("dd/MM/yyyy") + "'"
                             + " WHERE idCondicaoPgto = '" + Parcela.idCondicaoPgto + "'"
                             + " AND idFormaPgto = '" + Parcela.idFormaPgto + "'";
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

        public Parcela GetParcelas(int? idCondicaoPgto, int? idFormaPgto)
        {
            try
            {
                var model = new Models.Parcela();

                if (idCondicaoPgto != null && idFormaPgto != null)
                {
                    OpenConnection();
                    var sql = this.Search(idCondicaoPgto, idFormaPgto);
                    SqlQuery = new SqlCommand(sql, con);
                    reader = SqlQuery.ExecuteReader();

                    while (reader.Read())
                    {
                        model.idCondicaoPgto = Convert.ToInt32(reader["idCondicaoPgto"]);
                        model.idFormaPgto = Convert.ToInt32(reader["idFormaPgto"]);
                        model.dias = Convert.ToString(reader["dias"]);
                        model.txPercentParcela = Convert.ToDecimal(reader["txPercentParcela"]);
                        model.txPercentJuros = Convert.ToDecimal(reader["txPercentJuros"]);
                        model.txPercentMulta = Convert.ToDecimal(reader["txPercentMulta"]);
                        model.txPercentDesconto = Convert.ToDecimal(reader["txPercentDesconto"]);                        
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

        public void Delete(int? idCondicaoPgto, int? idFormaPgto)
        {
            try
            {
                string sql = "DELETE FROM tbParcelas WHERE idCondicaoPgto = " + idCondicaoPgto + " AND idFormaPgto = " + idFormaPgto + ";";
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

        private string Search(int? idCondicaoPgto, int? idFormaPgto)
        {
            var sql = string.Empty;
            var swhere = string.Empty;
            if (idCondicaoPgto != null && idFormaPgto != null)
            {
                swhere = " WHERE idCondicaoPgto = " + idCondicaoPgto + " AND idFormaPgto = " + idFormaPgto;
            }
            sql = @"
                    SELECT
                        tbParcela.idCondicaoPgto AS idCondicaoPgto,
                        tbParcela.idFormaPgto AS idFormaPgto,
                        tbParcela.dias AS dias,
                        tbParcela.txPercentParcela AS txPercentParcela,
                        tbParcela.txPercentJuros AS txPercentJuros,
                        tbParcela.txPercentMulta AS txPercentMulta,
                        tbParcela.txPercentDesconto AS txPercentDesconto,
                        tbParcela.dtCadastro AS dtCadastro,
                        tbParcela.dtUltAlteracao AS dtUltAlteracao
                    FROM tbParcela" + swhere;
            return sql;
        }
    }
}
