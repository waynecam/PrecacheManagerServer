﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//using PrecacheManagerServer.BLL.Enums;
using PrecacheManagerServer.Shared.Enums;

namespace PrecacheManagerServer.API.Models
{
    public interface IPlatformSettings_old
    {
        Dictionary<ApplicationMode, string> ConnectionStrings { get; set; }
    }
}
