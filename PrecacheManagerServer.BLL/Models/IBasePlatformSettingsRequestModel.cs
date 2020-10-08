using System.Collections.Generic;
//using PrecacheManagerServer.BLL.Enums;
using PrecacheManagerServer.Shared.Enums;

namespace PrecacheManagerServer.BLL.Models
{
    interface IBasePlatformSettingsRequestModel
    {

         List<string> Connections { get; set; }
         Dictionary<ApplicationMode, string> ConnectionStrings { get; set; }

        string Sql { get; set; }

        Dictionary<string, string> Where { get; set; }
    }
}