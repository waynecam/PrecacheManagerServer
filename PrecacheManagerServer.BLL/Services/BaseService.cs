using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using PrecacheManagerServer.DAL.Models;
using PrecacheManagerServer.BLL.Repositorys;
using PrecacheManagerServer.Shared.Models;

namespace PrecacheManagerServer.BLL.Services
{
    public class BaseService<T> : IBaseService<T> where T : BaseEntity
    {

        private readonly IBaseRepository<T> _repository;

        public BaseService(IBaseRepository<T> repository)
        {
            _repository = repository;
        }
        //public async Task<IEnumerable<T>> GetAsync(PlatformSettingsModel request)
        public async Task<IEnumerable<T>> GetAsync(PlatformSettingsQuery request)
        {
            return await _repository.GetAll(request);
        }

        //public async Task<T> GetById(PlatformSettingsModel request)
        public async Task<T> GetById(PlatformSettingsQuery request)
        {
            return await _repository.GetById(request);
        }

        //public async Task AddOrUpdate<TData>(PlatformSettingsModelAddOrUpdate<TData> request)
        public async Task AddOrUpdate<TData>(PlatformSettingsQueryAddUpdate<TData> request)
        {
            //_repository.addOrUpdate()

           await _repository.AddOrUpdate(request);
        }


        //public async Task AddOrUpdateSP<TData>(PlatformSettingsModelAddOrUpdate<TData> request)
        public async Task<bool> AddOrUpdateSP<TData>(PlatformSettingsQueryAddUpdate<TData> request)
        {
            //_repository.addOrUpdate()

            //return await Task.Run(() => { _repository.AddOrUpdateSP(request); });

            return await  _repository.AddOrUpdateSP(request);
        }


    }
}
