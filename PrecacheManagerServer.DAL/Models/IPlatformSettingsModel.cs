using System;
using System.Collections.Generic;
using System.Text;

namespace PrecacheManagerServer.DAL.Models
{
    public interface IPlatformSettingsModel
    {
        List<string> ConnectionStrings { get; set; }

        Dictionary<string, string> Where { get; set; }
    }
}
