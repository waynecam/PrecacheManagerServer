using PrecacheManagerServer.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace PrecacheManagerServer.Shared.Models
{
    public class PlatformSettingsQueryAddUpdate<T> : IPlatformSettingsQueryAddUpdate<T>
    {
        public List<SqlParameter> SqlParameters { get; set; }
        public List<T> Data { get; set; }
        public Dictionary<string, string> Where { get; set; }
        public string Sql { get; set; }
        public Dictionary<ApplicationMode, string> ConnectionStrings { get ; set; }
    }
}
