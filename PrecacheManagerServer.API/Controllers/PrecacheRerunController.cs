using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PrecacheManagerServer.API.Models;
//using PrecacheManagerServer.BLL.Enums.Extensions;
using PrecacheManagerServer.Shared.Enums.Extensions;
using PrecacheManagerServer.BLL.Models;
using PrecacheManagerServer.BLL.Services;
using PrecacheManagerServer.Shared.Models;
using PrecacheManagerServer.Controllers;

namespace PrecacheManagerServer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrecacheRerunController : UserController
    {
        IPrecacheRerunService _service;
        IPlatformSettings _platformSettings;

        public PrecacheRerunController(IServiceProvider serviceProvider, IPrecacheRerunService service, IPlatformSettings platformSettings) :base(serviceProvider)
        {
            _service = service;
            _platformSettings = platformSettings;
        }

        [HttpPost]
        [Route("[action]/{applicationModeId}")]
        public async Task<IResultMessage<bool>> RerunFailedPrecacheSearch(int applicationModeId, [FromBody] PrecacheRerun precacheRerun)
        {
            if (!ModelState.IsValid)
            {
                throw new HttpRequestException();
            }

            var result = new ResultMessage<bool>()
            {
                Success = false
            };

            //now pass onto the 
            try
            {
                foreach (var key in CurrentUser.PlatformSettings.ConnectionStrings.Keys)
                {

                    if ((int)key.GetAttribute<ApplicationModeIdAttribute>().ApplicationModeId == applicationModeId)
                    {
                        var platformSettingRequestsModelAddOrUpdate = new PlatformSettingRequestsModelAddOrUpdate<PrecacheRerun>();

                        platformSettingRequestsModelAddOrUpdate.ConnectionStrings.Add(key, CurrentUser.PlatformSettings.ConnectionStrings[key]);

                        platformSettingRequestsModelAddOrUpdate.Data.Add(precacheRerun);

                        var data = await _service.AddOrUpdateSP<PrecacheRerun>(platformSettingRequestsModelAddOrUpdate);


                        result.Success = data;
                        result.Data = data;
                        

                        //result.AddRange(r.ToList().Where(x => x.SiteId == siteId));
                        //result.AddRange(r.ToList());
                    }
                }
            }catch(Exception ex)
            {
                result.Success = false;
                result.Error = ex;
                result.FriendlyErrorMessage = "Unable to rerun failed PrecacheSearch";
            }

            return result;

        }
    }
}