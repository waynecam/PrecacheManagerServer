using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using AutoMapper;
using PrecacheManagerServer.BLL.Models;
using PrecacheManagerServer.DAL.Models;

namespace PrecacheManagerServer.BLL.Services
{
    public class PrecacheRerunService
    {
        private readonly IBaseService<PrecacheRerun> _service;
        private readonly IMapper _mapper;

        public PrecacheRerunService(IBaseService<PrecacheRerun> service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        public void AddOrUpdate<T>(PlatformSettingRequestsModelAddOrUpdate<T> request)
        {

            var arg = _mapper.Map<PlatformSettingsModelAddOrUpdate<T>>(request);
            var sql = "INSERT INTO PRECACHERERUN (HomepageSearchId, HomepageSearchType, SearchId, CreatedDate, Status) " +
                "VALUES (@HomepageSearchId, @HomepageSearchType, @SearchId, @CreatedDate, @Status)";


            var sqlParamaters = new List<SqlParameter>();


            var precacheRerun = request.Data.FirstOrDefault() as PrecacheRerun;

            var parameter1 = new SqlParameter("@HomepageSearchId", precacheRerun.HomePageSearchId);
            var parameter2 = new SqlParameter("@HomepageSearchType", precacheRerun.HomepageSearchType);
            var parameter3  = new SqlParameter("@SearchId", precacheRerun.SearchId);
            var parameter4 = new SqlParameter("@CreatedDate", DateTime.Now);
            var parameter5 = new SqlParameter("@Status", 1);




            arg.SqlParameters.Add(parameter1);
            arg.SqlParameters.Add(parameter2);
            arg.SqlParameters.Add(parameter3);
            arg.SqlParameters.Add(parameter4);
            arg.SqlParameters.Add(parameter5);









            _service.AddOrUpdate(arg);


        }

    }
}
