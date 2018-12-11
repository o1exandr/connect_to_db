using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace connect_to_db
{
    class UserProvider
    {
        private SqlConnection _con;

        public UserProvider(SqlConnection con)
        {
            _con = con;
        }

        public bool Login(string email, string password)
        {
            string conToDb = "Data Source=somebase.mssql.somee.com;Initial Catalog=somebase;User ID=finiuk_SQLLogin_1;Password=mvkwivz38h";

            using (SqlConnection con = new SqlConnection(conToDb))
            {
                con.Open();
                string query = $"Select * from tblUsers";
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = con;
                sqlCommand.CommandText = query;
                SqlDataReader reader = sqlCommand.ExecuteReader();
                while (reader.Read())
                {
                    if (reader["Email"].ToString() == email && reader["Password"].ToString() == password)
                        return true;
                }
                return false;
            }

        }
    }
}
