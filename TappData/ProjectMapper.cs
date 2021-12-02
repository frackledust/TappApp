using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using TappModels;

namespace TappData
{
    public static class ProjectMapper
    {
        public const string CONNECTION_STRING = @"Data Source = (localdb)\MSSQLLocalDB;Initial Catalog = TappDatabase; Integrated Security = True; Connect Timeout = 60; Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public static Collection<Project> Read(long Id, string role)
        {
            Collection<Project> projects = new Collection<Project>();

            using (SqlConnection connection = new SqlConnection(CONNECTION_STRING))
            {

                connection.Open();

                using (SqlCommand command = connection.CreateCommand())
                {

                    command.CommandType = CommandType.Text;
                    command.CommandText = $"SELECT * FROM [Project] WHERE [{role}_id] = @ID";
                    command.Parameters.AddWithValue("@ID", Id);

                    SqlDataReader reader = command.ExecuteReader();

                    while(reader.Read())
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
    }
}
