using System.Data.SqlClient;
using System.Reflection;

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


        // -- TESTES -- 
        public string Insert(System.Type type, string tableName)
        {
            string insertSQL = "insert into " + tableName + " ( ";
            string values = " values (";

            foreach (PropertyInfo property in type.GetProperties())
            {
                insertSQL += property.Name.ToLower() + ",";
                values += "@" + property.Name.ToLower() + ",";
            }
            insertSQL = insertSQL.Remove((insertSQL.Length - 1), 1);
            insertSQL += ")";
            values = values.Remove((values.Length - 1), 1);
            values += ")";

            return insertSQL + values;
        }

        public string Edit(System.Type type, string tableName)
        {
            string editSQL = "alter table " + tableName + " ( ";
            string values = " values (";

            foreach (PropertyInfo property in type.GetProperties())
            {
                editSQL += property.Name.ToLower() + ",";
                values += "@" + property.Name.ToLower() + ",";
            }
            editSQL = editSQL.Remove((editSQL.Length - 1), 1);
            editSQL += ")";
            values = values.Remove((values.Length - 1), 1);
            values += ")";

            return editSQL + values;
        }

        public string SelectAll(string tableName)
        {
            return "select * from " + tableName;
        }        
    }
}