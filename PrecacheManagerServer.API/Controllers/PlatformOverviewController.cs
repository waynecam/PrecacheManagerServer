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
using PrecacheManagerServer.BLL.Enums;
using PrecacheManagerServer.Shared.Models;

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
            new PlatformOverview(){ApplicationMode = ApplicationMode.International,
                PrecacheSites = new List<PrecacheSite>(){new PrecacheSite(){
                    ID =20, ApplicationMode=ApplicationMode.International,
                    Name ="International Clientsite 1",
                    PrecacheSearches = new List<PrecacheSearch>(){ new PrecacheSearch() { id =10} }
                } } },


            new PlatformOverview(){ApplicationMode = ApplicationMode.GermanyMedia,
                PrecacheSites = new List<PrecacheSite>(){new PrecacheSite()
                { ID=21, ApplicationMode=ApplicationMode.GermanyMedia,
                    Name ="Germany ClientSite 1",
                    PrecacheSearches = new List<PrecacheSearch>(){ new PrecacheSearch() { id =10} }
                } } }
        };


        public PlatformOverviewController(IPlatformOverviewService service, IPlatformSettings platformSetting)
        {
            _service = service;
            _platformSettings = platformSetting;
        }



        //[HttpGet]
        //public IEnumerable<PlatformOverview> GetPlatformOverviews()
        //{
        //    return PlatformOverViewCollection;
        //}

        [HttpGet]
        //public async Task<IEnumerable<PlatformOverviewResponseModel>> GetPlatformOverviews()
        public async Task<IResultMessage<IEnumerable<PlatformOverviewResponseModel>>> GetPlatformOverviews()
        {
            //var result = new List<PlatformOverviewResponseModel>();
            var data = new List<PlatformOverviewResponseModel>();

            var result = new ResultMessage<IEnumerable<PlatformOverviewResponseModel>>()
            {
                Success = true
            };



            //foreach (var key in _platformSettings.ConnectionStrings.Keys)
            //{
            //    platformSettingsRequestModel.ConnectionStrings.Add(_platformSettings.ConnectionStrings[key]);
            //}

            try
            {
                foreach (var key in _platformSettings.ConnectionStrings.Keys)
                {
                    var platformSettingsRequestModel = new PlatformSettingsRequestModel();

                    platformSettingsRequestModel.Connections.Add(key, _platformSettings.ConnectionStrings[key]);

                    var r = await _service.GetAsync(platformSettingsRequestModel);

                    data.AddRange(r.ToList());
                }

                result.Data = data;
            }catch (Exception ex)
            {
                result.Success = false;
                result.Error = ex;
                result.FriendlyErrorMessage = "Couldnt get platform Overviews at this time";
            }


            return result;

        }
    }
}