using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PrecacheManagerServer.Enums;

namespace PrecacheManagerServer.Models
{
    public class PlatformOverview
    {

        public ApplicationMode ApplicationMode { get; set; }

        public string PlatformDescription { get { return ApplicationMode.ToString(); } }

        public List<PrecacheSite> PrecacheSites { get; set; }



    }
}
