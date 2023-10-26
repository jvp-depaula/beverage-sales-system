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
                                                                    "dtNasc," + "dtCadastro, dtUltAlteracao) " +
                                                                    "VALUES ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', " +
                                                                    "'{9}', '{10}', '{11}', '{12}', '{13}', '{14}', '{15}')",
                    func.nmFuncionario,
                    func.dsApelido,
                    Util.Util.Unmask(func.nrCPF),
                    Util.Util.Unmask(func.nrRG),
                    Util.Util.Unmask(func.nrTelefoneCelular),
                    Util.Util.Unmask(func.nrTelefoneFixo),
                    func.dsEmail,
                    Util.Util.Unmask(func.nrCEP),
                    func.dsLogradouro,
                    Convert.ToInt32(func.nrEndereco),
                    func.dsBairro,
                    func.dsComplemento,
                    Convert.ToInt32(func.idCidade),
                    Convert.ToDateTime(func.dtNasc),
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

        public void Update(Models.Funcionarios func)
        {
            try
            {
                var sql = String.Format("UPDATE tbFuncionarios SET nmFuncionario = '{0}', dsApelido = '{1}', nrCPF = '{2}', nrRG = '{3}'," +
                    "nrTelefoneCelular = '{4}', nrTelefoneFixo = '{5}', dsEmail = '{6}'," +
                    "nrCEP = '{7}', dsLogradouro = '{8}', nrEndereco = '{9}', dsBairro = '{10}', dsComplemento = '{11}', " +
                    "idCidade = '{12}', dtNasc = '{13}', dtUltAlteracao = '{14}' WHERE idFuncionario = '{15}'",
                    func.nmFuncionario,
                    func.dsApelido,
                    Util.Util.Unmask(func.nrCPF),
                    Util.Util.Unmask(func.nrRG),
                    Util.Util.Unmask(func.nrTelefoneCelular),
                    Util.Util.Unmask(func.nrTelefoneFixo),
                    func.dsEmail,
                    Util.Util.Unmask(func.nrCEP),
                    func.dsLogradouro,
                    func.nrEndereco,
                    func.dsBairro,
                    func.dsComplemento,
                    func.idCidade,
                    Util.Util.FormatDate(func.dtNasc),
                    Util.Util.FormatDate(DateTime.Now),
                    func.id);
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
                        model.nmFuncionario = Convert.ToString(reader["nmFuncionario"]);
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
                string sql = "DELETE FROM tbFuncionarios WHERE idFuncionario = " + idFuncionario;
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
                        tbFuncionarios.idFuncionario AS idFuncionario,
                        tbFuncionarios.nmFuncionario AS nmFuncionario,
                        tbFuncionarios.dsApelido AS dsApelido,
                        tbFuncionarios.nrCPF AS nrCPF,
                        tbFuncionarios.nrRG AS nrRG,
                        tbFuncionarios.nrTelefoneCelular AS nrTelefoneCelular,
                        tbFuncionarios.nrTelefoneFixo AS nrTelefoneFixo,
                        tbFuncionarios.dsEmail AS dsEmail,
                        tbFuncionarios.nrCEP AS nrCEP,
                        tbFuncionarios.dsLogradouro AS dsLogradouro,
                        tbFuncionarios.nrEndereco AS nrEndereco,
                        tbFuncionarios.dsBairro AS dsBairro,
                        tbFuncionarios.dsComplemento AS dsComplemento,
                        tbFuncionarios.idCidade AS idCidade,
                        tbCidades.nmCidade AS nmCidade,
                        tbFuncionarios.dtNasc AS dtNasc,
                        tbFuncionarios.dtCadastro AS dtCadastro,
                        tbFuncionarios.dtUltAlteracao AS dtUltAlteracao
                    FROM tbFuncionarios
                    INNER JOIN tbCidades ON tbFuncionarios.idCidade = tbCidades.idCidade" + swhere;
            return sql;
        }
    }
}
