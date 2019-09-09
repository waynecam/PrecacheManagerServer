using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PrecacheManagerServer.API.Models;
using PrecacheManagerServer.BLL.Models;
using PrecacheManagerServer.BLL.Services;
using PrecacheManagerServer.Models;

namespace PrecacheManagerServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlatformOverviewController : ControllerBase
    {

        IPlatformOverviewService _service;
        IPlatformSettings _platformSettings;


        private ConcurrentBag<PlatformOverview> PlatformOverViewCollection = new ConcurrentBag<PlatformOverview>()
        {
            new PlatformOverview(){ApplicationMode = Enums.ApplicationMode.International,
                PrecacheSites = new List<PrecacheSite>(){new PrecacheSite(){ ID=20, ApplicationMode=Enums.ApplicationMode.International, Name="International Clientsite 1" }} },
            new PlatformOverview(){ApplicationMode = Enums.ApplicationMode.GermanyMedia,
                PrecacheSites = new List<PrecacheSite>(){new PrecacheSite(){ ID=21, ApplicationMode=Enums.ApplicationMode.GermanyMedia,Name="Germany ClientSite 1" }} }
        };


        public PlatformOverviewController(IPlatformOverviewService service, IPlatformSettings platformSetting)
        {
            _service = service;
            _platformSettings = platformSetting;
        }



        //[HttpGet]
        //public IEnumerable<PlatformOverview> GetPlatformOverviews()
        //{
        //    //return PlatformOverViewCollection;
        //}

        [HttpGet]
        public async Task<IEnumerable<PlatformOverviewResponseModel>> GetPlatformOverviews()
        {

            var platformSettingsRequestModel = new PlatformSettingsRequestModel();

            foreach (var key in _platformSettings.ConnectionStrings.Keys)
            {
                platformSettingsRequestModel.ConnectionStrings.Add(_platformSettings.ConnectionStrings[key]);
            }

            return await _service.GetAsync(platformSettingsRequestModel);
        
        }
    }
}