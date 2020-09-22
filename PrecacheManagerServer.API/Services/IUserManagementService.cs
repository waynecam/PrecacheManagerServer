using PrecacheManagerServer.API.Models;
using PrecacheManagerServer.BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrecacheManagerServer.API.Services
{
    public interface IUserManagementService
    {
        bool IsValidUser(string username, string password);
        UserResponseModel GetUser(string username, string password);
    }
}
