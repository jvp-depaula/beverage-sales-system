﻿using Sistema.Models;
using System.Data.SqlClient;

namespace Sistema.DAO
{
    public class DAOProdutos : DAO
    {
        public List<Produtos> GetProdutos(int? idFornecedor)
        {
            try
            {
                var sql = this.Search(null, idFornecedor);
                OpenConnection();
                SqlQuery = new SqlCommand(sql, con);
                reader = SqlQuery.ExecuteReader();
                var list = new List<Produtos>();

                while (reader.Read())
                {
                    var produto = new Produtos
                    {
                        idProduto = Convert.ToInt32(reader["idProduto"]),
                        dsProduto = Convert.ToString(reader["dsProduto"]),
                        idFornecedor = Convert.ToInt32(reader["idFornecedor"]),
                        nmFornecedor = Convert.ToString(reader["nmFornecedor"]),
                        idCategoria = Convert.ToInt32(reader["idCategoria"]),
                        nmCategoria = Convert.ToString(reader["nmCategoria"]),
                        idUnidade = Convert.ToInt32(reader["idUnidade"]),
                        dsUnidade = Convert.ToString(reader["dsUnidade"]),
                        idMarca = Convert.ToInt32(reader["idMarca"]),
                        nmMarca = Convert.ToString(reader["nmMarca"]),
                        cdNCM = Convert.ToString(reader["cdNCM"]),
                        vlVenda = Convert.ToDecimal(reader["vlVenda"]),
                        vlCusto = Convert.ToDecimal(reader["vlCusto"]),
                        qtdEstoque = Convert.ToDecimal(reader["qtdEstoque"]),
                        vlUltCompra = Convert.ToDecimal(reader["vlUltCompra"]),
                        observacao = Convert.ToString(reader["observacao"]),
                        dtCadastro = Convert.ToDateTime(reader["dtCadastro"]),
                        dtUltAlteracao = Convert.ToDateTime(reader["dtUltAlteracao"])                        
                    };

                    list.Add(produto);
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

        public void Insert(Models.Produtos produto)
        {
            try
            {
                var sql = string.Format("INSERT INTO tbProdutos (dsProduto, idFornecedor, idCategoria, idUnidade, idMarca, cdNCM, vlVenda, qtdEstoque, vlCusto, vlUltCompra, observacao, dtCadastro, dtUltAlteracao) VALUES ('{0}', {1}, {2}, {3}, {4}, '{5}', {6}, {7}, {8}, {9}, '{10}', '{11}', '{12}')",
                                        produto.dsProduto,
                                        produto.idFornecedor,
                                        Convert.ToInt32(produto.idCategoria),
                                        Convert.ToInt32(produto.idUnidade),
                                        Convert.ToInt32(produto.idMarca),
                                        produto.cdNCM,
                                        Convert.ToDecimal(produto.vlVenda),
                                        produto.vlCusto == null ? 0 : produto.vlCusto,
                                        produto.vlUltCompra == null ? 0 : produto.vlUltCompra,
                                        produto.qtdEstoque == null ? 0 : produto.qtdEstoque,
                                        produto.observacao,
                                        Util.Util.FormatDate(DateTime.Now),
                                        Util.Util.FormatDate(DateTime.Now));
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

        public void Update(Models.Produtos produto)
        {
            try
            {
                var sql = string.Format("UPDATE tbProdutos SET dsProduto = '{0}', idFornecedor = '{1}', idCategoria = '{2}', " +
                    "idUnidade = '{3}', idMarca = '{4}', cdNCM = '{5}', vlVenda = '{6}', observacao = '{7}', dtUltAlteracao = '{8}', vlCusto = {9}, vlUltCompra = {10}, qtdEstoque = {11} WHERE idProduto = '{12}'",
                     produto.dsProduto,
                     produto.idFornecedor,
                     Convert.ToInt32(produto.idCategoria),
                     Convert.ToInt32(produto.idUnidade),
                     Convert.ToInt32(produto.idMarca),
                     produto.cdNCM,
                     Convert.ToDecimal(produto.vlVenda),
                     produto.observacao,
                     Util.Util.FormatDate(DateTime.Now),
                     produto.vlCusto ?? 0,
                     produto.vlUltCompra ?? 0,
                     produto.qtdEstoque ?? 0,
                     produto.idProduto);
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

        public Produtos GetProduto(int? idProduto)
        {
            try
            {
                var model = new Models.Produtos();
                if (idProduto != null)
                {
                    OpenConnection();
                    var sql = this.Search(idProduto, null);
                    SqlQuery = new SqlCommand(sql, con);
                    reader = SqlQuery.ExecuteReader();
                    while (reader.Read())
                    {
                        model.idProduto = Convert.ToInt32(reader["idProduto"]);
                        model.dsProduto = Convert.ToString(reader["dsProduto"]);
                        model.idFornecedor = Convert.ToInt32(reader["idFornecedor"]);
                        model.nmFornecedor = Convert.ToString(reader["nmFornecedor"]);
                        model.idCategoria = Convert.ToInt32(reader["idCategoria"]);
                        model.nmCategoria = Convert.ToString(reader["nmCategoria"]);
                        model.idUnidade = Convert.ToInt32(reader["idUnidade"]);
                        model.dsUnidade = Convert.ToString(reader["dsUnidade"]);
                        model.idMarca = Convert.ToInt32(reader["idMarca"]);
                        model.nmMarca = Convert.ToString(reader["nmMarca"]);
                        model.cdNCM = Convert.ToString(reader["cdNCM"]);
                        model.vlVenda = Convert.ToDecimal(reader["vlVenda"]);
                        model.qtdEstoque = Convert.ToDecimal(reader["qtdEstoque"]);
                        model.vlUltCompra = Convert.ToDecimal(reader["vlUltcompra"]);
                        model.vlCusto = Convert.ToDecimal(reader["vlCusto"]);
                        model.observacao = Convert.ToString(reader["observacao"]);
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

        public void Delete(int? idProduto)
        {
            try
            {
                string sql = "DELETE FROM tbProdutos WHERE idProduto = " + idProduto;
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

        private string Search(int? id, int? idFornecedor)
        {
            var sql = string.Empty;
            var swhere = string.Empty;
            if (id != null && idFornecedor != null)
                swhere = " WHERE tbProdutos.idProduto = " + id + " AND tbProdutos.idFornecedor = " + idFornecedor + ";";
            else if (id != null)
            {
                if (idFornecedor != null)
                    swhere = " WHERE tbProdutos.idProduto = " + id + " AND tbProdutos.idFornecedor = " + idFornecedor + ";";
                else
                    swhere = " WHERE tbProdutos.idProduto = " + id + ";";
            }            

            sql = @"
                    SELECT
                        tbProdutos.idProduto AS idProduto,
                        tbProdutos.dsProduto AS dsProduto,
                        tbProdutos.idFornecedor AS idFornecedor,
                        tbFornecedores.nmFornecedor AS nmFornecedor,
                        tbProdutos.idCategoria AS idCategoria,
                        tbCategorias.nmCategoria AS nmCategoria,
                        tbProdutos.idUnidade AS idUnidade,
                        tbUnidades.dsUnidade AS dsUnidade,
                        tbProdutos.idMarca AS idMarca,
                        tbMarcas.nmMarca AS nmMarca,
                        tbProdutos.cdNCM AS cdNCM,
                        tbProdutos.vlVenda AS vlVenda,
                        tbProdutos.vlCusto AS vlCusto,
                        tbProdutos.vlUltCompra AS vlUltCompra,
                        tbProdutos.qtdEstoque AS qtdEstoque,
                        tbProdutos.observacao AS observacao,
                        tbProdutos.dtCadastro AS dtCadastro,
                        tbProdutos.dtUltAlteracao AS dtUltAlteracao
                        FROM tbProdutos 
                        JOIN tbFornecedores ON tbProdutos.idFornecedor = tbFornecedores.idFornecedor
                        JOIN tbCategorias ON tbProdutos.idCategoria = tbCategorias.idCategoria
                        JOIN tbUnidades ON tbProdutos.idUnidade = tbUnidades.idUnidade
                        JOIN tbMarcas ON tbProdutos.idMarca = tbMarcas.idMarca" + swhere;
            return sql;
        }
    }
}
