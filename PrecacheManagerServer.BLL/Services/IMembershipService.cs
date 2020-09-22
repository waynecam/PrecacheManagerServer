using PrecacheManagerServer.BLL.Enums;
using PrecacheManagerServer.BLL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PrecacheManagerServer.BLL.Services
{
    public interface IMembershipService
    {
        UserResponseModel GetUser(string username, string password);
        List<ApplicationMode> GetUserPlatforms(string userName);

    }
}
