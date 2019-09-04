using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PrecacheManagerServer.Enums;

namespace PrecacheManagerServer.API.Models
{
    public class PlatformSettings
    {

        Dictionary<ApplicationMode, string> ConnectionStrings { get; set; }



        public PlatformSettings()
        {
            ConnectionStrings.Add(ApplicationMode.International, "INTERNATIONAL_CONNECTIONSTRING");
            ConnectionStrings.Add(ApplicationMode.GermanyMedia  , "TMC_CONNECTIONSTRING");
        }

    }
}
