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
    public class PrecacheSiteController : ControllerBase
    {

        private static ConcurrentBag<PrecacheSite> PrecacheSiteCollection = new ConcurrentBag<PrecacheSite>()
        {
            new PrecacheSite() { Name = "Test INT Site", ApplicationMode = ApplicationMode.International },
            new PrecacheSite() { Name = "Test AUS Site", ApplicationMode = ApplicationMode.Australia },
            new PrecacheSite() { Name = "Test GERMANY MEDIA Site", ApplicationMode = ApplicationMode.GermanyMedia }
        };


        [HttpGet]
        public IEnumerable<PrecacheSite> GetSites()
        {
            return PrecacheSiteCollection;
        }


        //[HttpGet]
        //public IEnumerable<Site> GetSites(ApplicationMode appMode)
        //{
        //    var sites = SiteCollection.Where(X => X.ApplicationMode == appMode).Select(x => x);

        //    return sites;
        //}



    }
}