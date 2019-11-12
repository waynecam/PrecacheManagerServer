using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace PrecacheManagerServer.DAL.Models
{
    interface IPlatformSettingsModelAddOrUpdate <T>
    {
        List<SqlParameter> SqlParameters {get;set;}

         List<T> Data { get; set; }
    }
}
