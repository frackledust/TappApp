using System;
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

        public static int GetId(string username)
        {
            int person_id = default;
            if (username == null || username.Length == 0) return person_id;

            using (SqlConnection connection = new SqlConnection(CONNECTION_STRING))
            {
                connection.Open();

                using (SqlCommand command = connection.CreateCommand())
                {
                    try
                    {
                        command.CommandType = CommandType.Text;
                        command.CommandText = @"SELECT * FROM [Person] WHERE [username] = @username";
                        command.Parameters.AddWithValue("@username", username);

                        person_id = (int) (command.ExecuteScalar() ?? -1);
                        return person_id;
                    }
                    catch (Exception ex)
                    {
                        return -1;
                    }
                }
            }
        }

        public static int Deactive(int id)
        {
            using (SqlConnection connection = new SqlConnection(CONNECTION_STRING))
            {
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandType = CommandType.Text;
                    command.CommandText = @"UPDATE [Person] SET [is_active] = 0 WHERE person_id = @id";
                    command.Parameters.AddWithValue("@id", id);
                    connection.Open();

                    int rows_affected = command.ExecuteNonQuery();
                    return rows_affected;
                }
            }
        }
    }
}
