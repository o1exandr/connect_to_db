using System;
using System.Data.SqlClient;
using System.Configuration;

namespace connect_to_db
{
    class UserProvider
    {
        private SqlConnection _con;

        public UserProvider(SqlConnection con)
        {
            _con = con;
        }

        public bool Login(string email, string password = "")
        {
            string conToDb = ConfigurationManager.AppSettings["ConnectionString"];

            using (SqlConnection con = new SqlConnection(conToDb))
            {
                con.Open();
                string query = $"Select * from tblUsers";
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = con;
                sqlCommand.CommandText = query;
                SqlDataReader reader = sqlCommand.ExecuteReader();
                //int counter = 0;
                while (reader.Read())
                {
                    //Console.WriteLine($"({++counter, 3})\t{reader["Email"].ToString()}");
                    if (reader["Email"].ToString() == email)// && reader["Password"].ToString() == password)
                    {
                        Console.WriteLine($"\n{email} FOUNDED!");
                        return true;
                    }
                }
                Console.WriteLine($"\n{email} NOT founded");
                return false;
            }

        }
    }
}
