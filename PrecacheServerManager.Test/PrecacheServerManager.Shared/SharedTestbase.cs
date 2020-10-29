using PrecacheManagerServer.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace PrecacheServerManager.Test.Shared
{
    public class SharedTestbase
    {
        protected Dictionary<ApplicationMode, string> GetTestPlatformConnStrings()
        {
            var connectionStrings = new Dictionary<ApplicationMode, string>();
            //ConnectionStrings.Add(ApplicationMode.Australia, $"Connection Timeout=300;Data Source=SYDWINSQLP001;Initial Catalog=PortfolioManagementAUS;persist security info=True;user id=LinkedServerUser;password=7XvAAkG82b6vDECypojf;");
            connectionStrings.Add(ApplicationMode.International, $"Connection Timeout=300;Data Source=10.236.234.20\\PortfolioINT;Initial Catalog=PortfolioManagementINT;persist security info=True;Integrated Security=True;");
            connectionStrings.Add(ApplicationMode.GermanyMedia, $"Connection Timeout=300;Data Source=10.236.234.20\\PORTFOLIOGER;initial catalog=PortfolioManagementGER;persist security info=True;Integrated Security=True;");

            return connectionStrings;
        }
    }
}
