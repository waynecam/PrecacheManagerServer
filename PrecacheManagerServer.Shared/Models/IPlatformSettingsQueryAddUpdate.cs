using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace PrecacheManagerServer.Shared.Models
{
    public interface IPlatformSettingsQueryAddUpdate<T> : IPlatformSettingsQuery
    {
        List<SqlParameter> SqlParameters { get; set; }

        List<T> Data { get; set; }
    }
}
