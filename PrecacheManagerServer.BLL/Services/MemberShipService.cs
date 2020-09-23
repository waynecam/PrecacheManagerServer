using PrecacheManagerServer.BLL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using PrecacheManagerServer.BLL.Enums;


namespace PrecacheManagerServer.BLL.Services
{
    public class MemberShipService : IMembershipService
    {

      

        public UserResponseModel GetUser(string username, string password)
        {

            //return result.Select(t => _mapper.Map<User, UserResponseModel>(t));
            return new UserResponseModel() { UserName = "wayne.campbell", Password = "testpassword" ,
                ApplicationModes = new List<ApplicationMode> { ApplicationMode.GermanyMedia, ApplicationMode.International} };
        }


        public List<ApplicationMode> GetUserPlatforms(string userName)
        {

            //select * from membership database where username = username
            return new List<ApplicationMode>() { ApplicationMode.GermanyMedia };
        }
    }
}
