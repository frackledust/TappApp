using System.Collections.ObjectModel;
using TappData;
using TappModels;

namespace TappService
{
    /// <summary>
    ///  Handles the login domain logic
    /// </summary>
    public static class LoginService
    {
        /// <summary>
        /// Returns an id from database based on <paramref name="username"/>
        /// </summary>
        public static int GetId(string username)
        {
            return PersonGateway.GetId(username);
        }

        /// <summary>
        /// Returns collection of projects tagged with <paramref name="user_id"/>
        /// </summary>   
        /// <param name="user_role">
        ///     requester or translator
        /// </param>
        /// <param name="only_not_completed">
        ///     True - loads only not completed projects,
        ///     False - loads completed projects as well </param>
        /// <returns></returns>
        public static Collection<Project> LoadProjects(int user_id, string user_role, bool only_not_completed)
        {
            return ProjectMapper.Read(user_id, user_role, only_not_completed);
        }
    }
}
