using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PrecacheManagerServer.API.Models;
using PrecacheManagerServer.BLL.Models;
using PrecacheManagerServer.BLL.Services;
using PrecacheManagerServer.BLL.Enums.Extensions;


namespace PrecacheManagerServer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoggedPrecacheSearchItemController : ControllerBase
    {

        ILoggedPrecacheSearchItemService _service;
        IPlatformSettings _platformSettings;

        public LoggedPrecacheSearchItemController(ILoggedPrecacheSearchItemService service, IPlatformSettings platformSetting)
        {
            _service = service;
            _platformSettings = platformSetting;
        }


        [HttpGet]
        public async Task<IEnumerable<LoggedPrecacheSearchItemResponseModel>> Get()
        {
            var result = new List<LoggedPrecacheSearchItemResponseModel>();

            foreach (var key in _platformSettings.ConnectionStrings.Keys)
            {
                var platformSettingsRequestModel = new PlatformSettingsRequestModel();

                platformSettingsRequestModel.Connections.Add(key, _platformSettings.ConnectionStrings[key]);

                var r = await _service.GetAsync(platformSettingsRequestModel);



                result.AddRange(r.ToList().Take(500));
            }

            return result;

        }

        [HttpGet]
        [Route("[action]/{applicationMode}")]
        public async Task<IEnumerable<LoggedPrecacheSearchItemResponseModel>> GetByApplicationMode(int applicationMode)
        {
            var result = new List<LoggedPrecacheSearchItemResponseModel>();


            foreach (var key in _platformSettings.ConnectionStrings.Keys)
            {

                if ((int)key.GetAttribute<ApplicationModeIdAttribute>().ApplicationModeId == applicationMode)
                {
                    var platformSettingsRequestModel = new PlatformSettingsRequestModel();

                    platformSettingsRequestModel.Connections.Add(key, _platformSettings.ConnectionStrings[key]);

                    var r = await _service.GetAsync(platformSettingsRequestModel);

                    result.AddRange(r.ToList());
                    //result.AddRange(r.ToList());
                }
            }

            return result;
        }

        [HttpGet]
        [Route("[action]/{applicationMode}/{siteId}")]
        public async Task<IEnumerable<LoggedPrecacheSearchItemResponseModel>> GetByApplicationModeAndSiteId(int applicationMode, int siteId)
        {
            var result = new List<LoggedPrecacheSearchItemResponseModel>();


            foreach (var key in _platformSettings.ConnectionStrings.Keys)
            {

                if ((int)key.GetAttribute<ApplicationModeIdAttribute>().ApplicationModeId == applicationMode)
                {
                    var platformSettingsRequestModel = new PlatformSettingsRequestModel();

                    platformSettingsRequestModel.Connections.Add(key, _platformSettings.ConnectionStrings[key]);

                    var r = await _service.GetAsync(platformSettingsRequestModel);

                    result.AddRange(r.ToList().Where(x => x.SiteId == siteId));
                    //result.AddRange(r.ToList());
                }
            }

            var testloggedPrecacheSarchItem = new LoggedPrecacheSearchItemResponseModel()
            {
                ApplicationMode = 2,
                HomePageSearchType = 3,
                PrecacheIntegrityKey = Guid.NewGuid(),
                ErrorMessage = "This is a test",
                SearchId = 1,
                SearchVersion = 2,
                AreaNo = 5


            };

            result.Add(testloggedPrecacheSarchItem);

            return result;
        }

    }
}