using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PrecacheManagerServer.API.Models;
using PrecacheManagerServer.BLL.Models;
using PrecacheManagerServer.BLL.Services;
using PrecacheManagerServer.Enums;

namespace PrecacheManagerServer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrecacheSearchController : ControllerBase
    {
        IPrecacheSearchService _service;
        IPlatformSettings _platformSettings;
        
        public PrecacheSearchController(IPrecacheSearchService service, IPlatformSettings platformSettings)
        {
            _service = service;
            _platformSettings = platformSettings;
        }


        [HttpGet]
        public async Task<IEnumerable<PrecacheSearchResponseModel>> Get()
        {

            var platformSettingsRequestModel = new PlatformSettingsRequestModel();

            foreach(var key in _platformSettings.ConnectionStrings.Keys)
            {
                platformSettingsRequestModel.ConnectionStrings.Add(_platformSettings.ConnectionStrings[key]);
            }

            
            return await _service.GetAsync(platformSettingsRequestModel);
        }
    }
}