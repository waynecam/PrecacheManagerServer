using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using PrecacheManagerServer.DAL.Models;
using PrecacheManagerServer.DAL.Mappers;
using System.Data.SqlClient;
using PrecacheManagerServer.DAL.Contexts;
using AutoMapper;

namespace PrecacheManagerServer.BLL.Repositorys
{
    public class BaseRepository<T> : IBaseRepository<T> where T: BaseEntity
    {
        private SqlConnection _sqlConnection;

        private DBContext _dbContext;

        private readonly IMapper _mapper;

        public BaseRepository(IMapper mapper)
        {
            _mapper = mapper;
        }

        public async Task<IEnumerable<T>> GetAll(PlatformSettingsModel request)
        {
            //Table mapping logic
            //precachesearch => precachesearchItem Table then (using automapper) back again
            var type = typeof(T);
            var dbTable = DBMapper.Mapper[type];

            var sql = "SELECT * FROM [dbo].[" + dbTable + "]";

            // Here we need to Map PreacheSearchItems to PrecacheItem objects and return
            //return await QueryHandlers.ExecuteQueryGetResult<T>(sql, _dbContext.SqlConnection, _mapper);


            var conn = new SqlConnection(request.ConnectionStrings[0]);
            return await QueryHandlers.ExecuteQueryGetResult<T>(sql, conn, _mapper);



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
