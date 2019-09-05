using System;
using System.Collections.Generic;
using System.Text;

namespace PrecacheManagerServer.BLL.Models
{
    public class PlatformSettingsRequestModel
    {
        public List<string> ConnectionStrings;

        public PlatformSettingsRequestModel()
        {
            //read these from the config goign forward

            ConnectionStrings = new List<string>();
            //var conn = $"Connection Timeout=300;Data Source=GBR-C-SQL-001J\\PortfolioINT;Initial Catalog=PortfolioManagementINT;persist security info=True;Integrated Security=True;";
            //ConnectionStrings = new List<string>() { conn };
        }

    }
}
