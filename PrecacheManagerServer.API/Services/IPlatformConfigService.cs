using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PrecacheManagerServer.Shared.Enums;

namespace PrecacheManagerServer.API.Services
{
    public interface IPlatformConfigService
    {
        Dictionary<ApplicationMode, string> ConnectionStrings { get; }
    }
}
