using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PrecacheManagerServer.Enums;

namespace PrecacheManagerServer.API.Models
{
    public interface IPlatformSettings
    {
        Dictionary<ApplicationMode, string> ConnectionStrings { get; set; }
    }
}
