using PrecacheManagerServer.Shared.Enums.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrecacheManagerServer.Shared.Enums
{
    public enum ApplicationMode
    {
        [ApplicationModeId(2), ApplicationModeFriendlyDescription("Australia")]
        Australia = 0,
        //[ApplicationModeId(4), ApplicationModeFriendlyDescription("Germany")]
        //GermanyMedia = 2,
        [ApplicationModeId(2), ApplicationModeFriendlyDescription("International")]
        International = 2,

        [ApplicationModeId(4), ApplicationModeFriendlyDescription("Germany")]
        GermanyMedia = 4,
    }
}
