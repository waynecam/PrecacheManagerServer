using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PrecacheManagerServer.Enums;
using PrecacheManagerServer.Models;

namespace PrecacheManagerServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SitesController : ControllerBase
    {

        private static ConcurrentBag<Site> SiteCollection = new ConcurrentBag<Site>()
        {
            new Site() { Name = "Test INT Site", ApplicationMode = ApplicationMode.International },
            new Site() { Name = "Test AUS Site", ApplicationMode = ApplicationMode.Australia },
            new Site() { Name = "Test GERMANY MEDIA Site", ApplicationMode = ApplicationMode.GermanyMedia }
        };


        [HttpGet]
        public IEnumerable<Site> GetSites()
        {
            return SiteCollection;
        }


        //[HttpGet]
        //public IEnumerable<Site> GetSites(ApplicationMode appMode)
        //{
        //    var sites = SiteCollection.Where(X => X.ApplicationMode == appMode).Select(x => x);

        //    return sites;
        //}



    }
}