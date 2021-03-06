﻿using System;
using System.Collections.Generic;
using System.Text;
//using PrecacheManagerServer.DAL.Enums;
using PrecacheManagerServer.Shared.Enums;
namespace PrecacheManagerServer.DAL.Models
{
    public abstract class BasePlatformSettingsModel : IBasePlatformSettingsModel
    {
        public BasePlatformSettingsModel()
        {
            Where = new Dictionary<string, string>();
            //ConnectionStrings = new List<string>();
            Connections = new Dictionary<ApplicationMode, string>();
        }

        //public List<string> ConnectionStrings { get; set; }
        public Dictionary<ApplicationMode, string> Connections { get; set; }

        public Dictionary<string, string> Where { get; set; }

        public string Sql { get; set; }
    }
}
