﻿using System;
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
            var type = typeof(T);
            var dbTable = TableMapper.Mapper[type];

            var sql = "SELECT * FROM [dbo].[" + dbTable + "]";

            // Here we need to Map PreacheSearchItems to PrecacheItem objects and return
            //return await QueryHandlers.ExecuteQueryGetResult<T>(sql, _dbContext.SqlConnection, _mapper);


            var conn = new SqlConnection(request.ConnectionStrings[0]);
            //return await DBContext.ExecuteQueryGetResult<T>(sql, conn, _mapper);
            return await _dbContext.ExecuteQueryGetResult<T>(sql, conn);



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
