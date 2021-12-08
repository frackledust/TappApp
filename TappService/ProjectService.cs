using System.IO;
using TappData;
using TappModels;

namespace TappService
{
    /// <summary>
    /// Use Case 1:
    /// Domain logic handing project's parameters
    /// </summary>
    public static class ProjectService
    {
        /// <summary>
        /// Checks if <paramref name="file_path"/> leads to a txt file
        /// </summary>
        private static bool IsTxtFile(string file_path) => (file_path != null && file_path.EndsWith("txt"));

        private static bool GetParameters(string file_path, out string[] parameters)
        {
            string temp = Path.GetFileName(file_path);
            parameters = temp.Split('_');

            if (parameters.Length >= 3)
            {
                parameters[2] = parameters[2].Remove(parameters[2].Length - 4); //Deletes .txt
                return true;
            }
            return false;
        }

        public static Project CreateProject(string file_path, int requester_id, bool temporary)
        {
            //Check path
            if (IsTxtFile(file_path) == false) { return null; }

            //Create project
            string original_text = File.ReadAllText(file_path);

            if (GetParameters(file_path, out string[] parameters))
            {
                Project project = new Project()
                {
                    Id = default,
                    Name = parameters[0],
                    Original_language = parameters[1],
                    Translate_language = parameters[2],
                    Original_text = original_text,
                };

                //Upload to database
                if (temporary == false) { ProjectMapper.Create(project, requester_id); }

                return project;
            }

            return null;
        }

        //--------------------------------------------------------------------------------------------------------
    }
}
