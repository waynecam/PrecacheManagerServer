using System;
using System.Collections.Generic;
using System.Text;
//using PrecacheManagerServer.DAL.Enums;
using PrecacheManagerServer.Shared.Enums;
using PrecacheManagerServer.Shared.Enums.Extensions;
using PrecacheManagerServer.DAL.Models;


namespace PrecacheManagerServer.BLL.Models
{
    public class PlatformOverviewResponseModel
    {
        public PlatformOverviewResponseModel()
        {
            PrecacheSites = new List<PrecacheSite>();
        }

        public ApplicationMode ApplicationMode { get; set; }

        public string PlatformDescription { get { return ApplicationMode.GetAttribute<ApplicationModeFriendlyDescriptionAttribute>().ApplicationModeFriendlyDescription; } }

        public List<PrecacheSite> PrecacheSites { get; set; }
    }
}
