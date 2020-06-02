using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace PrecacheManagerServer.BLL.Models
{
    public class PlatformSettingRequestsModelAddOrUpdate <T> : BasePlatformSettingsRequestModel,  IPlatformSettingRequestsModelAddOrUpdate <T>
    {
        public List<T> Data { get; set; }

        public List<SqlParameter> SqlParameters { get; set; }


        public PlatformSettingRequestsModelAddOrUpdate()
        {
            Data = new List<T>();
        }
        
    }
}
