using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using TappModels;

namespace TappData
{
    public static class ProjectMapper
    {
        public const string CONNECTION_STRING = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=TappDatabase;Integrated Security=True;Connect Timeout=60;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        public static Collection<Project> Read(int id, string role)
        {
            Collection<Project> projects = new Collection<Project>();

            using (SqlConnection connection = new SqlConnection(CONNECTION_STRING))
            {
                connection.Open();

                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandType = CommandType.Text;
                    command.CommandText = $"SELECT * FROM [Project] WHERE [{role}_id] = @ID";
                    command.Parameters.AddWithValue("@ID", id);

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

        public static bool Create(Project project, int requester_id)
        {
            using (SqlConnection connection = new SqlConnection(CONNECTION_STRING))
            {
                connection.Open();

                int max_id = -1;

                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandType = CommandType.Text;
                    command.CommandText = $"SELECT MAX(project_id) FROM [Project]";

                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        max_id = (int)reader[0];
                    }

                    reader.Close();
                }

                string command_text = $"INSERT INTO [Project] (requester_id, name, original_language,translate_language, original_file) VALUES (@id,@name,@original,@translate,@text);";

                using (SqlCommand command = new SqlCommand(command_text, connection))
                {
                    command.Parameters.AddWithValue("@id", requester_id);
                    command.Parameters.AddWithValue("@name", project.Name);
                    command.Parameters.AddWithValue("@original", project.Original_language);
                    command.Parameters.AddWithValue("@translate", project.Translate_language);
                    command.Parameters.AddWithValue("@text", project.Original_text);

                    var rowsAffected = command.ExecuteNonQuery();

                    project.Id = max_id + 1;

                    return rowsAffected == 1;
                }
            }
        }
    }
}
