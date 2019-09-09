using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using PrecacheManagerServer.DAL.Models;


namespace PrecacheManagerServer.BLL.Services
{
    public interface IBaseService<T> where T: BaseEntity
    {
        Task<IEnumerable<T>> GetAsync(PlatformSettingsModel request);

        Task<T> GetById(PlatformSettingsModel request);
    }
}
