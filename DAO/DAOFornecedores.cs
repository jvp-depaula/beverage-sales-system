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
                        idCondicaoPgto = Convert.ToInt32(reader["idCondicaoPgto"]),
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
                var sql = string.Format("INSERT INTO tbFornecedores (nmFornecedor, nmFantasia, nrCNPJ, nrIE, nrTelefoneCelular, nrTelefoneFixo, " +
                                                                    "dsEmail, nrCEP, dsLogradouro, nrEndereco, dsBairro, dsComplemento, idCidade," +
                                                                    "idCondicaoPgto, dtCadastro, dtUltAlteracao) " +
                                                                    "VALUES ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', " +
                                                                    "'{9}', '{10}', '{11}', '{12}', '{13}', '{14}', '{15}')",
                    fornecedor.nmFornecedor,
                    fornecedor.nmFantasia,
                    Util.Util.Unmask(fornecedor.nrCNPJ),
                    Util.Util.Unmask(fornecedor.nrIE),
                    Util.Util.Unmask(fornecedor.nrTelefoneCelular),
                    Util.Util.Unmask(fornecedor.nrTelefoneFixo),
                    fornecedor.dsEmail,
                    Util.Util.Unmask(fornecedor.nrCEP),
                    fornecedor.dsLogradouro,
                    Convert.ToInt32(fornecedor.nrEndereco),
                    fornecedor.dsBairro,
                    fornecedor.dsComplemento,
                    Convert.ToInt32(fornecedor.idCidade),
                    Convert.ToInt32(fornecedor.idCondicaoPgto),
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

        public void Update(Models.Fornecedores fornecedor)
        {
            try
            {
                var sql = String.Format("UPDATE tbFornecedores SET nmFornecedor = '{0}', " +
                    "nmFantasia = '{1}', nrCNPJ = '{2}', nrIE = '{3}', nrTelefoneCelular = '{4}'," +
                    "nrTelefoneFixo = '{5}', dsEmail = '{6}', nrCEP = '{7}', dsLogradouro = '{8}', nrEndereco = '{9}'," +
                    "dsBairro = '{10}', dsComplemento = '{11}', idCidade = '{12}', idCondicaoPgto = '{13}', dtUltAlteracao = '{14}'" +
                    "WHERE idFornecedor = '{15}'",
                    fornecedor.nmFornecedor,
                    fornecedor.nmFantasia,
                    Util.Util.Unmask(fornecedor.nrCNPJ),
                    Util.Util.Unmask(fornecedor.nrIE),
                    Util.Util.Unmask(fornecedor.nrTelefoneCelular),
                    Util.Util.Unmask(fornecedor.nrTelefoneFixo),
                    fornecedor.dsEmail,
                    Util.Util.Unmask(fornecedor.nrCEP),
                    fornecedor.dsLogradouro,
                    fornecedor.nrEndereco,
                    fornecedor.dsBairro,
                    fornecedor.dsComplemento,
                    fornecedor.idCidade,
                    fornecedor.idCondicaoPgto,
                    Util.Util.FormatDate(DateTime.Now),
                    fornecedor.id);
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
                        model.idCondicaoPgto = Convert.ToInt32(reader["idCondicaoPgto"]);
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
                        tbFornecedores.idFornecedor AS idFornecedor,
                        tbFornecedores.nmFornecedor AS nmFornecedor,
                        tbFornecedores.nmFantasia AS nmFantasia,
                        tbFornecedores.nrCNPJ AS nrCNPJ,
                        tbFornecedores.nrIE AS nrIE,
                        tbFornecedores.nrTelefoneCelular AS nrTelefoneCelular,
                        tbFornecedores.nrTelefoneFixo AS nrTelefoneFixo,
                        tbFornecedores.dsEmail AS dsEmail,
                        tbFornecedores.nrCEP AS nrCEP,
                        tbFornecedores.dsLogradouro AS dsLogradouro,
                        tbFornecedores.nrEndereco AS nrEndereco,
                        tbFornecedores.dsBairro AS dsBairro,
                        tbFornecedores.dsComplemento AS dsComplemento,
                        tbFornecedores.idCidade AS idCidade,
                        tbFornecedores.idCondicaoPgto AS idCondicaoPgto,                        
                        tbCondicaoPgto.dsCondicaoPgto as dsCondicaoPgto,
                        tbFornecedores.dtCadastro AS dtCadastro,
                        tbFornecedores.dtUltAlteracao AS dtUltAlteracao
                    FROM tbFornecedores
                    INNER JOIN tbCidades ON tbFornecedores.idCidade = tbCidades.idcidade 
                    INNER JOIN tbCondicaoPgto ON tbFornecedores.idCondicaoPgto = tbCondicaoPgto.idCondicaoPgto " + swhere;
            return sql;
        }
    }
}
