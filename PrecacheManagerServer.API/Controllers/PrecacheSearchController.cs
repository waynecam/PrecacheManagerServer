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
        
        public PrecacheSearchController(IPrecacheSearchService service)
        {
            _service = service;
        }


        [HttpGet]
        public async Task<IEnumerable<PrecacheSearchResponseModel>> Get()
        {

            var platformSettingsRequestModel = new PlatformSettingsRequestModel();

            var conn = $"Connection Timeout=300;Data Source=GBR-C-SQL-001J\\PortfolioINT;Initial Catalog=PortfolioManagementINT;persist security info=True;Integrated Security=True;";
            platformSettingsRequestModel.ConnectionStrings = new List<string>() { conn };
            return await _service.GetAsync(platformSettingsRequestModel);
        }
    }
}