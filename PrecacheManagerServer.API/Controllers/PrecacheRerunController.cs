using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PrecacheManagerServer.API.Models;
using PrecacheManagerServer.BLL.Services;

namespace PrecacheManagerServer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrecacheRerunController : ControllerBase
    {
        IPrecacheSearchService _service;
        IPlatformSettings _platformSettings;

        public PrecacheRerunController(IPrecacheSearchService service, IPlatformSettings platformSettings)
        {
            _service = service;
            _platformSettings = platformSettings;
        }

        [HttpPost]
        [Route("[action]/{applicationMode}")]
        public void RerunFailedPrecacheSearch(int applicationMode, [FromBody] PrecacheRerun precacheRerun)
        {
            if (!ModelState.IsValid)
            {
                throw new HttpRequestException();
            }

        }
    }
}