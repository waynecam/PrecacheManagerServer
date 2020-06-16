using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PrecacheManagerServer.API.Models;
using PrecacheManagerServer.BLL.Services;
using PrecacheManagerServer.BLL.Models;
using System.ComponentModel.DataAnnotations;
using PrecacheManagerServer.BLL.Enums.Extensions;
using PrecacheManagerServer.Shared.Models;

namespace PrecacheManagerServer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrecacheSearchItemsCreatedController : ControllerBase
    {

        IPrecacheSearchItemsCreatedService _service;
        IPlatformSettings _platformSettings;


        public PrecacheSearchItemsCreatedController(IPrecacheSearchItemsCreatedService service, IPlatformSettings platformSetting)
        {
            _service = service;
            _platformSettings = platformSetting;
        }


        [HttpGet]
        public async Task<IEnumerable<PrecacheSearchItemsCreatedResponseModel>> Get()
        {

            var result = new List<PrecacheSearchItemsCreatedResponseModel>();


            foreach (var key in _platformSettings.ConnectionStrings.Keys)
            {
                var platformSettingsRequestModel = new PlatformSettingsRequestModel();

                platformSettingsRequestModel.Connections.Add(key, _platformSettings.ConnectionStrings[key]);

                var r = await _service.GetAsync(platformSettingsRequestModel);

                result.AddRange(r.ToList());
            }

            return result;
        }

        [HttpGet("{id}")]
        public async Task<PrecacheSearchItemsCreatedResponseModel> Get([Required]int id)
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
        public async Task<IResultMessage<IEnumerable<PrecacheSearchItemsCreatedResponseModel>>> GetByApplicationMode(int applicationMode)
        {
            var data = new List<PrecacheSearchItemsCreatedResponseModel>();
            var result = new ResultMessage<IEnumerable<PrecacheSearchItemsCreatedResponseModel>>()
            {
                Success = true
            };

            try
            {
                foreach (var key in _platformSettings.ConnectionStrings.Keys)
                {

                    if ((int)key.GetAttribute<ApplicationModeIdAttribute>().ApplicationModeId == applicationMode)
                    {
                        var platformSettingsRequestModel = new PlatformSettingsRequestModel();

                        platformSettingsRequestModel.Connections.Add(key, _platformSettings.ConnectionStrings[key]);

                        var r = await _service.GetAsync(platformSettingsRequestModel);

                        data.AddRange(r.ToList());
                        //result.AddRange(r.ToList());
                    }
                }

                result.Success = true;
                result.Data = data;
            }catch(Exception ex)
            {
                result.Success = false;
                result.Error = ex;
                result.FriendlyErrorMessage = "Couldnt get Precachesearchitems Created at this time";
            }

            return result;
        }




    }
}