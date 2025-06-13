using Sistema.Models;
using System.Data.SqlClient;

namespace Sistema.DAO
{
    public class DAOContasReceber : DAO
    {
        public List<ContasReceber> GetContasReceber()
        {
            try
            {
                var sql = this.Search();
                OpenConnection();
                SqlQuery = new SqlCommand(sql, con);
                reader = SqlQuery.ExecuteReader();
                var list = new List<ContasReceber>();

                while (reader.Read())
                {
                    var ContasReceber = new ContasReceber
                    {
                        idContasReceber = Convert.ToInt32(reader["idContaReceber"]),
                        nrParcela = Convert.ToInt32(reader["nrParcela"]),
                        dtVencimento = Convert.ToDateTime(reader["dtVencimento"]),
                        vlParcela = Convert.ToDecimal(reader["vlParcela"]),
                        idFormaPgto = Convert.ToInt32(reader["idFormaPgto"]),
                        dsFormaPgto = Convert.ToString(reader["dsFormaPgto"]),
                        idVenda = Convert.ToInt32(reader["idVenda"]),
                        flSituacao = Convert.ToString(reader["flSituacao"]),
                        txJuros = Convert.ToDecimal(reader["txJuros"]),
                        txMulta = Convert.ToDecimal(reader["txMulta"]),
                        txDesconto = Convert.ToDecimal(reader["txDesconto"]),
                        idCliente = Convert.ToInt32(reader["idCliente"]),
                        nmCliente = Convert.ToString(reader["nmCliente"])
                    };

                    list.Add(ContasReceber);
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
                        tbContasReceber.idContaReceber AS idContaReceber,
                        tbContasReceber.idCliente AS idCliente,
                        tbClientes.nmCliente AS nmCliente,
                        tbContasReceber.nrParcela AS nrParcela,
                        tbContasReceber.dtVencimento AS dtVencimento,
                        tbContasReceber.vlParcela AS vlParcela,
                        tbContasReceber.idFormaPgto AS idFormaPgto,
                        tbFormaPgto.dsFormaPgto AS dsFormaPgto,
                        tbContasReceber.idVenda AS idVenda,
                        tbContasReceber.flSituacao AS flSituacao,
                        tbContasReceber.txJuros AS txJuros,
                        tbContasReceber.txMulta AS txMulta,
                        tbContasReceber.txDesconto AS txDesconto
                    FROM tbContasReceber
                    INNER JOIN tbClientes ON tbContasReceber.idCliente = tbClientes.idCliente
                    INNER JOIN tbFormaPgto ON tbContasReceber.idFormaPgto = tbFormaPgto.idFormaPgto ORDER BY nrParcela ASC;";
            return sql;
        }
    }
}
