using System;
using System.Collections.Generic;
using System.Text;

namespace PrecacheManagerServer.DAL.Models
{
    public class PlatformSettingsModel : IPlatformSettingsModel
    {
        public List<string> ConnectionStrings { get; set; }
    }
}
