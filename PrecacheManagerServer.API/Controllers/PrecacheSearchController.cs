using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PrecacheManagerServer.BLL.Models;

namespace PrecacheManagerServer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrecacheSearchController : ControllerBase
    {
        BLL.Services.IPrecacheSearchService _service;
        public PrecacheSearchController(BLL.Services.IPrecacheSearchService service)
        {
            _service = service;
        }


        [HttpGet]
        public async Task<IEnumerable<PrecacheSearchResponseModel>> Get()
        {
            return await _service.GetAsync();
        }
    }
}