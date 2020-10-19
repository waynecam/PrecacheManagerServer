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
//using PrecacheManagerServer.BLL.Enums.Extensions;
using PrecacheManagerServer.Shared.Enums.Extensions;
using PrecacheManagerServer.Shared.Models;

namespace PrecacheManagerServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrecacheSearchController : UserController
    {
        IPrecacheSearchService _service;
        IPlatformSettings _platformSettings;
        
        public PrecacheSearchController(IServiceProvider serviceProvider, IPrecacheSearchService service, IPlatformSettings platformSettings) : base(serviceProvider)
        {
            _service = service;
            _platformSettings = platformSettings;
        }


        [HttpGet]
        public async Task<IEnumerable<PrecacheSearchResponseModel>> Get()
        {
            var result = new List<PrecacheSearchResponseModel>();

            //foreach (var key in _platformSettings.ConnectionStrings.Keys)
            foreach (var key in CurrentUser.PlatformSettings.ConnectionStrings.Keys)
                {
                var platformSettingsRequestModel = new PlatformSettingsRequestModel();

                platformSettingsRequestModel.ConnectionStrings.Add(key, CurrentUser.PlatformSettings.ConnectionStrings[key]);

                var r = await _service.GetAsync(platformSettingsRequestModel);

                result.AddRange(r.ToList().Take(3));
            }
            
            return result;
        }

        [HttpGet("{id}")]
        public async Task<PrecacheSearchResponseModel> Get([Required]int id)
        {

            var platformSettingsRequestModel = new PlatformSettingsRequestModel();

            //foreach (var key in _platformSettings.ConnectionStrings.Keys)
            foreach (var key in CurrentUser.PlatformSettings.ConnectionStrings.Keys)
                {
                platformSettingsRequestModel.Connections.Add(CurrentUser.PlatformSettings.ConnectionStrings[key]);
            }

            return await _service.GetById(platformSettingsRequestModel, id);
        }

        [HttpGet]
        [Route("[action]/{applicationMode}")]
        public async Task<IEnumerable<PrecacheSearchResponseModel>> GetByApplicationMode(int applicationMode)
        {
            var result = new List<PrecacheSearchResponseModel>();


            //foreach (var key in _platformSettings.ConnectionStrings.Keys)
            foreach (var key in CurrentUser.PlatformSettings.ConnectionStrings.Keys)
            {

                if ((int)key.GetAttribute<ApplicationModeIdAttribute>().ApplicationModeId == applicationMode)
                {
                    var platformSettingsRequestModel = new PlatformSettingsRequestModel();

                    platformSettingsRequestModel.ConnectionStrings.Add(key, CurrentUser.PlatformSettings.ConnectionStrings[key]);

                    var r = await _service.GetAsync(platformSettingsRequestModel);

                    result.AddRange(r.ToList());
                    //result.AddRange(r.ToList());
                }
            }

            return result;
        }

        [HttpGet]
        [Route("[action]/{applicationModeId}/{siteId}")]
        //public async Task<IEnumerable<PrecacheSearchResponseModel>> GetByApplicationModeAndSiteId(int applicationMode, int siteId)
        public async Task<IResultMessage<IEnumerable<PrecacheSearchResponseModel>>> GetByApplicationModeAndSiteId(int applicationModeId, int siteId)
        {
            var data = new List<PrecacheSearchResponseModel>();
            var result = new ResultMessage<IEnumerable<PrecacheSearchResponseModel>>()
            {
                Success = true
            };

            try
            {

                //foreach (var key in _platformSettings.ConnectionStrings.Keys)
                foreach (var key in CurrentUser.PlatformSettings.ConnectionStrings.Keys)
                    {

                    if ((int)key.GetAttribute<ApplicationModeIdAttribute>().ApplicationModeId == applicationModeId)
                    {
                        var platformSettingsRequestModel = new PlatformSettingsRequestModel();

                        platformSettingsRequestModel.ConnectionStrings.Add(key, CurrentUser.PlatformSettings.ConnectionStrings[key]);


                        platformSettingsRequestModel.Where.Add("siteid", siteId.ToString());

                        var r = await _service.GetAsync(platformSettingsRequestModel);

                         //data.AddRange(r.ToList().Where(x => x.SiteId == siteId));
                        data.AddRange(r.ToList());
                        //result.AddRange(r.ToList());
                    }
                }
                result.Success = true;
                result.Data = data;

            }
            catch(Exception ex)
            {
                result.Success = false;
                result.FriendlyErrorMessage = "Failed to fetch Precache Searches";
                result.Error = ex;
            }

            //return data;

            return result;
        }

    }
}