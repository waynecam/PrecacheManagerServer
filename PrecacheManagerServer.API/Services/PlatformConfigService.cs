using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PrecacheManagerServer.Shared.Enums;

namespace PrecacheManagerServer.API.Services
{
    public class PlatformConfigService : IPlatformConfigService
    {
        public Dictionary<ApplicationMode, string> ConnectionStrings { get; private set; }

        public PlatformConfigService()
        {
            ConnectionStrings = new Dictionary<ApplicationMode, string>();
            //ConnectionStrings.Add(ApplicationMode.Australia, $"Connection Timeout=300;Data Source=SYDWINSQLP001;Initial Catalog=PortfolioManagementAUS;persist security info=True;user id=LinkedServerUser;password=7XvAAkG82b6vDECypojf;");
            ConnectionStrings.Add(ApplicationMode.International, $"Connection Timeout=300;Data Source=10.236.234.20\\PortfolioINT;Initial Catalog=PortfolioManagementINT;persist security info=True;Integrated Security=True;");
            ConnectionStrings.Add(ApplicationMode.GermanyMedia, $"Connection Timeout=300;Data Source=10.236.234.20\\PORTFOLIOGER;initial catalog=PortfolioManagementGER;persist security info=True;Integrated Security=True;");



        }
    }
}
