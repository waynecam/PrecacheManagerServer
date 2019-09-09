using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using PrecacheManagerServer.DAL.Models;
using PrecacheManagerServer.DAL.Mappers;
using System.Data.SqlClient;
using PrecacheManagerServer.DAL.Contexts;
using AutoMapper;
using System.Linq;

namespace PrecacheManagerServer.BLL.Repositorys
{
    public class BaseRepository<T> : IBaseRepository<T> where T: BaseEntity
    {
        private readonly IMapper _mapper;
        private readonly IDBContext _dbContext;

        public BaseRepository(IMapper mapper, IDBContext dbContext)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<T>> GetAll(PlatformSettingsModel request)
        {
            //Table mapping logic
            //precachesearch => precachesearchItem Table then (using automapper) back again
            //var type = typeof(T);
            //var dbTable = TableMapper.Mapper[type];

            //var sql = "SELECT * FROM [dbo].[" + dbTable + "]";

            // Here we need to Map PreacheSearchItems to PrecacheItem objects and return
            //return await QueryHandlers.ExecuteQueryGetResult<T>(sql, _dbContext.SqlConnection, _mapper);


            var conn = new SqlConnection(request.ConnectionStrings[0]);
            //return await DBContext.ExecuteQueryGetResult<T>(sql, conn, _mapper);
            return await _dbContext.ExecuteQueryGetResult<T>(request.Sql, conn);



        }

        public async Task<T> GetById(PlatformSettingsModel request)
        {
            //Table mapping logic
            //precachesearch => precachesearchItem Table then (using automapper) back again


            var type = typeof(T);
            var dbTable = TableMapper.Mapper[type];
            var idColumn = request.Where.Keys.First();
            var value = request.Where[idColumn];
            var sql = "SELECT * FROM [dbo].[" + dbTable + "] WHERE " + idColumn + "= " + value;



            var conn = new SqlConnection(request.ConnectionStrings[0]);
            //return await DBContext.ExecuteQueryGetResult<T>(sql, conn, _mapper);

            var result = await _dbContext.ExecuteQueryGetResult<T>(sql, conn);
            return result.FirstOrDefault();
        }

        public async Task<IEnumerable<T>> Where(PlatformSettingsModel request, string sql)
        {
            //Table mapping logic
            //precachesearch => precachesearchItem Table then (using automapper) back again
            var conn = new SqlConnection(request.ConnectionStrings[0]);
            return await _dbContext.ExecuteQueryGetResult<T>(sql, conn);
        }
    }
}
