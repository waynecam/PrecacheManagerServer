using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PrecacheManagerServer.API.Models;
using PrecacheManagerServer.BLL.Enums.Extensions;
using PrecacheManagerServer.BLL.Models;
using PrecacheManagerServer.BLL.Services;

namespace PrecacheManagerServer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrecacheRerunController : ControllerBase
    {
        IPrecacheRerunService _service;
        IPlatformSettings _platformSettings;

        public PrecacheRerunController(IPrecacheRerunService service, IPlatformSettings platformSettings)
        {
            _service = service;
            _platformSettings = platformSettings;
        }

        [HttpPost]
        [Route("[action]/{applicationMode}")]
        public async Task RerunFailedPrecacheSearch(int applicationMode, [FromBody] PrecacheRerun precacheRerun)
        {
            if (!ModelState.IsValid)
            {
                throw new HttpRequestException();
            }

            //now pass onto the 

            foreach (var key in _platformSettings.ConnectionStrings.Keys)
            {

                if ((int)key.GetAttribute<ApplicationModeIdAttribute>().ApplicationModeId == applicationMode)
                {
                    var platformSettingRequestsModelAddOrUpdate = new PlatformSettingRequestsModelAddOrUpdate<PrecacheRerun>();

                    platformSettingRequestsModelAddOrUpdate.Connections.Add(key, _platformSettings.ConnectionStrings[key]);

                    platformSettingRequestsModelAddOrUpdate.Data.Add(precacheRerun);

                    await _service.AddOrUpdateSP<PrecacheRerun>(platformSettingRequestsModelAddOrUpdate);

                    //result.AddRange(r.ToList().Where(x => x.SiteId == siteId));
                    //result.AddRange(r.ToList());
                }
            }

        }
    }
}