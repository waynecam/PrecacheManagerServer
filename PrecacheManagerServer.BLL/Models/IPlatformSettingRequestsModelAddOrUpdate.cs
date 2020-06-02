using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace PrecacheManagerServer.BLL.Models
{
    interface IPlatformSettingRequestsModelAddOrUpdate <T>
    {
        List<T> Data { get; set; }
        List<SqlParameter> SqlParameters { get; set; }
    }
}
