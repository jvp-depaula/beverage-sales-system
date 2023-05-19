using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Sistema.DAO
{
    public class DAO
    {
        protected SqlConnection con;
        protected SqlCommand SqlQuery;
        protected SqlDataReader reader;

        protected void OpenConnection()
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json")
            .Build();

            try
            {
                con = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
                con.Open();
            }
            catch (Exception error)
            {
                throw new Exception("Erro ao abrir a conexão: " + error.Message);
            }
        }

        protected void CloseConnection()
        {
            try
            {
                if (con != null)
                {
                    con.Close();
                }
            }
            catch (Exception error)
            {
                throw new Exception("Erro ao fechar a conexão: " + error.Message);
            }
        }        
    }
}