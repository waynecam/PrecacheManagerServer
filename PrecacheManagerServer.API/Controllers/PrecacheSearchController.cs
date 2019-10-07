using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
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

            var result = new List<PrecacheSearchResponseModel>();
            

            foreach (var key in _platformSettings.ConnectionStrings.Keys)
            {
                var platformSettingsRequestModel = new PlatformSettingsRequestModel();

                platformSettingsRequestModel.Connections.Add(key, _platformSettings.ConnectionStrings[key]);

                var r = await _service.GetAsync(platformSettingsRequestModel);

                result.AddRange(r.ToList().Take(3));
            }

            return result;
        }

        [HttpGet("{id}")]
        public async Task<PrecacheSearchResponseModel> Get([Required]int id)
        {

            var platformSettingsRequestModel = new PlatformSettingsRequestModel();

            foreach (var key in _platformSettings.ConnectionStrings.Keys)
            {
                platformSettingsRequestModel.ConnectionStrings.Add(_platformSettings.ConnectionStrings[key]);
            }

            return await _service.GetById(platformSettingsRequestModel, id);
        }

        [HttpGet]
        [Route("[action]/{applicationMode}")]
        public async Task<IEnumerable<PrecacheSearchResponseModel>> GetByApplicationMode(int applicationMode)
        {
            var result = new List<PrecacheSearchResponseModel>();


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
        public async Task<IEnumerable<PrecacheSearchResponseModel>> GetByApplicationModeAndSiteId(int applicationMode, int siteId)
        {
            var result = new List<PrecacheSearchResponseModel>();


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

            return result;
        }

    }
}