using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using PrecacheManagerServer.DAL.Models;
using PrecacheManagerServer.Shared.Models;

namespace PrecacheManagerServer.BLL.Services
{
    public interface IBaseService<T> where T: BaseEntity
    {
        //Task<IEnumerable<T>> GetAsync(PlatformSettingsModel request);
        Task<IEnumerable<T>> GetAsync(PlatformSettingsQuery request);

        //Task<T> GetById(PlatformSettingsModel request);
        Task<T> GetById(PlatformSettingsQuery request);

        //Task AddOrUpdate<TData>(PlatformSettingsModelAddOrUpdate<TData> request);

        //Task AddOrUpdateSP<TData>(PlatformSettingsModelAddOrUpdate<TData> request);
        Task AddOrUpdate<TData>(PlatformSettingsQueryAddUpdate<TData> request);

        Task AddOrUpdateSP<TData>(PlatformSettingsQueryAddUpdate<TData> request);
    }
}
