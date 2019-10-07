using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PrecacheManagerServer.BLL.Enums.Extensions;

namespace PrecacheManagerServer.BLL.Enums
{
    public enum ApplicationMode
    {
        [ApplicationModeId(2)]
        Australia = 0,
        [ApplicationModeId(1)]
        Germany = 1,
        [ApplicationModeId(2)]
        GermanyMedia = 2,
        [ApplicationModeId(2)]
        International = 3
    }
}
