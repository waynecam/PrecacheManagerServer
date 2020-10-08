using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using PrecacheManagerServer.BLL.Models;
using PrecacheManagerServer.DAL.Models;
using PrecacheManagerServer.Shared.Models;

namespace PrecacheManagerServer.BLL.Services
{
    public class PrecacheRerunService : IPrecacheRerunService
    {
        private readonly IBaseService<PrecacheRerun> _service;
        private readonly IMapper _mapper;

        public PrecacheRerunService(IBaseService<PrecacheRerun> service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        public async Task AddOrUpdate<T>(PlatformSettingRequestsModelAddOrUpdate<T> request)
        {
            //var arg = _mapper.Map<PlatformSettingsModelAddOrUpdate<T>>(request);
            var arg = _mapper.Map<PlatformSettingsQueryAddUpdate<T>>(request);
            var sql = "INSERT INTO PRECACHERERUN (HomepageSearchId, HomepageSearchType, SearchId, CreatedDate, Status) " +
                "VALUES (@HomepageSearchId, @HomepageSearchType, @SearchId, @CreatedDate, @Status)";

            arg.Sql = sql;

            var sqlParamaters = new List<SqlParameter>();


            var precacheRerun = request.Data.FirstOrDefault() as PrecacheRerun;

            var parameter1 = new SqlParameter("@HomepageSearchId", precacheRerun.HomePageSearchId);
            var parameter2 = new SqlParameter("@HomepageSearchType", precacheRerun.HomepageSearchType);
            var parameter3 = new SqlParameter("@SearchId", precacheRerun.SearchId);
            var parameter4 = new SqlParameter("@CreatedDate", DateTime.Now);
            var parameter5 = new SqlParameter("@Status", 1);




            arg.SqlParameters.Add(parameter1);
            arg.SqlParameters.Add(parameter2);
            arg.SqlParameters.Add(parameter3);
            arg.SqlParameters.Add(parameter4);
            arg.SqlParameters.Add(parameter5);



           await  _service.AddOrUpdate(arg);


        }
        public async Task AddOrUpdateSP<T>(PlatformSettingRequestsModelAddOrUpdate<T> request)
        {
            //var arg = _mapper.Map<PlatformSettingsModelAddOrUpdate<T>>(request);
            var arg = _mapper.Map<PlatformSettingsQueryAddUpdate<T>>(request);

            //arg.SqlCommandType = System.Data.CommandType.StoredProcedure;

            var precacheRerun = _mapper.Map<PrecacheRerun>(request.Data.FirstOrDefault());

            var sql = "pc_InsertDynamicPrecacheRerunPrecacheSearch";

            arg.Sql = sql;

            var paramater1 = new SqlParameter("@precacheIntegrityKey", precacheRerun.PrecacheIntegrityKey);

            arg.SqlParameters.Add(paramater1);

            await _service.AddOrUpdateSP(arg);
        }
    }
}
