using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using PrecacheManagerServer.DAL.Models;
using PrecacheManagerServer.BLL.Repositorys;

namespace PrecacheManagerServer.BLL.Services
{
    public class BaseService<T> : IBaseService<T> where T : BaseEntity
    {

        private readonly IBaseRepository<T> _repository;

        public BaseService(IBaseRepository<T> repository)
        {
            _repository = repository;
        }
        public async Task<IEnumerable<T>> GetAsync()
        {
            return await _repository.GetAll();
        }

        public async Task<T> GetById(int id)
        {
            return await _repository.GetById(id);
        }

        public IEnumerable<T> Where(string sql)
        {
            return _repository.Where(sql);
        }
    }
}
