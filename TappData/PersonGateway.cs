using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace TappData
{
    /// <summary>
    /// Handles database quaries over table Person
    ///</summary>
    public static class PersonGateway
    {
        /// <summary>
        /// Creates database filter command based on spoken <paramref name="languages"/>
        ///</summary>
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

        /// <summary>
        /// Gets emails from people who speak all mentioned <paramref name="languages"/>
        ///</summary>
        public static DataTable GetEmails(string[] languages)
        {
            DataTable dt = new DataTable();

            using (SqlConnection connection = new SqlConnection(DBConnector.GetConnectionString()))
            {
                using (SqlCommand command = connection.CreateCommand())
                {
                    try
                    {
                        command.CommandType = CommandType.Text;
                        command.CommandText = GetCommandLanguages(languages);

                        connection.Open();

                        SqlDataReader reader = command.ExecuteReader();

                        dt.Load(reader);

                        return dt;
                    }
                    catch (SqlException ex)
                    {
                        Console.WriteLine(ex.Message);
                        throw ex;
                    }
                }
            }
        }

        /// <summary>
        /// Gets person_id based on <paramref name="username"/>
        ///</summary>
        public static int GetId(string username)
        {
            if (username == null || username.Length == 0) return -1;

            using (SqlConnection connection = new SqlConnection(DBConnector.GetConnectionString()))
            {
                using (SqlCommand command = connection.CreateCommand())
                {
                    try
                    {
                        command.CommandType = CommandType.Text;
                        command.CommandText = @"SELECT [person_id] FROM [Person] WHERE ([username] = @username AND [is_active] = 1)";
                        command.Parameters.AddWithValue("@username", username);

                        connection.Open();

                        SqlDataReader reader = command.ExecuteReader();

                        if (reader.Read())
                        {
                            return (int)reader[0];
                        }
                        else
                        {
                            return -1;
                        }
                    }
                    catch
                    {
                        return -1;
                    }
                }
            }
        }

        /// <summary>
        /// Sets is_active status of person with <paramref name="id"/> to 0/false
        ///</summary>
        public static int Deactive(int id)
        {
            using (SqlConnection connection = new SqlConnection(DBConnector.GetConnectionString()))
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
