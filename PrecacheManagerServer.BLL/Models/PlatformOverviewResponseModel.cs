using System;
using System.Collections.Generic;
using System.Text;
using PrecacheManagerServer.DAL.Enums;
using PrecacheManagerServer.DAL.Models;

namespace PrecacheManagerServer.BLL.Models
{
    public class PlatformOverviewResponseModel
    {
        public ApplicationMode ApplicationMode { get; set; }

        public string PlatformDescription { get { return ApplicationMode.ToString(); } }

        public List<PrecacheSite> PrecacheSites { get; set; }
    }
}
