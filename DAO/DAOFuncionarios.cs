using Sistema.Models;
using System.Data.SqlClient;

namespace Sistema.DAO
{
    public class DAOFuncionarios : DAO
    {
        public List<Funcionarios> GetFuncionarios()
        {
            try
            {
                var sql = this.Search(null, null);
                OpenConnection();
                SqlQuery = new SqlCommand(sql, con);
                reader = SqlQuery.ExecuteReader();
                var list = new List<Funcionarios>();

                while (reader.Read())
                {
                    var func = new Funcionarios
                    {
                        id = Convert.ToInt32(reader["idFuncionario"]),
                        nmFuncionario = Convert.ToString(reader["nmFuncionario"]),
                        dsApelido = Convert.ToString(reader["dsApelido"]),
                        nrCPF = Convert.ToString(reader["nrCPF"]),
                        nrRG = Convert.ToString(reader["nrRG"]),
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
                        dtUltAlteracao = Convert.ToDateTime(reader["dtUltAlteracao"])
                    };

                    list.Add(func);
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

        public void Insert(Models.Funcionarios func)
        {
            try
            {
                var sql = string.Format("INSERT INTO tbFuncionarios (nmFuncionario, dsApelido, nrCPF, nrRG, nrTelefoneCelular, nrTelefoneFixo, dsEmail," +
                                                                    "nrCEP, dsLogradouro, nrEndereco, dsBairro, dsComplemento, idCidade," +
                                                                    "dtNasc" + "dtCadastro, dtUltAlteracao) " +
                                                                    "VALUES ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', " +
                                                                    "'{9}', '{10}', '{11}', '{12}', '{13}', '{14}', '{15}')",
                    func.nmFuncionario,
                    func.dsApelido,
                    func.nrCPF,
                    func.nrRG,
                    func.nrTelefoneCelular,
                    func.nrTelefoneFixo,
                    func.dsEmail,
                    func.nrCEP,
                    func.dsLogradouro,
                    Convert.ToInt32(func.nrEndereco),
                    func.dsBairro,
                    func.dsComplemento,
                    Convert.ToInt32(func.idCidade),
                    Convert.ToDateTime(func.dtNasc),
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

        public void Update(Models.Funcionarios func)
        {
            try
            {
                string sql = "UPDATE tbFuncionarios SET nmFuncionario = '" +
                    func.nmFuncionario + "'," +
                    func.dsApelido + "'," +
                    " nrCPF = '" + func.nrCPF + "'," +
                    " nrRG = '" + func.nrRG + "'," +
                    " nrTelefoneCelular = '" + func.nrTelefoneCelular+ "'," +
                    " nrTelefoneFixo = '" + func.nrTelefoneFixo + "'," +
                    " dsEmail = '" + func.dsEmail + "'," +
                    " nrCEP = '" + func.nrCEP + "'," +
                    " dsLogradouro = '" + func.dsLogradouro + "'," +
                    " nrEndereco = '" + Convert.ToInt32(func.nrEndereco) + "'," +
                    " dsBairro = '" + func.dsBairro + "'," +
                    " dsComplemento = '" + func.dsComplemento + "'," +
                    " idCidade = '" + Convert.ToInt32(func.idCidade) + "'," +
                    " dtNasc = '" + Convert.ToDateTime(func.dtNasc) + "'," +
                    " dtUltAlteracao = '" + DateTime.Now.ToString("dd/MM/yyyy")
                    + "' WHERE idFuncionario = " + func.id;
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

        public Funcionarios GetFuncionario(int? idFuncionario)
        {
            try
            {
                var model = new Models.Funcionarios();
                if (idFuncionario != null)
                {
                    OpenConnection();
                    var sql = this.Search(idFuncionario, null);
                    SqlQuery = new SqlCommand(sql, con);
                    reader = SqlQuery.ExecuteReader();
                    while (reader.Read())
                    {
                        model.id = Convert.ToInt32(reader["idFuncionario"]);
                        model.nmFuncionario= Convert.ToString(reader["nmFuncionario"]);
                        model.dsApelido = Convert.ToString(reader["dsApelido"]);
                        model.nrCPF = Convert.ToString(reader["nrCPF"]);
                        model.nrRG = Convert.ToString(reader["nrRG"]);
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

        public void Delete(int? idFuncionario)
        {
            try
            {
                string sql = "DELETE FROM tbClientes WHERE idFuncionario = " + idFuncionario;
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
                swhere = " WHERE idFuncionario = " + id;
            }
            if (!string.IsNullOrEmpty(filter))
            {
                var filterQ = filter.Split(' ');
                foreach (var word in filterQ)
                {
                    swhere += " OR tbFuncionarios.nmFuncionario LIKE'%" + word + "%'";
                }
                swhere = " WHERE " + swhere.Remove(0, 3);
            }
            sql = @"
                    SELECT
                        idFuncionario AS idFuncionario,
                        nmFuncionario AS nmFuncionario,
                        dsApelido AS dsApelido,
                        nrCPF AS nrCPF,
                        nrRG AS nrRG,
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
                    FROM tbFuncionarios" + swhere;
            return sql;
        }
    }
}
