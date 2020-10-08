using PrecacheManagerServer.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace PrecacheManagerServer.Shared.Models
{
    public class PlatformSettingsQuery : IPlatformSettingsQuery
    {
        public Dictionary<string, string> Where { get;set; }
        public string Sql { get; set; }
        public Dictionary<ApplicationMode, string> ConnectionStrings { get; set; }
    }
}
