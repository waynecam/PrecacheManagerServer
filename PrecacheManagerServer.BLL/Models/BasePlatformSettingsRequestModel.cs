using System;
using System.Collections.Generic;
using System.Text;
using PrecacheManagerServer.BLL.Enums;

namespace PrecacheManagerServer.BLL.Models
{
   public abstract class BasePlatformSettingsRequestModel : IBasePlatformSettingsRequestModel
    {

        public List<string> ConnectionStrings { get; set; }
        public Dictionary<ApplicationMode, string> Connections { get; set; }

        public string Sql { get; set; }

        public BasePlatformSettingsRequestModel()
        {
            //read these from the config goign forward

            ConnectionStrings = new List<string>();
            Connections = new Dictionary<ApplicationMode, string>();
            //var conn = $"Connection Timeout=300;Data Source=GBR-C-SQL-001J\\PortfolioINT;Initial Catalog=PortfolioManagementINT;persist security info=True;Integrated Security=True;";
            //ConnectionStrings = new List<string>() { conn };
        }

    }
}
