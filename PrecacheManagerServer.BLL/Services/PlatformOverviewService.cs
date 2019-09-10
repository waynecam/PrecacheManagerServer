using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using PrecacheManagerServer.DAL.Models;
using System.Linq;
using PrecacheManagerServer.BLL.Models;
using System.Threading.Tasks;
using PrecacheManagerServer.BLL.Enums;

namespace PrecacheManagerServer.BLL.Services
{
    public class PlatformOverviewService : IPlatformOverviewService
    {
        private readonly IBaseService<PlatformOverview> _service;
        private readonly IMapper _mapper;


        public PlatformOverviewService(IBaseService<PlatformOverview> service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PlatformOverviewResponseModel>> GetAsync(PlatformSettingsRequestModel request)
        {


            var arg = _mapper.Map<PlatformSettingsModel>(request);

            //replace this with a more complex where query
            var sql = "SELECT  psi.[ID]" +
                "      ,[Data]" +
                "      ,[Data_Length]" +
                "      ,[CreatedDate]" +
                "      ,[LastUpdateDate]" +
                "      ,[Filter]" +
                "      ,[DashBoardSearchType]" +
                "      ,[SearchId]" +
                "      ,[SearchVersion]" +
                "      ,[HomepageSearchId]" +
                "      ,[ApplicationMode]" +
                "      ,[PrecacheKey]" +
                "      ,[AreaNo]" +
                "      ,[SiteId]" +
                "      ,[HomePageSearchType]" +
                "      ,[DynamicPrecacheSearchId]" +
                "      ,[AreaSearchName]" +
                "      ,[PrecacheIntegrityKey]" +
                "	  ,cs.Sitename" +
                "	  ,cs.Id as SiteId" +
                "  FROM [" + PrecacheDbTable.PrecacheSearchItem.GetSchemaName() + "].[" + PrecacheDbTable.PrecacheSearchItem.GetTableName() + "] psi"
                 + " JOIN  " +
                 "[" + PrecacheDbTable.Clientsite.GetSchemaName() + "].[" + PrecacheDbTable.Clientsite.GetTableName() + "] cs ON SiteID = cs.ID" +
                "  WHERE psi.applicationMode = 2 and psi.IsDeleted = 0";

            arg.Sql = sql;


            var result = await _service.GetAsync(arg);
                

            return result.Select(t => _mapper.Map<PlatformOverview, PlatformOverviewResponseModel>(t));




            //return result.Select(t => _mapper.Map<PrecacheSearch, PrecacheSearchResponseModel>(t)).Take(10);

            //return new List<PrecacheSearchResponseModel>() { new PrecacheSearchResponseModel() { Id = 10 } };
        }

        public async Task<PlatformOverviewResponseModel> GetById(PlatformSettingsRequestModel request, int id)
        {
            //use the type here to work out which column is the id column and pass to the base service class

            //need to get the id column for the type in question

            var arg = _mapper.Map<PlatformSettingsModel>(request);
            arg.Where.Add("id", id.ToString());

            return _mapper.Map<PlatformOverview, PlatformOverviewResponseModel>(await _service.GetById(arg));
        }

       

    }
}
