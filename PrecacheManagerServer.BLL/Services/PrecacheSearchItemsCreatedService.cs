using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
//using PrecacheManagerServer.BLL.Enums;
//using PrecacheManagerServer.BLL.Enums.Extensions;
using PrecacheManagerServer.BLL.Models;
using PrecacheManagerServer.DAL.Models;
//using PrecacheManagerServer.BLL.Enums;
//using PrecacheManagerServer.BLL.Enums.Extensions;
using PrecacheManagerServer.Shared.Models;
using PrecacheManagerServer.Shared.Enums;
using PrecacheManagerServer.Shared.Enums.Extensions;

namespace PrecacheManagerServer.BLL.Services
{
    public class PrecacheSearchItemsCreatedService : IPrecacheSearchItemsCreatedService
    {
        private readonly IBaseService<PrecacheSearchItemsCreated> _service;
        private readonly IMapper _mapper;

        public PrecacheSearchItemsCreatedService(IBaseService<PrecacheSearchItemsCreated> service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PrecacheSearchItemsCreatedResponseModel>> GetAsync(PlatformSettingsRequestModel request)
        {
            //var arg = _mapper.Map<PlatformSettingsModel>(request);
            var arg = _mapper.Map<PlatformSettingsQuery>(request);

            //replace this with a more complex where query
            var sql = "SELECT " +
                "		[ID]" +
                "      ,[CreatedDate]" +
                "      ,[LastUpdateDate]" +
                "      ,[DashBoardSearchType]" +
                "      ,[SearchId]" +
                "      ,[SearchVersion]" +
                "      ,[HomepageSearchId]" +
                "      ,[ApplicationMode]" +
                "      ,[AreaNo]" +
                "      ,[AreaSearchName]" +
                "      ,[SiteId]" +
                "      ,[HomePageSearchType]" +
                "      ,[DynamicPrecacheSearchId]" +
                "      ,[PrecacheIntegrityKey]" +
                "      ,[IsDuplicate]" +
                "  FROM [" + PrecacheDbTable.PrecacheSearchItemsCreated.GetSchemaName() + "].[" + PrecacheDbTable.PrecacheSearchItemsCreated.GetTableName() + "] " +
                "  WHERE ApplicationMode = '" + (int)request.ConnectionStrings.Keys.First().GetAttribute<ApplicationModeIdAttribute>().ApplicationModeId + "'";
            arg.Sql = sql;


            var result =  await _service.GetAsync(arg);



            return result.Select(t => _mapper.Map<PrecacheSearchItemsCreated,PrecacheSearchItemsCreatedResponseModel>(t));
        }
       public async Task<PrecacheSearchItemsCreatedResponseModel> GetById(PlatformSettingsRequestModel request, int id)
        {
            //var arg = _mapper.Map<PlatformSettingsModel>(request);
            var arg = _mapper.Map<PlatformSettingsQuery>(request);
            arg.Where.Add("id", id.ToString());

            return _mapper.Map<PrecacheSearchItemsCreated, PrecacheSearchItemsCreatedResponseModel>(await _service.GetById(arg));
            
        }


    }
}
