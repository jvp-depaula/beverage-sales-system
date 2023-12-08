using Sistema.Models;
using System.Data.SqlClient;
using System.Reflection;

namespace Sistema.DAO
{
    public class DAOContasPagar : DAO
    {
        public List<ContasPagar> GetContasPagar()
        {
            try
            {
                var sql = this.Search();
                OpenConnection();
                SqlQuery = new SqlCommand(sql, con);
                reader = SqlQuery.ExecuteReader();
                var list = new List<ContasPagar>();

                while (reader.Read())
                {
                    var contasPagar = new ContasPagar
                    {
                        idFornecedor = Convert.ToInt32(reader["idFornecedor"]),
                        nmFornecedor = Convert.ToString(reader["nmFornecedor"]),
                        nrModelo = Convert.ToString(reader["nrModelo"]),
                        nrSerie = Convert.ToString(reader["nrSerie"]),
                        nrNota = Convert.ToInt32(reader["nrNota"]),
                        nrParcela = Convert.ToInt32(reader["nrParcela"]),
                        dtEmissao = Convert.ToDateTime(reader["dtEmissao"]),
                        dtVencimento = Convert.ToDateTime(reader["dtVencimento"]),
                        vlParcela = Convert.ToDecimal(reader["vlParcela"]),
                        idFormaPgto = Convert.ToInt32(reader["idFormaPgto"]),
                        dsFormaPgto = Convert.ToString(reader["dsFormaPgto"]),
                        vlPago = Convert.ToDecimal(reader["vlPago"]),
                        dtPgto = Convert.ToDateTime(reader["dtPgto"]),
                        flSituacao = Convert.ToString(reader["flSituacao"]),
                        txJuros = Convert.ToDecimal(reader["txJuros"]),
                        txMulta = Convert.ToDecimal(reader["txMulta"]),
                        txDesconto = Convert.ToDecimal(reader["txDesconto"]),
                    };

                    list.Add(contasPagar);
                };

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

        private string Search()
        {
            var sql = String.Empty;
            sql = @"
                    SELECT
                        tbContasPagar.idFornecedor AS idFornecedor,
                        tbFornecedores.nmFornecedor AS nmFornecedor,
                        tbContasPagar.nrModelo AS nrModelo,
                        tbContasPagar.nrSerie AS nrSerie,
                        tbContasPagar.nrNota AS nrNota,
                        tbContasPagar.nrParcela AS nrParcela,
                        tbContasPagar.dtEmissao AS dtEmissao,
                        tbContasPagar.dtVencimento AS dtVencimento,
                        tbContasPagar.vlParcela AS vlParcela,
                        tbContasPagar.idFormaPgto AS idFormaPgto,
                        tbFormaPgto.dsFormaPgto AS dsFormaPgto,
                        tbContasPagar.vlPago AS vlPago,
                        tbContasPagar.dtPgto AS dtPgto,
                        tbContasPagar.flSituacao AS flSituacao,
                        tbContasPagar.txJuros AS txJuros,
                        tbContasPagar.txMulta AS txMulta,
                        tbContasPagar.txDesconto AS txDesconto
                    FROM tbContasPagar
                    INNER JOIN tbFornecedores ON tbContasPagar.idFornecedor = tbFornecedores.idFornecedor
                    INNER JOIN tbFormaPgto ON tbContasPagar.idFormaPgto = tbFormaPgto.idFormaPgto  ORDER BY nrParcela ASC;";
            return sql;
        }
    }
}
