using PrecacheManagerServer.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace PrecacheManagerServer.Shared.Models
{
    public interface IPlatformSettingsQuery : IPlatformSettings
    {
        Dictionary<string, string> Where { get; set; }

        string Sql { get; set; }

    }
}
