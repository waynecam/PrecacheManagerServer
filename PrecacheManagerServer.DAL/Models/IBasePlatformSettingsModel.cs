using System;
using System.Collections.Generic;
using System.Text;
//using PrecacheManagerServer.DAL.Enums;
using PrecacheManagerServer.Shared.Enums;

namespace PrecacheManagerServer.DAL.Models
{
    public interface IBasePlatformSettingsModel 
    {
        List<string> ConnectionStrings { get; set; }
        Dictionary<ApplicationMode, string> Connections { get; set; }

        Dictionary<string, string> Where { get; set; }

        string Sql { get; set; }
    }
}
