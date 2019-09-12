using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using PrecacheManagerServer.DAL.Models;
using System.Linq;
using PrecacheManagerServer.BLL.Models;
using System.Threading.Tasks;
using PrecacheManagerServer.BLL.Enums;
using PrecacheManagerServer.DAL.Enums;

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
                "      ,psi.[IsDeleted]" +
                "	  ,cs.Sitename" +
                "	  ,cs.Id as SiteId" +
                "  FROM [" + PrecacheDbTable.PrecacheSearchItem.GetSchemaName() + "].[" + PrecacheDbTable.PrecacheSearchItem.GetTableName() + "] psi"
                 + " JOIN  " +
                 "[" + PrecacheDbTable.Clientsite.GetSchemaName() + "].[" + PrecacheDbTable.Clientsite.GetTableName() + "] cs ON SiteID = cs.ID" +
                "  WHERE psi.applicationMode = " + (int)request.Connections.Keys.First() + " and psi.IsDeleted = 0";

            arg.Sql = sql;


            var result = await _service.GetAsync(arg);
            

            var platformOverviewRM = new PlatformOverviewResponseModel();

            platformOverviewRM.ApplicationMode = result.Select(x => x.ApplicationMode).FirstOrDefault();

            //get the distinct precacheSites
            var precacheSites = new List<PrecacheSite>();

            //now get all the distinct siteids and the associated
            var siteGrpBy = result.GroupBy(x => new { x.SiteId, x.Name }).Select(y => y.Key);

         
            //noew build the precachesites and the related precachesearches
            foreach (var key in siteGrpBy)
            {

                var precachesite = new PrecacheSite(); ;
                var precacheSearches = new List<PrecacheSearch>();

                precachesite.SiteId = key.SiteId;
                precachesite.Name = key.Name;
       
                precacheSearches = result.Where(x => x.SiteId == key.SiteId).Select(x => new PrecacheSearch()
                {
                    Id = x.Id,
                    AreaNo = x.AreaNo,
                    CreatedDate = x.CreatedDate,
                    LastUpdateDate = x.LastUpdateDate,
                    IsDeleted = x.IsDeleted,
                    DashboardSearchType = x.DashboardSearchType,
                    SearchId = x.SearchId,
                    HomepageSearchId = x.HomepageSearchId,
                    ApplicationMode = (int)x.ApplicationMode,
                    PrecacheKey = x.PrecacheKey,
                    SiteId = x.SiteId,
                    HomePageSearchType = x.HomePageSearchType,
                    DynamicPrecacheSearchId = x.DynamicPrecacheSearchId,
                    AreaSearchName = x.AreaSearchName,
                    PrecacheIntegrityKey = x.PrecacheIntegrityKey
                }).ToList();


                precachesite.PrecacheSearches = precacheSearches;

                platformOverviewRM.PrecacheSites.Add(precachesite);
            }


            var finalResult = new List<PlatformOverviewResponseModel>();
            finalResult.Add(platformOverviewRM);

            return finalResult;







            ////get all the precachesite objects
            ////var precacheSites = new List<PrecacheSite>();

            ////foreach (var item in result)
            ////{
            ////    var site = result.Select(x => x.PrecacheSites).FirstOrDefault();
            ////    precacheSites.Add(site.FirstOrDefault());
            ////}

            ////now get all the distinct siteids
            ////var siteids = precacheSites.GroupBy(x => x.SiteId).Select(y => y.Key);

            ////now get all the distinct siteids
            ////var grpBySiteId = precacheSites.GroupBy(x => new { x.SiteId, x.Name }).Select(y => y.Key);


            ////var platformOverview = new PlatformOverview();
            ////now get all the precachesearches from the results based on the siteids
            ////foreach (var key in grpBySiteId)
            ////{
            ////    var precachesite = new PrecacheSite();

            ////    precachesite.SiteId = key.SiteId;
            ////    precachesite.Name = key.Name;

            ////    var precachesearches = new List<PrecacheSearch>();
            ////    precachesites = precacheSites.Where(x => x.)


            ////}



            //var groupby = result.GroupBy(x => x.PrecacheSites.);

            //var platformOverView = new PlatformOverview();
            //platformOverView.ApplicationMode = request.Connections.Keys.First();

            //foreach (var grp in groupby)
            //{
            //    var precacheSite = new PrecacheSite() { SiteId = grp.Key };

            //    precacheSite.PrecacheSearches = result.Where(x => x.SiteId == grp.Key).Select(x => x).ToList();
            //    platformOverView.PrecacheSites.Add(precacheSite);
            //}


            //var precacheSites = new List<PrecacheSite>();

            //foreach(var item in result)
            //{
            //    precacheSites.Add(item.PrecacheSites.FirstOrDefault());
            //}

            ////now get the precache searches for each the site
            //foreach(var site in precacheSites)
            //{
            //    var precachesearches = precacheSites.Where(x => x.SiteId == site.SiteId).Select(x => x);

            //}


            //return result.Select(t => _mapper.Map<PlatformOverview, PlatformOverviewResponseModel>(t));




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
