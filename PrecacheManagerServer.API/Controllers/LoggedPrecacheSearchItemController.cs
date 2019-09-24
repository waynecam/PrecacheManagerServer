using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PrecacheManagerServer.API.Models;
using PrecacheManagerServer.BLL.Models;
using PrecacheManagerServer.BLL.Services;


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

    }
}