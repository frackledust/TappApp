using System;
using System.Data;
using System.Data.SqlClient;

namespace TappData
{
    public static class PersonGateway
    {
        public const string CONNECTION_STRING = @"Data Source = (localdb)\MSSQLLocalDB;Initial Catalog = TappDatabase; Integrated Security = True; Connect Timeout = 60; Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public static int Read(string username)
        {
            if(username == null || username.Length == 0) return 0;

            int person_id = 0;

            using (SqlConnection connection = new SqlConnection(CONNECTION_STRING))
            {

                connection.Open();

                using (SqlCommand command = connection.CreateCommand())
                {

                    command.CommandType = CommandType.Text;
                    command.CommandText = $"SELECT * FROM [Person] WHERE [username] = @username";
                    command.Parameters.AddWithValue("@username", username);

                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        person_id = (int)reader["person_id"];
                    }
                }

            }

            return person_id;
        }
    }
}
