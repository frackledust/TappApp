using System.Collections.ObjectModel;
using TappData;
using TappModels;

namespace TappService
{
    public static class LoginService
    {
        public static int GetId(string username)
        {
            return PersonGateway.GetId(username);
        }

        public static Collection<Project> LoadProjects(int user_id, string user_role, bool only_not_completed)
        {
            return ProjectMapper.Read(user_id, user_role, only_not_completed);
        }
    }
}
