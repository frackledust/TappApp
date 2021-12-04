using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace TappData
{
    public static class PersonGateway
    {
        public const string CONNECTION_STRING = @"Data Source = (localdb)\MSSQLLocalDB;Initial Catalog = TappDatabase; Integrated Security = True; Connect Timeout = 60; Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        private static string GetCommandLanguages(string[] languages)
        {
            StringBuilder string_builder = new StringBuilder();
            string_builder.Append($"SELECT [email] FROM [Person] WHERE [languages] LIKE '%'");

            foreach (string language in languages)
            {
                string_builder.Append($"AND [languages] LIKE '%{language}%'");
            }

            return string_builder.ToString();
        }

        public static DataTable GetEmails(string[] languages)
        {
            DataTable dt = new DataTable();

            using (SqlConnection connection = new SqlConnection(CONNECTION_STRING))
            {

                connection.Open();

                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandType = CommandType.Text;
                    command.CommandText = GetCommandLanguages(languages);

                    SqlDataReader reader = command.ExecuteReader();


                    dt.Load(reader);

                    return dt;
                }
            }
        }

        public static int Read(string username)
        {
            if (username == null || username.Length == 0) return 0;

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
