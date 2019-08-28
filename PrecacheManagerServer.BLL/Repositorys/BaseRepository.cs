using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using PrecacheManagerServer.DAL.Models;

namespace PrecacheManagerServer.BLL.Repositorys
{
    public class BaseRepository<T> : IBaseRepository<T> where T: BaseEntity
    {
        public Task<IEnumerable<T>> GetAll()
        {
            //Table mapping logic
            //precachesearch => precachesearchItem Table then (using automapper) back again
            throw new NotImplementedException();
        }

        public Task<T> GetById(int id)
        {
            //Table mapping logic
            //precachesearch => precachesearchItem Table then (using automapper) back again
            throw new NotImplementedException();
        }

        public IEnumerable<T> Where(string sql)
        {
            //Table mapping logic
            //precachesearch => precachesearchItem Table then (using automapper) back again
            throw new NotImplementedException();
        }
    }
}
