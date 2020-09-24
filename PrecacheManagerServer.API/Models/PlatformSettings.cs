using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//using PrecacheManagerServer.BLL.Enums;
using PrecacheManagerServer.Shared.Enums;

namespace PrecacheManagerServer.API.Models
{
    public class PlatformSettings : IPlatformSettings
    {

        public Dictionary<ApplicationMode, string> ConnectionStrings { get; set; }



        public PlatformSettings()
        {
            ConnectionStrings = new Dictionary<ApplicationMode, string>();
            //ConnectionStrings.Add(ApplicationMode.International, $"Connection Timeout=300;Data Source=GBR-C-SQL-001J\\PortfolioINT;Initial Catalog=PortfolioManagementINT;persist security info=True;Integrated Security=True;");
            //ConnectionStrings.Add(ApplicationMode.GermanyMedia, $"Connection Timeout=300;Data Source=GBR-P-SQL-011\\PortfolioDEU;Initial Catalog=PortfolioManagementTMC;persist security info=True;Integrated Security=True;");
            //ConnectionStrings.Add(ApplicationMode.Germany, $"Connection Timeout=300;Data Source=GBR-P-SQL-011\\PortfolioDEU;Initial Catalog=PortfolioManagementDe;persist security info=True;Integrated Security=True;");

            //ConnectionStrings.Add(ApplicationMode.GermanyMedia, $"Connection Timeout=300;Data Source=DUBWINSQLP001\\PORTFOLIOGER;initial catalog=PortfolioManagementGER;persist security info=True;Integrated Security=True;");
            //ConnectionStrings.Add(ApplicationMode.Australia, $"Connection Timeout=300;Data Source=SYDWINSQLP001;Initial Catalog=PortfolioManagementAUS;persist security info=True;user id=LinkedServerUser;password=7XvAAkG82b6vDECypojf;");
            ConnectionStrings.Add(ApplicationMode.International, $"Connection Timeout=300;Data Source=10.236.234.20\\PortfolioINT;Initial Catalog=PortfolioManagementINT;persist security info=True;Integrated Security=True;");
            ConnectionStrings.Add(ApplicationMode.GermanyMedia, $"Connection Timeout=300;Data Source=10.236.234.20\\PORTFOLIOGER;initial catalog=PortfolioManagementGER;persist security info=True;Integrated Security=True;");



        }
    }
}
