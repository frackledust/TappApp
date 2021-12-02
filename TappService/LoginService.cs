using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using TappModels;
using TappData;

namespace TappService
{
    public static class LoginService
    {
        public static Collection<Project> LoadProjects(string username, string user_role)
        {

            int user_id = PersonGateway.Read(username);

            Collection<Project> projects = ProjectMapper.Read(user_id, user_role);

            return projects;
        }
    }
}
