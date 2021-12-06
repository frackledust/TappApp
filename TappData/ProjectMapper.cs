using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using TappModels;

namespace TappData
{
    public static class ProjectMapper
    {
        private const string CONNECTION_STRING = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=TappDatabase;Integrated Security=True;Connect Timeout=60;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        private const string insert_project_command = @"INSERT INTO [Project] (requester_id, name, original_language,translate_language, original_file) VALUES (@id,@name,@original,@translate,@text); SELECT CAST(scope_identity() AS int);";
        private const string delete_translator_command = @"UPDATE [Project] SET [translator_id] = NULL, [translate_file] = NULL WHERE [translator_id] = @id AND [is_completed] = 0";

        public static bool Create(Project project, int requester_id)
        {
            using (SqlConnection connection = new SqlConnection(CONNECTION_STRING))
            {
                using (SqlCommand command = new SqlCommand(insert_project_command, connection))
                {
                    try
                    {
                        command.Parameters.AddWithValue("@id", requester_id);
                        command.Parameters.AddWithValue("@name", project.Name);
                        command.Parameters.AddWithValue("@original", project.Original_language);
                        command.Parameters.AddWithValue("@translate", project.Translate_language);
                        command.Parameters.AddWithValue("@text", project.Original_text);
                        connection.Open();

                        int inserted_id = (int)command.ExecuteScalar();

                        project.Id = inserted_id;
                        return true;
                    }
                    catch
                    {
                        return false;
                    }
                }
            }
        }

        public static Collection<Project> Read(int id, string role, bool only_not_completed)
        {
            Collection<Project> projects = new Collection<Project>();

            using (SqlConnection connection = new SqlConnection(CONNECTION_STRING))
            {
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandType = CommandType.Text;
                    command.CommandText = $"SELECT * FROM [Project] WHERE [{role}_id] = {id}";
                    if(only_not_completed)
                    {
                        command.CommandText += " AND [is_completed] = 0;";
                    }

                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        int project_id = (int)reader["project_id"];
                        string name = (string)reader["name"];
                        string original_language = (string)reader["original_language"];
                        string translate_language = (string)reader["translate_language"];
                        string original_text = (string)reader["original_file"];
                        string translated_text = reader["translate_file"] as string;

                        Project p = new Project
                        {
                            Id = project_id,
                            Name = name,
                            Original_language = original_language,
                            Translate_language = translate_language,
                            Original_text = original_text,
                            Translated_text = translated_text
                        };

                        projects.Add(p);
                    }
                }
            }
            return projects;
        }

        public static void Update(Collection<Project> projects)
        {

        }

        public static int DeleteTranslations(int translator_id)
        {
            using (SqlConnection connection = new SqlConnection(CONNECTION_STRING))
            {
                using (SqlCommand command = new SqlCommand(delete_translator_command, connection))
                {
                    command.Parameters.AddWithValue("@id", translator_id);
                    connection.Open();

                    int rows_affected = command.ExecuteNonQuery();
                    return rows_affected;
                }
            }
        }
    }
}
