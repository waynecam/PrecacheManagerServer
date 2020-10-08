using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using PrecacheManagerServer.DAL.Models;
using PrecacheManagerServer.Shared.Models;

namespace PrecacheManagerServer.BLL.Repositorys
{
    public interface IBaseRepository<T> where T : BaseEntity
    {
        //Task<IEnumerable<T>> GetAll(PlatformSettingsModel request);
        Task<IEnumerable<T>> GetAll(PlatformSettingsQuery request);
        //Task<T> GetById(PlatformSettingsModel request);
        Task<T> GetById(PlatformSettingsQuery request);

        //Task AddOrUpdate<TData>(PlatformSettingsModelAddOrUpdate<TData> request);
        //Task AddOrUpdateSP<TData>(PlatformSettingsModelAddOrUpdate<TData> request);
        Task AddOrUpdate<TData>(PlatformSettingsQueryAddUpdate<TData> request);
        Task AddOrUpdateSP<TData>(PlatformSettingsQueryAddUpdate<TData> request);

    }
}
    