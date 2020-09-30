using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrecacheManagerServer.Shared.Enums
{
    public enum HomePageSearchType
    {
        SystemHomepagePrecache = 1,
        SystemHomepageWithCatagoryHierachyPrecache = 2,
        UserDefinedHomepagePrecache = 3,
        DynamicPrecache = 4
    }
}
