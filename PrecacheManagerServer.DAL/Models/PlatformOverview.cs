using System;
using System.Collections.Generic;
using System.Text;
using PrecacheManagerServer.DAL.Enums;

namespace PrecacheManagerServer.DAL.Models
{
    public class PlatformOverview :BaseEntity
    {
            public ApplicationMode ApplicationMode { get; set; }

            public string PlatformDescription { get { return ApplicationMode.ToString(); } }

            public List<PrecacheSite> PrecacheSites { get; set; }
    }
}
