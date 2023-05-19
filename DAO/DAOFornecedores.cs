using Sistema.Models;
using System.Data.SqlClient;

namespace Sistema.DAO
{
    public class DAOFornecedores : DAO
    {
        public List<Fornecedores> GetFornecedores()
        {
            try
            {
                var sql = this.Search(null, null);
                OpenConnection();
                SqlQuery = new SqlCommand(sql, con);
                reader = SqlQuery.ExecuteReader();
                var list = new List<Fornecedores>();

                while (reader.Read())
                {
                    var fornecedor = new Fornecedores
                    {
                        id = Convert.ToInt32(reader["idFornecedor"]),
                        nmFornecedor = Convert.ToString(reader["nmFornecedor"]),
                        nmFantasia = Convert.ToString(reader["nmFantasia"]),
                        nrCNPJ = Convert.ToString(reader["nrCNPJ"]),
                        nrIE = Convert.ToString(reader["nrIE"]),
                        nrTelefoneCelular = Convert.ToString(reader["nrTelefoneCelular"]),
                        nrTelefoneFixo = Convert.ToString(reader["nrTelefoneFixo"]),
                        dsEmail = Convert.ToString(reader["dsEmail"]),
                        nrCEP = Convert.ToString(reader["nrCEP"]),
                        dsLogradouro = Convert.ToString(reader["dsLogradouro"]),
                        nrEndereco = Convert.ToString(reader["nrEndereco"]),
                        dsBairro = Convert.ToString(reader["dsBairro"]),
                        idCidade = Convert.ToInt32(reader["idCidade"]),
                        dtCadastro = Convert.ToDateTime(reader["dtCadastro"]),
                        dtUltAlteracao = Convert.ToDateTime(reader["dtUltAlteracao"])
                    };

                    list.Add(fornecedor);
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

        public void Insert(Models.Fornecedores fornecedor)
        {
            try
            {
                var sql = string.Format("INSERT INTO tbFornecedores (nrFornecedor, nmFantasia, nrCNPJ, nrIE, nrTelefoneCelular, nrTelefoneFixo, " +
                                                                    "dsEmail, nrCEP, dsLogradouro, nrEndereco, dsBairro, dsComplemento, idCidade," +
                                                                    "dtCadastro, dtUltAlteracao) " +
                                                                    "VALUES ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', " +
                                                                    "'{9}', '{10}', '{11}', '{12}', '{13}', '{14}')",
                    fornecedor.nmFornecedor,
                    fornecedor.nmFantasia,
                    fornecedor.nrCNPJ,
                    fornecedor.nrIE,
                    fornecedor.nrTelefoneCelular,
                    fornecedor.nrTelefoneFixo,
                    fornecedor.dsEmail,
                    fornecedor.nrCEP,
                    fornecedor.dsLogradouro,
                    Convert.ToInt32(fornecedor.nrEndereco),
                    fornecedor.dsBairro,
                    fornecedor.dsComplemento,
                    Convert.ToInt32(fornecedor.idCidade),
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

        public void Update(Models.Fornecedores fornecedor)
        {
            try
            {
                string sql = "UPDATE tbFornecedores SET nmFornecedor = '" +
                    fornecedor.nmFornecedor + "'," +
                    " nmFantasia = '" + fornecedor.nmFantasia + "'," +
                    " nrCNPJ = '" + fornecedor.nrCNPJ + "'," +
                    " nrIE = '" + fornecedor.nrIE + "'," +
                    " nrTelefoneCelular = '" + fornecedor.nrTelefoneCelular + "'," +
                    " nrTelefoneFixo = '" + fornecedor.nrTelefoneFixo + "'," +
                    " dsEmail = '" + fornecedor.dsEmail + "'," +
                    " nrCEP = '" + fornecedor.nrCEP + "'," +
                    " dsLogradouro = '" + fornecedor.dsLogradouro + "'," +
                    " nrEndereco = '" + Convert.ToInt32(fornecedor.nrEndereco) + "'," +
                    " dsBairro = '" + fornecedor.dsBairro + "'," +
                    " dsComplemento = '" + fornecedor.dsComplemento + "'," +
                    " idCidade = '" + Convert.ToInt32(fornecedor.idCidade) + "'," +
                    " dtUltAlteracao = '" + DateTime.Now.ToString("dd/MM/yyyy")
                    + "' WHERE idFornecedor = " + fornecedor.id;
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

        public Fornecedores GetFornecedor(int? idFornecedor)
        {
            try
            {
                var model = new Models.Fornecedores();
                if (idFornecedor != null)
                {
                    OpenConnection();
                    var sql = this.Search(idFornecedor, null);
                    SqlQuery = new SqlCommand(sql, con);
                    reader = SqlQuery.ExecuteReader();
                    while (reader.Read())
                    {
                        model.id = Convert.ToInt32(reader["idFornecedor"]);
                        model.nmFornecedor = Convert.ToString(reader["nmFornecedor"]);
                        model.nmFantasia = Convert.ToString(reader["nmFantasia"]);
                        model.nrCNPJ = Convert.ToString(reader["nrCNPJ"]);
                        model.nrIE = Convert.ToString(reader["nrIE"]);
                        model.nrTelefoneCelular = Convert.ToString(reader["nrTelefoneCelular"]);
                        model.nrTelefoneFixo = Convert.ToString(reader["nrTelefoneFixo"]);
                        model.dsEmail = Convert.ToString(reader["dsEmail"]);
                        model.nrCEP = Convert.ToString(reader["nrCEP"]);
                        model.dsLogradouro = Convert.ToString(reader["dsLogradouro"]);
                        model.nrEndereco = Convert.ToString(reader["nrEndereco"]);
                        model.dsBairro = Convert.ToString(reader["dsBairro"]);
                        model.dsComplemento = Convert.ToString(reader["dsComplemento"]);
                        model.idCidade = Convert.ToInt32(reader["idCidade"]);
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

        public void Delete(int? idFornecedor)
        {
            try
            {
                string sql = "DELETE FROM tbFornecedores WHERE idFornecedor = " + idFornecedor;
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
                swhere = " WHERE idFornecedor = " + id;
            }
            if (!string.IsNullOrEmpty(filter))
            {
                var filterQ = filter.Split(' ');
                foreach (var word in filterQ)
                {
                    swhere += " OR tbFornecedores.nmFornecedor LIKE'%" + word + "%'";
                }
                swhere = " WHERE " + swhere.Remove(0, 3);
            }
            sql = @"
                    SELECT
                        idFornecedor AS idFornecedor,
                        nmFornecedor AS nmFornecedor,
                        nmFantasia AS nmFantasia,
                        nrCNPJ AS nrCNPJ,
                        nmIE AS nmIE,
                        nrTelefoneCelular AS nrTelefoneCelular,
                        nrTelefoneFixo AS nrTelefoneFixo,
                        dsEmail AS dsEmail,
                        nrCEP AS nrCEP,
                        dsLogradouro AS dsLogradouro,
                        nrEndereco AS nrEndereco,
                        dsBairro AS dsBairro,
                        dsComplemento AS dsComplemento,
                        idCidade AS idCidade,
                        dtCadastro AS dtCadastro,
                        dtUltAlteracao AS dtUltAlteracao
                    FROM tbFornecedores" + swhere;
            return sql;
        }
    }
}
