using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using PrecacheManagerServer.DAL.Enums;

namespace PrecacheManagerServer.DAL.Models
{
    public class PlatformSettingsModelAddOrUpdate <T> : BasePlatformSettingsModel, IPlatformSettingsModelAddOrUpdate <T>
    {
      public List<SqlParameter> SqlParameters { get; set; }

      public List<T> Data { get; set; }
    }
}
