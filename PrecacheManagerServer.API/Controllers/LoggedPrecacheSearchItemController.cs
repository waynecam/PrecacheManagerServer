using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PrecacheManagerServer.API.Models;
using PrecacheManagerServer.BLL.Models;
using PrecacheManagerServer.BLL.Services;
//using PrecacheManagerServer.BLL.Enums.Extensions;
using PrecacheManagerServer.Shared.Models;
using PrecacheManagerServer.Shared.Enums.Extensions;
using PrecacheManagerServer.Controllers;

namespace PrecacheManagerServer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoggedPrecacheSearchItemController : UserController
    {

        ILoggedPrecacheSearchItemService _service;
        IPlatformSettings _platformSettings;

        public LoggedPrecacheSearchItemController(IServiceProvider serviceProvider, ILoggedPrecacheSearchItemService service, IPlatformSettings platformSetting) :base(serviceProvider)
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

                platformSettingsRequestModel.ConnectionStrings.Add(key, _platformSettings.ConnectionStrings[key]);

                var r = await _service.GetAsync(platformSettingsRequestModel);



                result.AddRange(r.ToList().Take(500));
            }

            return result;

        }

        [HttpGet]
        [Route("[action]/{applicationModeId}")]
        public async Task<IResultMessage<IEnumerable<LoggedPrecacheSearchItemResponseModel>>> GetByApplicationMode(int applicationModeId)
        {
            var data = new List<LoggedPrecacheSearchItemResponseModel>();

            var result = new ResultMessage<IEnumerable<LoggedPrecacheSearchItemResponseModel>>()
            {
                Success = true
            };
            try
            {

                foreach (var key in _platformSettings.ConnectionStrings.Keys)
                {

                    if ((int)key.GetAttribute<ApplicationModeIdAttribute>().ApplicationModeId == applicationModeId)
                    {
                        var platformSettingsRequestModel = new PlatformSettingsRequestModel();

                        platformSettingsRequestModel.ConnectionStrings.Add(key, _platformSettings.ConnectionStrings[key]);

                        var r = await _service.GetAsync(platformSettingsRequestModel);

                        data.AddRange(r.ToList());
                        //result.AddRange(r.ToList());
                    }
                }

                result.Data = data;
                result.Success = true;

            }catch(Exception ex)
            {
                result.Success = false;
                result.FriendlyErrorMessage = "Failed to fetch Failed Precache Searches";
                result.Error = ex;
            }




           

            return result;
        }

        [HttpGet]
        [Route("[action]/{applicationModeId}/{siteId}")]
        public async Task<IResultMessage<List<LoggedPrecacheSearchItemResponseModel>>> GetByApplicationModeAndSiteId(int applicationModeId, int siteId)
        {
            //var result = new List<LoggedPrecacheSearchItemResponseModel>();

            var result = new ResultMessage<List<LoggedPrecacheSearchItemResponseModel>>()
            {
                Success = true
            };

            var data = new List<LoggedPrecacheSearchItemResponseModel>();

            foreach (var key in CurrentUser.PlatformSettings.ConnectionStrings.Keys)
            {

                if ((int)key.GetAttribute<ApplicationModeIdAttribute>().ApplicationModeId == applicationModeId)
                {
                    var platformSettingsRequestModel = new PlatformSettingsRequestModel();

                    platformSettingsRequestModel.ConnectionStrings.Add(key, CurrentUser.PlatformSettings.ConnectionStrings[key]);

                    var r = await _service.GetAsync(platformSettingsRequestModel);



                    data.AddRange(r.ToList().Where(x => x.SiteId == siteId));
                    //result.AddRange(r.ToList());
                }
            }



            var testloggedPrecacheSarchItem = new LoggedPrecacheSearchItemResponseModel()
            {
                ApplicationMode = 2,
                HomePageSearchType = 3,
                PrecacheIntegrityKey = new Guid("1D798C43-89B2-4A32-B302-97F4AA1940DD"),
                ErrorMessage = "This is a test",
                SearchId = 1,
                SearchVersion = 2,
                AreaNo = 5


            };



            //result.Add(testloggedPrecacheSarchItem);

            result.Data = new List<LoggedPrecacheSearchItemResponseModel> { testloggedPrecacheSarchItem };

            //result.Data = data;



            return result;
        }

    }
}