﻿using Sistema.Models;
using System.Data.SqlClient;

namespace Sistema.DAO
{
    public class DAOCategorias : DAO
    {
        public List<Categorias> GetCategorias()
        {
            try
            {
                var sql = this.Search(null, null);
                OpenConnection();
                SqlQuery = new SqlCommand(sql, con);
                reader = SqlQuery.ExecuteReader();
                var list = new List<Categorias>();

                while (reader.Read())
                {
                    var categoria = new Categorias
                    {
                        idCategoria = Convert.ToInt32(reader["idCategoria"]),
                        nmCategoria = Convert.ToString(reader["nmCategoria"]),
                        dtCadastro = Convert.ToDateTime(reader["dtCadastro"]),
                        dtUltAlteracao = Convert.ToDateTime(reader["dtUltAlteracao"])
                    };

                    list.Add(categoria);
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

        public void Insert(Models.Categorias categoria)
        {
            try
            {
                /*
                var sql = string.Format("INSERT INTO tbCategorias (nmCategoria, dtCadastro, dtUltAlteracao) VALUES ('{0}', '{1}', '{2}')",
                    categoria.nmCategoria,
                    Util.Util.FormatDate(DateTime.Now),
                    Util.Util.FormatDate(DateTime.Now)
                );

                OpenConnection();
                SqlQuery = new SqlCommand(sql, con);
                SqlQuery.ExecuteNonQuery();
                */ 

                // TESTE REQUISITADO PELO PROFESSOR EDUARDO RIBEIRO

                OpenConnection();
                string query = "INSERT INTO tbCategorias (nmCategoria, dtCadastro, dtUltAlteracao) VALUES (@nmCategoria, @dtCadastro, @dtUltAlteracao);";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@nmCategoria", categoria.nmCategoria);
                    cmd.Parameters.AddWithValue("@dtCadastro", DateTime.Now);
                    cmd.Parameters.AddWithValue("@dtUltAlteracao", DateTime.Now);

                    SqlQuery = cmd;
                    SqlQuery.ExecuteNonQuery();
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

        public void Update(Models.Categorias categoria)
        {
            try
            {
                var sql = String.Format("UPDATE tbCategorias SET nmCategoria = '{0}', dtUltAlteracao = '{1}' WHERE idCategoria = '{2}'", categoria.nmCategoria, Util.Util.FormatDate(DateTime.Now), categoria.idCategoria);
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

        public Categorias GetCategoria(int? idCategoria)
        {
            try
            {
                var model = new Models.Categorias();

                if (idCategoria != null)
                {
                    OpenConnection();
                    var sql = this.Search(idCategoria, null);
                    SqlQuery = new SqlCommand(sql, con);
                    reader = SqlQuery.ExecuteReader();

                    while (reader.Read())
                    {
                        model.idCategoria = Convert.ToInt32(reader["idCategoria"]);
                        model.nmCategoria = Convert.ToString(reader["nmCategoria"]);
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

        public void Delete(int? idCategoria)
        {
            try
            {
                string sql = "DELETE FROM tbCategorias WHERE idCategoria = " + idCategoria;
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
                swhere = " WHERE idCategoria = " + id;
            }
            if (!string.IsNullOrEmpty(filter))
            {
                var filterQ = filter.Split(' ');
                foreach (var word in filterQ)
                {
                    swhere += " OR tbCategorias.nmCategoria LIKE'%" + word + "%'";
                }
                swhere = " WHERE " + swhere.Remove(0, 3);
            }
            sql = @"
                    SELECT
                        idCategoria AS idCategoria,
                        nmCategoria AS nmCategoria,
                        dtCadastro AS dtCadastro,
                        dtUltAlteracao AS dtUltAlteracao
                    FROM tbCategorias" + swhere;
            return sql;
        }
    }
}
