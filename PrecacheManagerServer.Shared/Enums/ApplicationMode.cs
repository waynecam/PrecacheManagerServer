using PrecacheManagerServer.Shared.Enums.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrecacheManagerServer.Shared.Enums
{
    public enum ApplicationMode
    {
        [ApplicationModeId(2)]
        Australia = 0,
        [ApplicationModeId(1)]
        Germany = 1,
        [ApplicationModeId(4)]
        GermanyMedia = 2,
        [ApplicationModeId(2)]
        International = 3
    }
}
