﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PrecacheManagerServer.DAL.Enums;

namespace PrecacheManagerServer.API.Models
{
    public class PlatformSettings : IPlatformSettings
    {

        public Dictionary<ApplicationMode, string> ConnectionStrings { get; set; }



        public PlatformSettings()
        {
            ConnectionStrings = new Dictionary<ApplicationMode, string>();
            ConnectionStrings.Add(ApplicationMode.International, $"Connection Timeout=300;Data Source=GBR-C-SQL-001J\\PortfolioINT;Initial Catalog=PortfolioManagementINT;persist security info=True;Integrated Security=True;");
            ConnectionStrings.Add(ApplicationMode.GermanyMedia, $"Connection Timeout=300;Data Source=GBR-P-SQL-011\\PortfolioDEU;Initial Catalog=PortfolioManagementTMC;persist security info=True;Integrated Security=True;");
        }

    }
}
