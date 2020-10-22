using System;
using System.Collections.Generic;
using System.Text;
using PrecacheManagerServer.BLL.Models;
using System.Threading.Tasks;

namespace PrecacheManagerServer.BLL.Services
{
    public interface IPrecacheRerunService
    {
        Task AddOrUpdate<T>(PlatformSettingRequestsModelAddOrUpdate<T> request);

        Task<bool> AddOrUpdateSP<T>(PlatformSettingRequestsModelAddOrUpdate<T> request);
    }
}
