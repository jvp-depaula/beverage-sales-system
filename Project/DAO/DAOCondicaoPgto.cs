﻿using Sistema.Models;
using System.Collections.Generic;
using System.Data.SqlClient;
using static Sistema.Models.CondicaoPgto;

namespace Sistema.DAO
{
    public class DAOCondicaoPgto : DAO
    {
        public List<CondicaoPgto> GetCondicoesPgto()
        {
            try
            {
                var sql = this.Search(null, null);
                OpenConnection();
                SqlQuery = new SqlCommand(sql, con);
                reader = SqlQuery.ExecuteReader();
                var list = new List<CondicaoPgto>();

                while (reader.Read())
                {
                    var condicaoPgto = new CondicaoPgto
                    {
                        idCondicaoPgto = Convert.ToInt32(reader["idCondicaoPgto"]),
                        dsCondicaoPgto = Convert.ToString(reader["dsCondicaoPgto"]),
                        txJuros = Convert.ToDecimal(reader["txJuros"]),
                        txMulta = Convert.ToDecimal(reader["txMulta"]),
                        txDesconto = Convert.ToDecimal(reader["txDesconto"]),
                        dtCadastro = Convert.ToDateTime(reader["dtCadastro"]),
                        dtUltAlteracao = Convert.ToDateTime(reader["dtUltAlteracao"])
                    };

                    list.Add(condicaoPgto);
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

        public List<CondicaoPgto> GetParcelas(int idCondicaoPgto)
        {
            try
            {
                var sql = this.SearchParcelas(idCondicaoPgto);
                OpenConnection();
                SqlQuery = new SqlCommand(sql, con);
                reader = SqlQuery.ExecuteReader();
                var list = new List<CondicaoPgto>();

                while (reader.Read())
                {
                    var parcela = new CondicaoPgto
                    {
                        idCondicaoPgto = Convert.ToInt32(reader["idCondicaoPgto"]),
                        dsCondicaoPgto = Convert.ToString(reader["dsCondicaoPgto"]),
                        idFormaPgto = Convert.ToInt32(reader["idFormaPgto"]),
                        dsFormaPgto = Convert.ToString(reader["dsFormaPgto"]),
                        nrParcela = Convert.ToInt32(reader["nrParcela"]),
                        qtDias = Convert.ToInt32(reader["qtDias"]),
                        txPercentual = Convert.ToDecimal(reader["txPercentual"])
                    };

                    list.Add(parcela);
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

        public void Insert(Models.CondicaoPgto condicaoPgto)
        {
            try
            {
                var sql = string.Format("INSERT INTO tbCondicaoPgto (dsCondicaoPgto, txMulta, txJuros, txDesconto, dtCadastro, dtUltAlteracao) VALUES ('{0}', {1}, {2}, {3}, '{4}', '{5}'); SELECT SCOPE_IDENTITY()",
                    condicaoPgto.dsCondicaoPgto,
                    this.FormatDecimal(condicaoPgto.txDesconto),
                    this.FormatDecimal(condicaoPgto.txJuros),
                    this.FormatDecimal(condicaoPgto.txMulta),
                    Util.Util.FormatDate(DateTime.Now),
                    Util.Util.FormatDate(DateTime.Now)
                );
                
                var sqlParcela = "INSERT INTO tbParcelas (idCondicaoPgto, idFormaPgto, nrParcela, qtDias, txPercentual) VALUES ({0}, {1}, {2}, {3}, {4})";
                   
                using (con)
                {
                    OpenConnection();

                    SqlTransaction sqlTrans = con.BeginTransaction();
                    SqlCommand command = con.CreateCommand();
                    command.Transaction = sqlTrans;
                    try
                    {
                        command.CommandText = sql;
                        var idCondicaoPgto = Convert.ToInt32(command.ExecuteScalar());
                        foreach (var item in condicaoPgto.ListParcelas)
                        {
                            var Item = string.Format(sqlParcela, idCondicaoPgto, item.idFormaPgto, item.nrParcela, item.qtDias, item.txPercentual.ToString().Replace(",", "."));
                            command.CommandText = Item;
                            command.ExecuteNonQuery();
                        }
                        sqlTrans.Commit();
                    }
                    catch (Exception ex)
                    {
                        sqlTrans.Rollback();
                        throw new Exception(ex.Message);
                    }
                    finally
                    {
                        con.Close();
                    }
                }
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

        public void Update(Models.CondicaoPgto condicaoPgto)
        {
            try
            {
                var sqlParcelasRemove = "DELETE FROM tbParcelas WHERE idCondicaoPgto = " + condicaoPgto.idCondicaoPgto;

                var sql = String.Format("UPDATE tbCondicaoPgto SET dsCondicaoPgto = '{0}', txMulta = '{1}', txDesconto = '{2}', txJuros = '{3}', dtUltAlteracao = '{4}' WHERE idCondicaoPgto = '{5}'",
                    condicaoPgto.dsCondicaoPgto,
                    condicaoPgto.txMulta,
                    condicaoPgto.txDesconto,
                    condicaoPgto.txJuros,
                    Util.Util.FormatDate(DateTime.Now),
                    condicaoPgto.idCondicaoPgto
                );

                var sqlParcela = "INSERT INTO tbParcelas (idCondicaoPgto, idFormaPgto, nrParcela, qtDias, txPercentual) VALUES ({0}, {1}, {2}, {3}, {4})";


                using (con)
                {
                    OpenConnection();

                    SqlTransaction sqlTrans = con.BeginTransaction();
                    SqlCommand command = con.CreateCommand();
                    command.Transaction = sqlTrans;
                    try
                    {
                        command.CommandText = sqlParcelasRemove;
                        command.ExecuteNonQuery();
                        command.CommandText = sql;
                        command.ExecuteNonQuery();

                        foreach (var item in condicaoPgto.ListParcelas)
                        {
                            var Item = string.Format(sqlParcela, condicaoPgto.idCondicaoPgto, item.idFormaPgto, item.nrParcela, item.qtDias, item.txPercentual.ToString().Replace(",", "."));
                            command.CommandText = Item;
                            command.ExecuteNonQuery();
                        }
                        sqlTrans.Commit();
                    }
                    catch (Exception ex)
                    {
                        sqlTrans.Rollback();
                        throw new Exception(ex.Message);
                    }
                    finally
                    {
                        con.Close();
                    }
                }

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

        public CondicaoPgto GetCondicaoPgto(int? idcondicaoPgto)
        {
            try
            {
                var model = new Models.CondicaoPgto();

                if (idcondicaoPgto != null)
                {
                    OpenConnection();
                    var sql = this.Search(idcondicaoPgto, null);
                    var sqlParcelas = this.SearchParcelas(idcondicaoPgto);
                    var lista = new List<Parcelas>();
                    SqlQuery = new SqlCommand(sql + sqlParcelas, con);
                    reader = SqlQuery.ExecuteReader();

                    while (reader.Read())
                    {
                        model.idCondicaoPgto = Convert.ToInt32(reader["idCondicaoPgto"]);
                        model.dsCondicaoPgto = Convert.ToString(reader["dsCondicaoPgto"]);
                        model.txMulta = Convert.ToDecimal(reader["txMulta"]);
                        model.txJuros = Convert.ToDecimal(reader["txJuros"]);
                        model.txDesconto = Convert.ToDecimal(reader["txDesconto"]);
                        model.dtCadastro = Convert.ToDateTime(reader["dtCadastro"]);
                        model.dtUltAlteracao = Convert.ToDateTime(reader["dtUltAlteracao"]);
                    };

                    if (reader.NextResult())
                    {
                        while (reader.Read())
                        {
                            var item = new Parcelas()
                            {
                                idCondicaoPgto = Convert.ToInt32(reader["idCondicaoPgto"]),
                                dsCondicaoPgto = Convert.ToString(reader["dsCondicaoPgto"]),
                                idFormaPgto = Convert.ToInt32(reader["idFormaPgto"]),
                                dsFormaPgto = Convert.ToString(reader["dsFormaPgto"]),
                                nrParcela = Convert.ToInt16(reader["nrParcela"]),
                                qtDias = Convert.ToInt16(reader["qtDias"]),
                                txPercentual = Convert.ToDecimal(reader["txPercentual"])
                            };

                            lista.Add(item);
                        }
                    }
                    model.ListParcelas = lista;
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

        public void Delete(int? idcondicaoPgto)
        {
            try
            {
                var sqlParcelasRemove = "DELETE FROM tbParcelas WHERE idCondicaoPgto = " + idcondicaoPgto;
                string sql = "DELETE FROM tbCondicaoPgto WHERE idCondicaoPgto = " + idcondicaoPgto;
                using (con)
                {
                    OpenConnection();

                    SqlTransaction sqlTrans = con.BeginTransaction();
                    SqlCommand command = con.CreateCommand();
                    command.Transaction = sqlTrans;
                    try
                    {
                        command.CommandText = sqlParcelasRemove;
                        command.ExecuteNonQuery();
                        command.CommandText = sql;
                        command.ExecuteNonQuery();
                        sqlTrans.Commit();
                    }
                    catch (Exception ex)
                    {
                        sqlTrans.Rollback();
                        throw new Exception(ex.Message);
                    }
                }
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
                swhere = " WHERE idcondicaoPgto = " + id;
            }
            if (!string.IsNullOrEmpty(filter))
            {
                var filterQ = filter.Split(' ');
                foreach (var word in filterQ)
                {
                    swhere += " OR tbCondicaoPgto.dsCondicaoPgto LIKE '%" + word + "%'";
                }
                swhere = " WHERE " + swhere.Remove(0, 3);
            }
            sql = @"
                    SELECT
                        tbCondicaoPgto.idcondicaoPgto AS idcondicaoPgto,
                        tbCondicaoPgto.dsCondicaoPgto AS dsCondicaoPgto,
                        tbCondicaoPgto.txMulta AS txMulta,
                        tbCondicaoPgto.txDesconto AS txDesconto,
                        tbCondicaoPgto.txJuros AS txJuros,
                        tbCondicaoPgto.dtCadastro AS dtCadastro,
                        tbCondicaoPgto.dtUltAlteracao AS dtUltAlteracao
                    FROM tbCondicaoPgto" + swhere;
            return sql;
        }

        private string SearchParcelas(int? id)
        {
            var sql = string.Empty;

            sql = @"
                    SELECT
                        tbParcelas.idCondicaoPgto AS idCondicaoPgto,
                        tbCondicaoPgto.dsCondicaoPgto AS dsCondicaoPgto,
                        tbParcelas.idFormaPgto AS idFormaPgto,
                        tbFormaPgto.dsFormaPgto AS dsFormaPgto,
                        tbParcelas.nrParcela AS nrParcela,
                        tbParcelas.qtDias AS qtDias,
                        tbParcelas.txPercentual AS txPercentual                        
                    FROM tbParcelas
                    INNER JOIN tbCondicaoPgto ON tbParcelas.idCondicaoPgto = tbCondicaoPgto.idCondicaoPgto
                    INNER JOIN tbFormaPgto ON tbParcelas.idFormaPgto = tbFormaPgto.idFormaPgto
                    WHERE tbParcelas.idCondicaoPgto = " + id + " ORDER BY nrParcela ASC";

            return sql;
        }
    }
}
