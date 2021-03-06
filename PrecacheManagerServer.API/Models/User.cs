﻿//using PrecacheManagerServer.BLL.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PrecacheManagerServer.Shared.Enums;
using PrecacheManagerServer.Shared.Models;


namespace PrecacheManagerServer.API.Models
{
    public class User
    {
        public string UserName { get; set; }
        public string Password { get; set; }

        public IEnumerable<ApplicationMode> ApplicationModes { get; set; }

        public IPlatformSettings PlatformSettings { get; set; }
    }
}
