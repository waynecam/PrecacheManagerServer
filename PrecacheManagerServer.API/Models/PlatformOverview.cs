using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//using PrecacheManagerServer.Enums;
//using PrecacheManagerServer.BLL.Enums;
using PrecacheManagerServer.Shared.Enums;
using PrecacheManagerServer.Shared.Enums.Extensions;

namespace PrecacheManagerServer.Models
{
    public class PlatformOverview
    {

        public ApplicationMode ApplicationMode { get; set; }

        public string PlatformDescription { get { return ApplicationMode.GetAttribute<ApplicationModeFriendlyDescriptionAttribute>().ApplicationModeFriendlyDescription; } }

        public List<PrecacheSite> PrecacheSites { get; set; }



    }
}
