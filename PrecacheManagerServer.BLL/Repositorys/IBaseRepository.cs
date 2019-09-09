using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using PrecacheManagerServer.DAL.Models;

namespace PrecacheManagerServer.BLL.Repositorys
{
    public interface IBaseRepository<T> where T : BaseEntity
    {
        Task<IEnumerable<T>> GetAll(PlatformSettingsModel request);
        Task<T> GetById(PlatformSettingsModel request);
      
    }
}
    