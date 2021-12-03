using System.Collections.ObjectModel;
using TappModels;
using TappData;

namespace TappService
{
    public static class LoginService
    {
        public static int GetId(string username)
        {
            return PersonGateway.Read(username);
        }

        public static Collection<Project> LoadProjects(int user_id, string user_role)
        {
            return ProjectMapper.Read(user_id, user_role);
        }
    }
}
