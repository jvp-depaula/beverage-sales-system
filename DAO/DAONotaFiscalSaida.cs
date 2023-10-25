using Microsoft.CodeAnalysis.FlowAnalysis;
using Sistema.Models;
using System.Data.SqlClient;

namespace Sistema.DAO
{
    public class DAONotaFiscalSaida : DAO
    {
        public List<NotaFiscalSaida> GetNotasFiscaisEntrada()
        {
            try
            {
                var sql = this.Search(null, null, null);
                OpenConnection();
                SqlQuery = new SqlCommand(sql, con);
                reader = SqlQuery.ExecuteReader();
                var list = new List<NotaFiscalSaida>();

                while (reader.Read())
                {
                    var NotasFiscaisSaida = new NotaFiscalSaida
                    {
                        nrModelo = Convert.ToString(reader["nrModelo"]),
                        nrSerie = Convert.ToString(reader["nrSerie"]),
                        nrNota = Convert.ToString(reader["nrNota"]),
                        dtEmissao = Convert.ToDateTime(reader["dtEmissao"]),
                        chaveNFe = Convert.ToString(reader["chaveNFe"]),
                        flSituacao = Convert.ToString(reader["flSituacao"]),
                        vlTotalNota = Convert.ToDecimal(reader["vlTotalNota"]),
                        vlTotalItens = Convert.ToDecimal(reader["vlTotalItens"]),
                        vlDesconto = Convert.ToDecimal(reader["vlDesconto"]),
                        idCliente = Convert.ToInt32(reader["idCliente"]),
                        dtCadastro = Convert.ToDateTime(reader["dtCadastro"]),
                        dtUltAlteracao = Convert.ToDateTime(reader["dtUltalteracao"])
                    };

                    list.Add(NotasFiscaisSaida);
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

        public void Insert(Models.NotaFiscalSaida NotaFiscalSaida)
        {
            try
            {
                var sql = string.Format("INSERT INTO tbNotaFiscalSaida (nrModelo, nrSerie, nrNota, dtEmissao, " +
                    "chaveNFe, flSituacao, vlTotalNota, vlTotalItens, vlDesconto, idcliente, dtCadastro, dtUltAlteracao) " +
                    "VALUES ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}', '{11}')",
                    NotaFiscalSaida.nrModelo,
                    NotaFiscalSaida.nrSerie,
                    NotaFiscalSaida.nrNota,
                    Convert.ToDateTime(NotaFiscalSaida.dtEmissao),
                    NotaFiscalSaida.chaveNFe,
                    NotaFiscalSaida.flSituacao,
                    Convert.ToDecimal(NotaFiscalSaida.vlTotalNota),
                    Convert.ToDecimal(NotaFiscalSaida.vlTotalItens),
                    Convert.ToDecimal(NotaFiscalSaida.vlDesconto),
                    Convert.ToInt32(NotaFiscalSaida.idCliente),
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

        public void Update(Models.NotaFiscalSaida NotaFiscalSaida)
        {
            try
            {
                string sql = "UPDATE tbNotaFiscalSaida SET dtEmissao = '" + Util.Util.FormatDate(NotaFiscalSaida.dtEmissao) + "',"
                             + " chaveNFe = '" + Util.Util.Unmask(NotaFiscalSaida.chaveNFe) + "',"
                             + " flSituacao = '" + NotaFiscalSaida.flSituacao + "',"
                             + " vlTotalNota = '" + NotaFiscalSaida.vlTotalNota + "',"
                             + " vlTotalItens = '" + NotaFiscalSaida.vlTotalItens + "',"
                             + " vlDesconto = '" + NotaFiscalSaida.vlDesconto + "',"
                             + " idCliente = '" + NotaFiscalSaida.idCliente + "', "
                             + " dtUltAlteracao = '" + DateTime.Now.ToString("dd/MM/yyyy") + "'"
                             + " AND nrModelo = '" + NotaFiscalSaida.nrModelo + "'"
                             + " AND nrSerie = '" + NotaFiscalSaida.nrSerie + "'"
                             + " AND nrNota = '" + NotaFiscalSaida.nrNota + "'";                             
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

        public NotaFiscalSaida GetNotaFiscalSaida(string nrModelo, string nrSerie, string nrNota)
        {
            try
            {
                var model = new Models.NotaFiscalSaida();

                if (!String.IsNullOrEmpty(nrModelo) && !String.IsNullOrEmpty(nrSerie) && !String.IsNullOrEmpty(nrNota))
                {
                    OpenConnection();
                    var sql = this.Search(nrModelo, nrSerie, nrNota);
                    SqlQuery = new SqlCommand(sql, con);
                    reader = SqlQuery.ExecuteReader();

                    while (reader.Read())
                    {
                        model.nrModelo = Convert.ToString(reader["nrModelo"]);
                        model.nrSerie = Convert.ToString(reader["nrSerie"]);
                        model.nrNota = Convert.ToString(reader["nrNota"]);
                        model.dtEmissao = Convert.ToDateTime(reader["dtEmissao"]);
                        model.chaveNFe = Convert.ToString(reader["chaveNFe"]);
                        model.flSituacao = Convert.ToString(reader["flSituacao"]);
                        model.vlTotalNota = Convert.ToDecimal(reader["vlTotalNota"]);
                        model.vlTotalItens = Convert.ToDecimal(reader["vlTotalItens"]);
                        model.vlDesconto = Convert.ToDecimal(reader["vlDesconto"]);
                        model.idCliente = Convert.ToInt32(reader["idCliente"]);
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

        public void Delete(string nrModelo, string nrSerie, string nrNota)
        {
            try
            {
                string sql = "DELETE FROM tbNotaFiscalSaida WHERE nrModelo = " + nrModelo +
                             " AND nrSerie = " + nrSerie + " AND nrNota = " + nrNota + ";";
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

        private string Search(string filter, string filter2, string filter3)
        {
            var sql = string.Empty;
            var swhere = string.Empty;
            if (!String.IsNullOrEmpty(filter) && !String.IsNullOrEmpty(filter2) && !String.IsNullOrEmpty(filter3))
            {
                swhere = " WHERE nrModelo = " + filter + " AND nrSerie = " + filter2 + " AND nrNota = " + filter3;
            }
            sql = @"
                    SELECT
                        tbNotaFiscalSaida.nrModelo AS nrModelo,
                        tbNotaFiscalSaida.nrSerie AS nrSerie,
                        tbNotaFiscalSaida.nrNota AS nrNota,
                        tbNotaFiscalSaida.dtEmissao AS dtEmissao,
                        tbNotaFiscalSaida.chaveNFe AS chaveNFe,
                        tbNotaFiscalSaida.flSituacao AS flSituacao,
                        tbNotaFiscalSaida.vlTotalNota AS vlTotalNota,
                        tbNotaFiscalSaida.vlTotalItens AS vlTotalItens,
                        tbNotaFiscalSaida.vlDesconto AS vlDesconto,
                        tbNotaFiscalSaida.idCliente AS idCliente,
                        tbClientes.nmCliente AS nmCliente,
                        tbNotaFiscalSaida.dtCadastro AS dtCadastro,
                        tbNotaFiscalSaida.dtUltAlteracao AS dtUltAlteracao
                    FROM tbNotaFiscalSaida
                    INNER JOIN tbClientes ON tbNotaFiscalSaida.idCliente = tbClientes.idCliente " + swhere;
            return sql;
        }
    }
}
