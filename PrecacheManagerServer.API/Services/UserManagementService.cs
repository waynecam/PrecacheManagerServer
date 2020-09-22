using PrecacheManagerServer.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PrecacheManagerServer.BLL.Services;
using PrecacheManagerServer.BLL.Models;
using AutoMapper;

namespace PrecacheManagerServer.API.Services
{
    //https://garywoodfine.com/asp-net-core-2-2-jwt-authentication-tutorial/
    public class UserManagementService :IUserManagementService
    {
       
        IMembershipService _memberShipService;
        private readonly IMapper _mapper;

        public UserManagementService(IMembershipService membershipService, IMapper mapper)
        {
            
            _memberShipService = membershipService;
            _mapper = mapper;
        }
        public bool IsValidUser(string userName, string password)
        {
           var user = _memberShipService.GetUser(userName, password);

            // use this 
            //http://www.advancesharp.com/blog/1216/oauth-web-api-token-based-authentication-with-custom-database
            return user != null;
        }

        public UserResponseModel GetUser(string userName, string password)
        {
            var user = _memberShipService.GetUser(userName, password);

            //var result = _mapper.Map<User, UserResponseModel>(user);


            // use this 
            //http://www.advancesharp.com/blog/1216/oauth-web-api-token-based-authentication-with-custom-database
            return user;
        }

    }
}
