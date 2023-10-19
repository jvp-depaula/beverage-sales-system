using Microsoft.CodeAnalysis.FlowAnalysis;
using Sistema.Models;
using System.Data.SqlClient;

namespace Sistema.DAO
{
    public class DAONotaFiscalEntrada : DAO
    {
        public List<NotaFiscalEntrada> GetNotasFiscaisEntrada()
        {
            try
            {
                var sql = this.Search(null, null);
                OpenConnection();
                SqlQuery = new SqlCommand(sql, con);
                reader = SqlQuery.ExecuteReader();
                var list = new List<NotaFiscalEntrada>();

                while (reader.Read())
                {
                    var NotasFiscaisEntrada = new NotaFiscalEntrada
                    {
                        idFornecedor = Convert.ToInt32(reader["idFornecedor"]),
                        nrModelo = Convert.ToString(reader["nrModelo"]),
                        nrSerie = Convert.ToString(reader["nrSerie"]),
                        nrNota = Convert.ToString(reader["nrNota"]),
                        dtEmissao = Convert.ToDateTime(reader["dtEmissao"]),
                        dtEntrada = Convert.ToDateTime(reader["dtEntrada"]),
                        chaveNFe = Convert.ToString(reader["chaveNFe"]),
                        flSituacao = Convert.ToString(reader["flSituacao"]),
                        vlTotalNota = Convert.ToDecimal(reader["vlTotalNota"]),
                        vlFrete = Convert.ToDecimal(reader["vlFrete"]),
                        vlSeguro = Convert.ToDecimal(reader["vlSeguro"]),
                        vlOutrasDespesas = Convert.ToDecimal(reader["vlOutrasDespesas"]),
                        vlTotalItens = Convert.ToDecimal(reader["vlTotalItens"]),
                        vlDesconto = Convert.ToDecimal(reader["vlDesconto"])
                    };

                    list.Add(NotasFiscaisEntrada);
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

        public void Insert(Models.NotaFiscalEntrada NotaFiscalEntrada)
        {
            try
            {
                var sql = string.Format("INSERT INTO tbNotaFiscalEntrada (idFornecedor, nrModelo, nrSerie, nrNota, dtEmissao, dtEntrada" +
                    "chaveNFe, flSituacao, vlTotalNota, vlFrete, vlSeguro, vlOutrasDespesas, vlTotalItens, vlDesconto, dtCadastro, dtUltAlteracao) " +
                    "VALUES ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}', '{11}', '{12}', '{13}', '{14}', '{15}')",
                    Convert.ToInt32(NotaFiscalEntrada.idFornecedor),
                    NotaFiscalEntrada.nrModelo,
                    NotaFiscalEntrada.nrSerie,
                    NotaFiscalEntrada.nrNota,
                    Convert.ToDateTime(NotaFiscalEntrada.dtEmissao),
                    Convert.ToDateTime(NotaFiscalEntrada.dtEntrada),
                    NotaFiscalEntrada.chaveNFe,
                    NotaFiscalEntrada.flSituacao,
                    Convert.ToDecimal(NotaFiscalEntrada.vlTotalNota),
                    Convert.ToDecimal(NotaFiscalEntrada.vlFrete),
                    Convert.ToDecimal(NotaFiscalEntrada.vlSeguro),
                    Convert.ToDecimal(NotaFiscalEntrada.vlOutrasDespesas),
                    Convert.ToDecimal(NotaFiscalEntrada.vlTotalItens),
                    Convert.ToDecimal(NotaFiscalEntrada.vlDesconto),
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

        public void Update(Models.NotaFiscalEntrada notaFiscalEntrada)
        {
            try
            {
                string sql = "UPDATE tbNotafiscalEntrada SET dtEmissao = '" + notaFiscalEntrada.dtEmissao + "',"
                             + " dtEntrada = '" + notaFiscalEntrada.dtEntrada + "',"
                             + " chaveNFe = '" + notaFiscalEntrada.chaveNFe + "',"
                             + " flSituacao = '" + notaFiscalEntrada.flSituacao + "',"
                             + " vlTotalNota = '" + notaFiscalEntrada.vlTotalNota + "',"
                             + " vlFrete = '" + notaFiscalEntrada.vlFrete + "',"
                             + " vlSeguro = '" + notaFiscalEntrada.vlSeguro + "',"
                             + " vlOutrasDespesas = '" + notaFiscalEntrada.vlOutrasDespesas + "',"
                             + " vlTotalItens = '" + notaFiscalEntrada.vlTotalItens + "',"
                             + " vlDesconto = '" + notaFiscalEntrada.vlDesconto + "',"
                             + " dtUltAlteracao = '" + DateTime.Now.ToString("dd/MM/yyyy") + "'"
                             + " WHERE idFornecedor = '" + notaFiscalEntrada.idFornecedor + "'"
                             + " AND nrModelo = '" + notaFiscalEntrada.nrModelo + "'"
                             + " AND nrSerie = '" + notaFiscalEntrada.nrSerie + "'"
                             + " AND nrNota = '" + notaFiscalEntrada.nrNota + "'";                             
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

        public NotaFiscalEntrada GetNotaFiscalEntrada(int? idFornecedor, string nrModelo, string nrSerie, string nrNota)
        {
            try
            {
                var model = new Models.NotaFiscalEntrada();

                if (idFornecedor != null && !String.IsNullOrEmpty(nrModelo) && 
                    !String.IsNullOrEmpty(nrSerie) && !String.IsNullOrEmpty(nrNota))
                {
                    OpenConnection();
                    var sql = this.Search(idFornecedor, nrModelo, nrSerie, nrNota);
                    SqlQuery = new SqlCommand(sql, con);
                    reader = SqlQuery.ExecuteReader();

                    while (reader.Read())
                    {
                        model.idFornecedor = Convert.ToInt32(reader["idFornecedor"]);
                        model.nrModelo = Convert.ToString(reader["nrModelo"]);
                        model.nrSerie = Convert.ToString(reader["nrSerie"]);
                        model.nrNota = Convert.ToString(reader["nrNota"]);
                        model.dtEmissao = Convert.ToDateTime(reader["dtEmissao"]);
                        model.dtEntrada = Convert.ToDateTime(reader["dtEntrada"]);
                        model.chaveNFe = Convert.ToString(reader["chaveNFe"]);
                        model.flSituacao = Convert.ToString(reader["flSituacao"]);
                        model.vlTotalNota = Convert.ToDecimal(reader["vlTotalNota"]);
                        model.vlFrete = Convert.ToDecimal(reader["vlFrete"]);
                        model.vlSeguro = Convert.ToDecimal(reader["vlSeguro"]);
                        model.vlOutrasDespesas = Convert.ToDecimal(reader["vlOutrasDespesas"]);
                        model.vlTotalItens = Convert.ToDecimal(reader["vlTotalItens"]);
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

        public void Delete(int? idFornecedor, string nrModelo, string nrSerie, string nrNota)
        {
            try
            {
                string sql = "DELETE FROM tbNotaFiscalEntrada WHERE idFornecedor = " + idFornecedor + " AND nrModelo = " + nrModelo +
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

        private string Search(int? id, string filter, string filter2, string filter3)
        {
            var sql = string.Empty;
            var swhere = string.Empty;
            if (id != null && !String.IsNullOrEmpty(filter) &&
                   !String.IsNullOrEmpty(filter2) && !String.IsNullOrEmpty(filter3))
            {
                swhere = " WHERE idFornecedor = " + id + " AND nrModelo = " + filter + " AND nrSerie = " + filter2 + " AND nrNota = " + filter3;
            }
            sql = @"
                    SELECT
                        tbNotaFiscalEntrada.idFornecedor AS idFornecedor,
                        tbNotaFiscalEntrada.nrModelo AS nrModelo,
                        tbNotaFiscalEntrada.nrSerie AS nrSerie,
                        tbNotaFiscalEntrada.nrNota AS nrNota,
                        tbFornecedores.nmFornecedor AS nmFornecedor,
                        tbNotaFiscalEntrada.dtEmissao AS dtEmissao,
                        tbNotaFiscalEntrada.dtEntrada AS dtEntrada,
                        tbNotaFiscalEntrada.chaveNFe AS chaveNFe,
                        tbNotaFiscalEntrada.flSituacao AS flSituacao,
                        tbNotaFiscalEntrada.vlTotalNota AS vlTotalNota,
                        tbNotaFiscalEntrada.vlFrete AS vlFrete,
                        tbNotaFiscalEntrada.vlSeguro AS vlSeguro,
                        tbNotaFiscalEntrada.vlOutrasDespesas AS vlOutrasDespesas,
                        tbNotaFiscalEntrada.vlTotalItens AS vlTotalItens,
                        tbNotaFiscalEntrada.vlDesconto AS vlDesconto,
                        tbNotaFiscalEntrada.dtCadastro AS dtCadastro,
                        tbNotaFiscalEntrada.dtUltAlteracao AS dtUltAlteracao
                    FROM tbNotaFiscalEntrada
                    INNER JOIN tbFornecedores ON tbNotaFiscalEntrada.idFornecedor = tbFornecedores.idFornecedor " + swhere;
            return sql;
        }
    }
}
