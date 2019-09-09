﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using PrecacheManagerServer.BLL.Models;
using System.Linq;
using PrecacheManagerServer.DAL.Models;
using PrecacheManagerServer.BLL.Enums;

namespace PrecacheManagerServer.BLL.Services
{
    public class PrecacheSearchService : IPrecacheSearchService
    {

        private readonly IBaseService<PrecacheSearch> _service;
        private readonly IMapper _mapper;


        public PrecacheSearchService(IBaseService<PrecacheSearch> service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PrecacheSearchResponseModel>> GetAsync(PlatformSettingsRequestModel request)
        {

            
            var arg = _mapper.Map<PlatformSettingsModel>(request);

            var sql = "SELECT * FROM [" + PrecacheDbTable.PrecacheSearchItem.GetSchemaName() +"].[" + PrecacheDbTable.PrecacheSearchItem.GetTableName() + "]";

            arg.Sql = sql;

            var result = await _service.GetAsync(arg);

            return result.Select(t => _mapper.Map<PrecacheSearch, PrecacheSearchResponseModel>(t)).Take(10);

            //return new List<PrecacheSearchResponseModel>() { new PrecacheSearchResponseModel() { Id = 10 } };
        }

        public async Task<PrecacheSearchResponseModel> GetById(PlatformSettingsRequestModel request, int id)
        {

            //use the type here to work out which column is the id column and pass to the base service class

            //need to get the id column for the type in question

            var arg = _mapper.Map<PlatformSettingsModel>(request);
            arg.Where.Add("id", id.ToString());

            var sql = "SELECT * FROM [" + PrecacheDbTable.Clientsite.GetSchemaName() + "].[" + PrecacheDbTable.Clientsite.GetTableName() + "]";

            arg.Sql = sql;

            return _mapper.Map<PrecacheSearch, PrecacheSearchResponseModel>(await _service.GetById(arg));
        }
    }
}
