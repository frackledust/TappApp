using System.Collections.ObjectModel;
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

        /// <summary>
        /// Splits file name into file name and languages of project
        /// </summary>
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

        /// <summary>
        /// Creates project based on txt file 
        /// </summary>
        /// <param name="temporary">TRUE - project is not uploaded to database</param>
        public static Project CreateProject(string file_path, int requester_id, bool temporary)
        {
            //Check path
            if (IsTxtFile(file_path) == false) { return null; }

            //Create project
            string original_text = File.ReadAllText(file_path);

            if (GetParameters(file_path, out string[] parameters))
            {
                Project project = new Project
                (
                    -1,
                    parameters[0],
                    parameters[1],
                    parameters[2],
                    original_text,
                    default
                );

                //Upload to database
                if (temporary == false) { ProjectMapper.Create(project, requester_id); }

                return project;
            }

            return null;
        }

        //--------------------------------------------------------------------------------------------------------

        /// <summary>
        /// Separates projects that changed translation and sends them to DAO
        ///</summary>
        public static int SaveProjects(Collection<Project> projects)
        {
            Collection<Project> changed = new Collection<Project>();
            foreach(Project p in projects)
            {
                if(p.IsChanged)
                {
                    changed.Add(p);
                }
            }
            return ProjectMapper.Update(changed);
        }

        /// <summary>
        /// Sends project to DAO to get deleted
        ///</summary>
        public static void DeleteProject(Project project)
        {
            if (project == null) { return; }

            if (project.Id <= 0) { return; }

            ProjectMapper.DeleteProject(project.Id);
        }
    }
}
