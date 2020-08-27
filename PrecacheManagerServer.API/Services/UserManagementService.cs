using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrecacheManagerServer.API.Services
{
    public class UserManagementService :IUserManagementService
    {
        public bool IsValidUser(string userName, string password)
        {
            return true;
        }
    }
}
