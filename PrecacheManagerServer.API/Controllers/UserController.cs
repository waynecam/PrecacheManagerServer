using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PrecacheManagerServer.API.Models;
using PrecacheManagerServer.API.Services;
//using PrecacheManagerServer.BLL.Enums;
//using PrecacheManagerServer.Enums;
//using ApplicationMode = PrecacheManagerServer.BLL.Enums.ApplicationMode;
using PrecacheManagerServer.Shared.Enums;
using PrecacheManagerServer.Shared.Models;

namespace PrecacheManagerServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        IPlatformSettings _platformSettings;
        IPlatformConfigService _platformConfigService; 

        public UserController(IServiceProvider serviceProvider)
        {
            //https://stackoverflow.com/questions/21916020/base-controller-constructor-injection-in-asp-net-mvc-with-unity
            _platformSettings = (PlatformSettings)serviceProvider.GetService(typeof(IPlatformSettings));

            _platformConfigService = (IPlatformConfigService)serviceProvider.GetService(typeof(IPlatformConfigService));
        }


        public virtual User CurrentUser
        {
            get
            {
                return BuildCurrentUser();
            }
        }

        public User BuildCurrentUser()
        {
            var user = new User();

            //https://stackoverflow.com/questions/36641338/how-to-get-current-user-in-asp-net-core
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            try
            {
                if (identity != null)
                {
                    IEnumerable<Claim> claims = identity.Claims;
                    // or
                    //identity.FindFirst("ClaimName").Value;
                    user = new User()
                    {
                        //UserName = claims.FirstOrDefault(x => x.Type.Equals("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name", StringComparison.OrdinalIgnoreCase)).Value,

                        //https://stackoverflow.com/questions/22246538/access-claim-values-in-controller-in-mvc-5
                        UserName = claims.Where(c => c.Type == ClaimTypes.Name).Select(c => c.Value).FirstOrDefault(),
                        ApplicationModes = GetUserApplicationModes(claims).ToList()
                        


                    };

                    user.PlatformSettings = GetPlatformSettings(user.ApplicationModes, _platformSettings);


                };
            }catch(Exception ex)
            {

            }



            return user;
        }

        private IPlatformSettings GetPlatformSettings(IEnumerable<ApplicationMode> appModes, IPlatformSettings platformSettings)
        {
            IPlatformSettings userPlatformSettings = new PlatformSettings();
            //userPlatformSettings.ConnectionStrings.Clear();
           

            foreach (var appMode in appModes)
            {
                //foreach(var connection in platformSettings.ConnectionStrings)
                foreach(var connection in _platformConfigService.ConnectionStrings)
                {
                    if(connection.Key == appMode)
                    {
                        userPlatformSettings.ConnectionStrings.Add(connection.Key, connection.Value);
                    }
                }
            }

            return userPlatformSettings;



        }

        //private List<IPlatformSettings> GetPlatformSettings(List<ApplicationMode> applicationModes, IPlatformSettings platformSettings)
        //{
        //    List<IPlatformSettings> userPlatformSettings = new List<IPlatformSettings>();

        //    userPlatformSettings = platformSettings.sel

        //    return userPlatformSettings;
        //}

        private IEnumerable<ApplicationMode> GetUserApplicationModes(IEnumerable<Claim> claims)
        {
            List<ApplicationMode> appModes = new List<ApplicationMode>();
            
            var enumValues = Enum.GetValues(typeof(ApplicationMode)).Cast<ApplicationMode>();

            foreach(var appMode in enumValues)
            {
                foreach(var claim in claims)
                {
                    if(claim.Value.ToLower() == appMode.ToString().ToLower())
                    {
                        appModes.Add(appMode);
                    }
                }
            }

            return appModes;

        }
    }
}