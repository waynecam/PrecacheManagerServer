﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using PrecacheManagerServer.BLL.Enums;
using PrecacheManagerServer.BLL.Models;
using PrecacheManagerServer.DAL.Models;

namespace PrecacheManagerServer.BLL.Services
{
    public class LoggedPrecacheSearchItemService : ILoggedPrecacheSearchItemService
    {

        private readonly IBaseService<LoggedPrecacheSearchItem> _service;
        private readonly IMapper _mapper;

        public LoggedPrecacheSearchItemService(IBaseService<LoggedPrecacheSearchItem> service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        public async Task<IEnumerable<LoggedPrecacheSearchItemResponseModel>> GetAsync(PlatformSettingsRequestModel request)
        {


            var arg = _mapper.Map<PlatformSettingsModel>(request);


            var sql = "SELECT TOP 1000 [ID]" +
                "      ,[HomePageSearchType]" +
                "      ,[HomepageSearchId]" +
                "      ,[SiteId]" +
                "      ,[CreatedDate]" +
                "      ,[LastUpdateDate]" +
                "      ,[DashBoardSearchTypeEnum]" +
                "      ,[SearchId]" +
                "      ,[SearchVersion]" +
                "      ,[ApplicationMode]" +
                "      ,[AreaNo]" +
                "      ,[ErrorMessage]" +
                "         FROM[" + PrecacheDbTable.LoggedPrecacheSearchItem.GetSchemaName() + "].[" + PrecacheDbTable.LoggedPrecacheSearchItem.GetTableName() + "]" +
                "  WHERE applicationMode = " + (int)request.Connections.Keys.First() + "";

            arg.Sql = sql;


            var result = await _service.GetAsync(arg);

            return result.Select(t => _mapper.Map<LoggedPrecacheSearchItem, LoggedPrecacheSearchItemResponseModel>(t));



        }

       public async Task<LoggedPrecacheSearchItemResponseModel> GetById(PlatformSettingsRequestModel request, int id)
        {
            var arg = _mapper.Map<PlatformSettingsModel>(request);
            arg.Where.Add("id", id.ToString());

            var sql = "SELECT * FROM [" + PrecacheDbTable.LoggedPrecacheSearchItem.GetSchemaName() + "].[" + PrecacheDbTable.LoggedPrecacheSearchItem.GetTableName() + "]";

            arg.Sql = sql;

            return _mapper.Map<LoggedPrecacheSearchItem, LoggedPrecacheSearchItemResponseModel>(await _service.GetById(arg));
        }
    }
}