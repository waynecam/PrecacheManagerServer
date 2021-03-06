﻿using System;
using System.Collections.Generic;
using System.Text;
//using PrecacheManagerServer.BLL.Enums;
using PrecacheManagerServer.Shared.Enums;

namespace PrecacheManagerServer.BLL.Models
{
   public abstract class BasePlatformSettingsRequestModel : IBasePlatformSettingsRequestModel
    {

        public List<string> Connections { get; set; }
        public Dictionary<ApplicationMode, string> ConnectionStrings { get; set; }

        public string Sql { get; set; }

        public Dictionary<string, string> Where { get; set; }

        public BasePlatformSettingsRequestModel()
        {
            //read these from the config goign forward

            Connections = new List<string>();
            ConnectionStrings = new Dictionary<ApplicationMode, string>();
            Where = new Dictionary<string, string>();
            //var conn = $"Connection Timeout=300;Data Source=GBR-C-SQL-001J\\PortfolioINT;Initial Catalog=PortfolioManagementINT;persist security info=True;Integrated Security=True;";
            //ConnectionStrings = new List<string>() { conn };
        }

    }
}
