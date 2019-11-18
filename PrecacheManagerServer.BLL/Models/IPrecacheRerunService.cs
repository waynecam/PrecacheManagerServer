using System;
using System.Collections.Generic;
using System.Text;
using PrecacheManagerServer.BLL.Models;

namespace PrecacheManagerServer.BLL.Services
{
    public interface IPrecacheRerunService
    {
        void AddOrUpdate<T>(PlatformSettingRequestsModelAddOrUpdate<T> request);
    }
}
