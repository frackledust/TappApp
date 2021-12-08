using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using TappModels;

namespace TappData
{
    /// <summary>
    /// Handles database quaries over table Project
    ///</summary>
    public static class ProjectMapper
    {
        private const string insert_project_command = @"INSERT INTO [Project] (requester_id, name, original_language,translate_language, original_file) VALUES (@id,@name,@original,@translate,@text); SELECT CAST(scope_identity() AS int);";
        private const string delete_translator_command = @"UPDATE [Project] SET [translator_id] = NULL, [translate_file] = NULL WHERE [translator_id] = @id AND [is_completed] = 0";
        private const string update_translation_command = @"UPDATE [Project] SET [translate_file] = @translation WHERE [project_id] = @id";

        /// <summary>
        /// Greates a new instance pf <see cref="Project"/> in the database
        ///</summary>
        public static bool Create(Project project, int requester_id)
        {
            using (SqlConnection connection = new SqlConnection(DBConnector.GetConnectionString()))
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

        /// <summary>
        /// Gets collecion of <see cref="Project"/> where <paramref name="role"/>_id is set to <paramref name="id"/>
        ///</summary>
        public static Collection<Project> Read(int id, string role, bool only_not_completed)
        {
            Collection<Project> projects = new Collection<Project>();

            using (SqlConnection connection = new SqlConnection(DBConnector.GetConnectionString()))
            {
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandType = CommandType.Text;
                    command.CommandText = $"SELECT * FROM [Project] WHERE [{role}_id] = {id}";
                    if (only_not_completed)
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

        /// <summary>
        /// Updates translation in database for each of <paramref name="projects"/>
        ///</summary>
        public static int Update(Collection<Project> projects)
        {
            using (SqlConnection connection = new SqlConnection(DBConnector.GetConnectionString()))
            {
                using (SqlCommand command = new SqlCommand(update_translation_command, connection))
                {
                    int rows_affected = 0;
                    foreach (Project p in projects)
                    {
                        command.Parameters.AddWithValue("@id", p.Id);
                        command.Parameters.AddWithValue("@translation", p.Translated_text);
                        connection.Open();

                        rows_affected += command.ExecuteNonQuery();
                    }

                    return rows_affected;
                }
            }
        }


        /// <summary>
        /// Updates is_complete status to true/1 for each of <paramref name="projects"/>
        ///</summary>
        public static int MarkComplete(Collection<Project> projects)
        {
            using (SqlConnection connection = new SqlConnection(DBConnector.GetConnectionString()))
            {
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandType = CommandType.Text;
                    command.CommandText = @"UPDATE [Project] SET [is_complete] = 1 WHERE [project_id] = @id";

                    int rows_affected = 0;
                    foreach (Project p in projects)
                    {
                        command.Parameters.AddWithValue("@id", p.Id);
                        command.Parameters.AddWithValue("@translation", p.Translated_text);
                        connection.Open();

                        rows_affected += command.ExecuteNonQuery();
                    }

                    return rows_affected;
                }
            }
        }

        /// <summary>
        ///Deletes unfinished translations and sets translator id as NULL
        ///</summary>
        public static int DeleteTranslations(int translator_id)
        {
            using (SqlConnection connection = new SqlConnection(DBConnector.GetConnectionString()))
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

        /// <summary>
        ///Deletes project from database
        ///</summary>
        public static int DeleteProject(int project_id)
        {
            using (SqlConnection connection = new SqlConnection(DBConnector.GetConnectionString()))
            {
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandType = CommandType.Text;
                    command.CommandText = "DELETE FROM [Project] WHERE [project_id]=@id";
                    command.Parameters.AddWithValue("@id", project_id);
                    connection.Open();

                    int rows_affected = command.ExecuteNonQuery();
                    return rows_affected;
                }
            }
        }
    }
}
