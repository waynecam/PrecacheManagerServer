using System;
using System.Collections.Generic;
using System.Text;

namespace PrecacheManagerServer.DAL.Models
{
    public class PlatformSettingsModel : IPlatformSettingsModel
    {
        public PlatformSettingsModel()
        {
            Where = new Dictionary<string, string>();
            ConnectionStrings = new List<string>();
        }
        public List<string> ConnectionStrings { get; set; }

        public Dictionary<string, string> Where { get; set; }

        public string Sql { get; set; }
    }
}
