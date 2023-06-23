using Sistema.Models;
using System.Data.SqlClient;

namespace Sistema.DAO
{
    public class DAOClientes : DAO
    {
        public List<Clientes> GetClientes()
        {
            try
            {
                var sql = this.Search(null, null);
                OpenConnection();
                SqlQuery = new SqlCommand(sql, con);
                reader = SqlQuery.ExecuteReader();
                var list = new List<Clientes>();

                while (reader.Read())
                {
                    var cliente = new Clientes
                    {
                        id = Convert.ToInt32(reader["idCliente"]),
                        flTipo = Convert.ToString(reader["flTipo"]),
                        nmCliente = Convert.ToString(reader["nmCliente"]),
                        nmFantasia = Convert.ToString(reader["nmFantasia"]),
                        nrCPFCNPJ = Convert.ToString(reader["nrCPFCNPJ"]),                        
                        nrRG_IE = Convert.ToString(reader["nrRG_IE"]),
                        nrTelefoneCelular = Convert.ToString(reader["nrTelefoneCelular"]),
                        nrTelefoneFixo = Convert.ToString(reader["nrTelefoneFixo"]),
                        dsEmail = Convert.ToString(reader["dsEmail"]),
                        nrCEP = Convert.ToString(reader["nrCEP"]),
                        dsLogradouro = Convert.ToString(reader["dsLogradouro"]),
                        nrEndereco = Convert.ToString(reader["nrEndereco"]),
                        dsBairro = Convert.ToString(reader["dsBairro"]),
                        dsComplemento = Convert.ToString(reader["dsComplemento"]),
                        idCidade = Convert.ToInt32(reader["idCidade"]),
                        dtNasc = Convert.ToDateTime(reader["dtNasc"]),
                        dtCadastro = Convert.ToDateTime(reader["dtCadastro"]),
                        dtUltAlteracao = Convert.ToDateTime(reader["dtUltAlteracao"]),                        
                    };

                    list.Add(cliente);
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

        public void Insert(Models.Clientes cliente)
        {
            try
            {
                var sql = string.Format("INSERT INTO tbClientes (nmCliente, flTipo, nmFantasia, nrCPFCNPJ, nrRG_IE, nrTelefoneCelular, nrTelefoneFixo, dsEmail," +
                                                                "nrCEP, dsLogradouro, nrEndereco, dsBairro, dsComplemento, idCidade, dtNasc," +
                                                                "dtCadastro, dtUltAlteracao) " +
                                                                "VALUES ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', " +
                                                                "'{9}', '{10}', '{11}', '{12}', '{13}', '{14}', '{15}', '{16}')",
                    cliente.nmCliente,
                    cliente.flTipo,
                    cliente.nmFantasia,
                    Util.Util.Unmask(cliente.nrCPFCNPJ),
                    Util.Util.Unmask(cliente.nrRG_IE),
                    Util.Util.Unmask(cliente.nrTelefoneCelular),
                    Util.Util.Unmask(cliente.nrTelefoneFixo),
                    cliente.dsEmail,
                    Util.Util.Unmask(cliente.nrCEP),
                    cliente.dsLogradouro,
                    Convert.ToInt32(cliente.nrEndereco),
                    cliente.dsBairro,
                    cliente.dsComplemento,
                    Convert.ToInt32(cliente.idCidade),
                    Convert.ToDateTime(cliente.dtNasc),
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

        public void Update(Models.Clientes cliente)
        {
            try
            {
                string sql = "UPDATE tbClientes SET" +
                    " nmCliente = '" + cliente.nmCliente + "'," +
                    " flTipo = '" + cliente.flTipo + "'," +
                    " nmFantasia = '" + cliente.nmFantasia + "'," +                    
                    " nrCPFCNPJ = '" + Util.Util.Unmask(cliente.nrCPFCNPJ) + "'," +
                    " nrRG_IE = '" + Util.Util.Unmask(cliente.nrRG_IE) + "'," +
                    " nrTelefoneCelular = '" + Util.Util.Unmask(cliente.nrTelefoneCelular) + "'," +
                    " nrTelefoneFixo = '" + Util.Util.Unmask(cliente.nrTelefoneFixo) + "'," +
                    " dsEmail = '" + cliente.dsEmail + "'," +
                    " nrCEP = '" + Util.Util.Unmask(cliente.nrCEP) + "'," +
                    " dsLogradouro = '" + cliente.dsLogradouro + "'," +
                    " nrEndereco = '" + Convert.ToInt32(cliente.nrEndereco) + "'," +
                    " dsBairro = '" + cliente.dsBairro + "'," +
                    " dsComplemento = '" + cliente.dsComplemento + "'," +
                    " idCidade = '" + Convert.ToInt32(cliente.idCidade) + "'," +
                    " dtNasc = '" + Util.Util.FormatDate(cliente.dtNasc) + "'," +
                    " dtUltAlteracao = '" + DateTime.Now.ToString("dd/MM/yyyy")
                    + "' WHERE idCliente = " + cliente.id;
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

        public Clientes GetCliente(int? idCliente)
        {
            try
            {
                var model = new Models.Clientes();
                if (idCliente != null)
                {
                    OpenConnection();
                    var sql = this.Search(idCliente, null);
                    SqlQuery = new SqlCommand(sql, con);
                    reader = SqlQuery.ExecuteReader();
                    while (reader.Read())
                    {
                        model.id = Convert.ToInt32(reader["idCliente"]);
                        model.flTipo = Convert.ToString(reader["flTipo"]);
                        model.nmCliente = Convert.ToString(reader["nmCliente"]);
                        model.nmFantasia = Convert.ToString(reader["nmFantasia"]);
                        model.nrCPFCNPJ = Convert.ToString(reader["nrCPFCNPJ"]);
                        model.nrRG_IE = Convert.ToString(reader["nrRG_IE"]);
                        model.nrTelefoneCelular = Convert.ToString(reader["nrTelefoneCelular"]);
                        model.nrTelefoneFixo = Convert.ToString(reader["nrTelefoneFixo"]);
                        model.dsEmail = Convert.ToString(reader["dsEmail"]);
                        model.nrCEP = Convert.ToString(reader["nrCEP"]);
                        model.dsLogradouro = Convert.ToString(reader["dsLogradouro"]);
                        model.nrEndereco = Convert.ToString(reader["nrEndereco"]);
                        model.dsBairro = Convert.ToString(reader["dsBairro"]);
                        model.dsComplemento = Convert.ToString(reader["dsComplemento"]);
                        model.idCidade = Convert.ToInt32(reader["idCidade"]);
                        model.dtNasc = Convert.ToDateTime(reader["dtNasc"]);
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

        public void Delete(int? idCliente)
        {
            try
            {
                string sql = "DELETE FROM tbClientes WHERE idCliente = " + idCliente;
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
                swhere = " WHERE idCliente = " + id;
            }
            if (!string.IsNullOrEmpty(filter))
            {
                var filterQ = filter.Split(' ');
                foreach (var word in filterQ)
                {
                    swhere += " OR tbClientes.nmCliente LIKE'%" + word + "%'";
                }
                swhere = " WHERE " + swhere.Remove(0, 3);
            }
            sql = @"
                    SELECT
                        idCliente AS idCliente,
                        flTipo AS flTipo,
                        nmCliente AS nmCliente,
                        nmFantasia AS nmFantasia,
                        nrCPFCNPJ AS nrCPFCNPJ,
                        nrRG_IE AS nrRG_IE,
                        nrTelefoneCelular AS nrTelefoneCelular,
                        nrTelefoneFixo AS nrTelefoneFixo,
                        dsEmail AS dsEmail,
                        nrCEP AS nrCEP,
                        dsLogradouro AS dsLogradouro,
                        nrEndereco AS nrEndereco,
                        dsBairro AS dsBairro,
                        dsComplemento AS dsComplemento,
                        idCidade AS idCidade,
                        dtNasc AS dtNasc,
                        dtCadastro AS dtCadastro,
                        dtUltAlteracao AS dtUltAlteracao
                    FROM tbClientes" + swhere;
            return sql;
        }
    }
}
