﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PrecacheManagerServer.Models;

namespace PrecacheManagerServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlatformOverviewController : ControllerBase
    {
        private ConcurrentBag<PlatformOverview> PlatformOverViewCollection = new ConcurrentBag<PlatformOverview>()
        {
            new PlatformOverview(){ApplicationMode = Enums.ApplicationMode.International,
                Sites = new List<Site>(){new Site(){ ID=20, ApplicationMode=Enums.ApplicationMode.International, Name="International Clientsite 1" }} },
            new PlatformOverview(){ApplicationMode = Enums.ApplicationMode.GermanyMedia,
                Sites = new List<Site>(){new Site(){ ID=21, ApplicationMode=Enums.ApplicationMode.GermanyMedia,Name="Germany ClientSite 1" }} }
        };


        [HttpGet]

        public IEnumerable<PlatformOverview> GetPlatformOverviews()
        {
            return PlatformOverViewCollection;
        }
    }
}