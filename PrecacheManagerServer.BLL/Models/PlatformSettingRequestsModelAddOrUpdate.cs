using System;
using System.Collections.Generic;
using System.Text;

namespace PrecacheManagerServer.BLL.Models
{
    public class PlatformSettingRequestsModelAddOrUpdate <T> : BasePlatformSettingsRequestModel,  IPlatformSettingRequestsModelAddOrUpdate <T>
    {
        public List<T> Data { get; set; }
    }
}
