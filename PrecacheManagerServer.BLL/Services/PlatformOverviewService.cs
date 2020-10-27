using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using PrecacheManagerServer.DAL.Models;
using System.Linq;
using PrecacheManagerServer.BLL.Models;
using System.Threading.Tasks;
//using PrecacheManagerServer.BLL.Enums.Extensions;
//using PrecacheManagerServer.BLL.Enums;
using PrecacheManagerServer.Shared.Enums.Extensions;
using PrecacheManagerServer.Shared.Enums;
using PrecacheManagerServer.Shared.Models;
using PrecacheManagerServer.Shared.ExtensionMethods;

namespace PrecacheManagerServer.BLL.Services
{
    public class PlatformOverviewService : IPlatformOverviewService
    {
        private readonly IBaseService<PrecacheSearchPlus> _precacheSearchPlusService;
        private readonly IBaseService<LoggedPrecacheSearchItem> _loggedPrecacheSearchService2;
        private readonly IBaseService<PrecacheSearchItemsCreated> _precacheSearchesCreatedService;
        private readonly IMapper _mapper;


        public PlatformOverviewService(IBaseService<PrecacheSearchPlus> precacheSearchPlusService, IBaseService<LoggedPrecacheSearchItem> loggedPrecacheSearchService, IBaseService<PrecacheSearchItemsCreated> precacheSearchesCreatedService, IMapper mapper)
        {
            _precacheSearchPlusService = precacheSearchPlusService;
            _loggedPrecacheSearchService2 = loggedPrecacheSearchService;
            _precacheSearchesCreatedService = precacheSearchesCreatedService;
            _mapper = mapper;
        }

        public virtual async Task<IEnumerable<PlatformOverviewResponseModel>> GetAsync(PlatformSettingsRequestModel request)
        {
            //var arg = _mapper.Map<PlatformSettingsModel>(request);
            var arg =  _mapper.Map<PlatformSettingsQuery>(request);
            var appMode = request.ConnectionStrings.Keys.First().GetAttribute<ApplicationModeIdAttribute>().ApplicationModeId;
            //get the precachesearch(plus) items
            IEnumerable<PrecacheSearchPlus> precacheSearchsQuery = await GetPrecacheSearchPlusItems(arg, appMode);

            //get the loggedprecachesearches
            IEnumerable<LoggedPrecacheSearchItem> loggedPrecacheSearchesQuery = await GetLoggedPrecacheSearchItems(request, arg);

            //now get the precachesearchitemscreated
            IEnumerable<PrecacheSearchItemsCreated> preceacheSeacrehItemsCreatedQuery = await GetPrecacheSearchItemsCreatedItems(request, arg);

            //Now build the platformOverview
            PlatformOverviewResponseModel platformOverviewRM = BuildPlatformOverviewModel(precacheSearchsQuery, loggedPrecacheSearchesQuery, preceacheSeacrehItemsCreatedQuery);

            var finalResult = new List<PlatformOverviewResponseModel>();
            finalResult.Add(platformOverviewRM);

            return finalResult;
        }

        private static PlatformOverviewResponseModel BuildPlatformOverviewModel(IEnumerable<PrecacheSearchPlus> precacheSearchsQuery, IEnumerable<LoggedPrecacheSearchItem> loggedPrecacheSearchesQuery, IEnumerable<PrecacheSearchItemsCreated> preceacheSeacrehItemsCreatedQuery)
        {
            var platformOverviewRM = new PlatformOverviewResponseModel();

            var appMode = precacheSearchsQuery.Select(x => x.ApplicationMode).FirstOrDefault();

            platformOverviewRM.ApplicationMode = appMode;

            platformOverviewRM.ApplicationModeId = appMode.GetAttribute<ApplicationModeIdAttribute>().ApplicationModeId;


            //get the distinct precacheSites
            var precacheSites = new List<PrecacheSite>();

            //now get all the distinct siteids and the associated
            var siteGrpBy = precacheSearchsQuery.GroupBy(x => new { x.SiteId, x.Name, x.ApplicationMode }).Select(y => y.Key);


            //now build the precachesites and the related precachesearches/logged precache searches/ precachsearchitems created
            foreach (var key in siteGrpBy)
            {
                var precachesite = new PrecacheSite(); ;
                var precacheSearches = new List<PrecacheSearch>();

                precachesite.SiteId = key.SiteId;
                precachesite.Name = key.Name;
                precachesite.ApplicationMode = key.ApplicationMode;

                precacheSearches = precacheSearchsQuery.Where(x => x.SiteId == key.SiteId).Select(x => new PrecacheSearch()
                {
                    Id = x.Id,
                    AreaNo = x.AreaNo,
                    CreatedDate = x.CreatedDate.FormattedDateAndTime(),
                    LastUpdateDate = x.LastUpdateDate.FormattedDateAndTime(),
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

                //get the sites precacheSearches
                precachesite.PrecacheSearches = precacheSearches;

                //get the sites logged Precache Searches
                var siteLoggedPrecacheSearches = loggedPrecacheSearchesQuery.Where(x => x.SiteId == key.SiteId).ToList();
                precachesite.LoggedPrecacheSearchItems = siteLoggedPrecacheSearches;

                //get the sites precacheSearchItemsCreated Precache Searches
                var sitePrecacheSearchItemsCreated = preceacheSeacrehItemsCreatedQuery.Where(x => x.SiteId == key.SiteId).ToList();
                precachesite.PrecacheSearchItemsCreated = sitePrecacheSearchItemsCreated;

                //add precache site to the platformOverview object (all sites for given platform)
                platformOverviewRM.PrecacheSites.Add(precachesite);

            }

            return platformOverviewRM;
        }

        //private async Task<IEnumerable<PrecacheSearchItemsCreated>> GetPrecacheSearchItemsCreatedItems(PlatformSettingsRequestModel request, PlatformSettingsModel arg)
        private async Task<IEnumerable<PrecacheSearchItemsCreated>> GetPrecacheSearchItemsCreatedItems(PlatformSettingsRequestModel request, PlatformSettingsQuery arg)
        {
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


            var preceacheSeacrehItemsCreatedQuery = await _precacheSearchesCreatedService.GetAsync(arg);

            preceacheSeacrehItemsCreatedQuery.Select(t => _mapper.Map<PrecacheSearchItemsCreated, PrecacheSearchItemsCreatedResponseModel>(t));
            return preceacheSeacrehItemsCreatedQuery;
        }

       //private async Task<IEnumerable<LoggedPrecacheSearchItem>> GetLoggedPrecacheSearchItems(PlatformSettingsRequestModel request, PlatformSettingsModel arg)
       private async Task<IEnumerable<LoggedPrecacheSearchItem>> GetLoggedPrecacheSearchItems(PlatformSettingsRequestModel request, PlatformSettingsQuery arg)
        {
            var sql = "SELECT [ID]" +
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
                "      ,[PrecacheIntegrityKey]" +
                "         FROM[" + PrecacheDbTable.LoggedPrecacheSearchItem.GetSchemaName() + "].[" + PrecacheDbTable.LoggedPrecacheSearchItem.GetTableName() + "]" +
                "  WHERE applicationMode = " + (int)request.ConnectionStrings.Keys.First().GetAttribute<ApplicationModeIdAttribute>().ApplicationModeId + "";

            arg.Sql = sql;


            var loggedPrecacheSearchesQuery = await _loggedPrecacheSearchService2.GetAsync(arg);

            loggedPrecacheSearchesQuery.Select(t => _mapper.Map<LoggedPrecacheSearchItem, LoggedPrecacheSearchItemResponseModel>(t));
            return loggedPrecacheSearchesQuery;
        }

        //private async Task<IEnumerable<PrecacheSearchPlus>> GetPrecacheSearchPlusItems(PlatformSettingsModel arg, int appMode)
            private async Task<IEnumerable<PrecacheSearchPlus>> GetPrecacheSearchPlusItems(PlatformSettingsQuery arg, int appMode)
        {
            //replace this with a more complex where query
            var sql = "SELECT psi.[ID]" +
             
                "      ,[Data_Length]" +
                "      ,[CreatedDate]" +
                "      ,[LastUpdateDate]" +
             
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
                "  WHERE psi.applicationMode = " + appMode + " and psi.IsDeleted = 0";

            arg.Sql = sql;


            var precacheSearchsQuery = await _precacheSearchPlusService.GetAsync(arg);
            return precacheSearchsQuery;
        }

        //public async Task<IEnumerable<PlatformOverviewResponseModel>> GetAsync(PlatformSettingsRequestModel request)
        //{


        //    var arg = _mapper.Map<PlatformSettingsModel>(request);
        //    var appMode = request.Connections.Keys.First().GetAttribute<ApplicationModeIdAttribute>().ApplicationModeId;

        //    //replace this with a more complex where query
        //        var sql = "SELECT psi.[ID]" +
        //            "      ,[Data]" +
        //            "      ,[Data_Length]" +
        //            "      ,[CreatedDate]" +
        //            "      ,[LastUpdateDate]" +
        //            "      ,[Filter]" +
        //            "      ,[DashBoardSearchType]" +
        //            "      ,[SearchId]" +
        //            "      ,[SearchVersion]" +
        //            "      ,[HomepageSearchId]" +
        //            "      ,[ApplicationMode]" +
        //            "      ,[PrecacheKey]" +
        //            "      ,[AreaNo]" +
        //            "      ,[SiteId]" +
        //            "      ,[HomePageSearchType]" +
        //            "      ,[DynamicPrecacheSearchId]" +
        //            "      ,[AreaSearchName]" +
        //            "      ,[PrecacheIntegrityKey]" +
        //            "      ,psi.[IsDeleted]" +
        //            "	  ,cs.Sitename" +
        //            "	  ,cs.Id as SiteId" +
        //            "  FROM [" + PrecacheDbTable.PrecacheSearchItem.GetSchemaName() + "].[" + PrecacheDbTable.PrecacheSearchItem.GetTableName() + "] psi"
        //             + " JOIN  " +
        //             "[" + PrecacheDbTable.Clientsite.GetSchemaName() + "].[" + PrecacheDbTable.Clientsite.GetTableName() + "] cs ON SiteID = cs.ID" +
        //            "  WHERE psi.applicationMode = " + appMode + " and psi.IsDeleted = 0";

        //    arg.Sql = sql;


        //    var result = await _service.GetAsync(arg);


        //    var platformOverviewRM = new PlatformOverviewResponseModel();

        //    platformOverviewRM.ApplicationMode = result.Select(x => x.ApplicationMode).FirstOrDefault();

        //    //get the distinct precacheSites
        //    var precacheSites = new List<PrecacheSite>();

        //    //now get all the distinct siteids and the associated
        //    var siteGrpBy = result.GroupBy(x => new { x.SiteId, x.Name, x.ApplicationMode }).Select(y => y.Key);


        //    //now build the precachesites and the related precachesearches
        //    foreach (var key in siteGrpBy)
        //    {

        //        var precachesite = new PrecacheSite(); ;
        //        var precacheSearches = new List<PrecacheSearch>();

        //        precachesite.SiteId = key.SiteId;
        //        precachesite.Name = key.Name;
        //        precachesite.ApplicationMode = key.ApplicationMode;

        //        precacheSearches = result.Where(x => x.SiteId == key.SiteId).Select(x => new PrecacheSearch()
        //        {
        //            Id = x.Id,
        //            AreaNo = x.AreaNo,
        //            CreatedDate = x.CreatedDate,
        //            LastUpdateDate = x.LastUpdateDate,
        //            IsDeleted = x.IsDeleted,
        //            DashboardSearchType = x.DashboardSearchType,
        //            SearchId = x.SearchId,
        //            HomepageSearchId = x.HomepageSearchId,
        //            ApplicationMode = (int)x.ApplicationMode,
        //            PrecacheKey = x.PrecacheKey,
        //            SiteId = x.SiteId,
        //            HomePageSearchType = x.HomePageSearchType,
        //            DynamicPrecacheSearchId = x.DynamicPrecacheSearchId,
        //            AreaSearchName = x.AreaSearchName,
        //            PrecacheIntegrityKey = x.PrecacheIntegrityKey
        //        }).ToList();


        //        precachesite.PrecacheSearches = precacheSearches;

        //        platformOverviewRM.PrecacheSites.Add(precachesite);

        //    }

        //    //now get the loggedprecachesearches
        //    var sql2 = "SELECT [ID]" +
        //        "      ,[HomePageSearchType]" +
        //        "      ,[HomepageSearchId]" +
        //        "      ,[SiteId]" +
        //        "      ,[CreatedDate]" +
        //        "      ,[LastUpdateDate]" +
        //        "      ,[DashBoardSearchTypeEnum]" +
        //        "      ,[SearchId]" +
        //        "      ,[SearchVersion]" +
        //        "      ,[ApplicationMode]" +
        //        "      ,[AreaNo]" +
        //        "      ,[ErrorMessage]" +
        //        "      ,[PrecacheIntegrityKey]" +
        //        "         FROM[" + PrecacheDbTable.LoggedPrecacheSearchItem.GetSchemaName() + "].[" + PrecacheDbTable.LoggedPrecacheSearchItem.GetTableName() + "]" +
        //        "  WHERE applicationMode = " + (int)request.Connections.Keys.First().GetAttribute<ApplicationModeIdAttribute>().ApplicationModeId + "";

        //    arg.Sql = sql2;


        //    var result2 = await _service2.GetAsync(arg);

        //    result2.Select(t => _mapper.Map<LoggedPrecacheSearchItem, LoggedPrecacheSearchItemResponseModel>(t));

        //    platformOverviewRM.LoggedPrecacheSearchItems = result2.ToList();


        //    //now get the precachesearchitemscreated
        //    var sql3 = "SELECT " +
        //        "		[ID]" +
        //        "      ,[CreatedDate]" +
        //        "      ,[LastUpdateDate]" +
        //        "      ,[DashBoardSearchType]" +
        //        "      ,[SearchId]" +
        //        "      ,[SearchVersion]" +
        //        "      ,[HomepageSearchId]" +
        //        "      ,[ApplicationMode]" +
        //        "      ,[AreaNo]" +
        //        "      ,[AreaSearchName]" +
        //        "      ,[SiteId]" +
        //        "      ,[HomePageSearchType]" +
        //        "      ,[DynamicPrecacheSearchId]" +
        //        "      ,[PrecacheIntegrityKey]" +
        //        "      ,[IsDuplicate]" +
        //        "  FROM [" + PrecacheDbTable.PrecacheSearchItemsCreated.GetSchemaName() + "].[" + PrecacheDbTable.PrecacheSearchItemsCreated.GetTableName() + "] " +
        //        "  WHERE ApplicationMode = '" + (int)request.Connections.Keys.First().GetAttribute<ApplicationModeIdAttribute>().ApplicationModeId + "'";
        //    arg.Sql = sql3;


        //    var result3 = await _service3.GetAsync(arg);

        //    result3.Select(t => _mapper.Map<PrecacheSearchItemsCreated, PrecacheSearchItemsCreatedResponseModel>(t));

        //    platformOverviewRM.PrecacheSearchItemsCreated = result3.ToList();


        //    //Get the failed precache services
        //    //var failedPrecacheSearches = new List<LoggedPrecacheSearchItem>();

        //    //failedPrecacheSearches = result.Where(x => x.FailedSearchSiteID == key.SiteId).Select(x => new LoggedPrecacheSearchItem()
        //    //{
        //    //    SiteId = x.FailedSearchSiteID

        //    //}).ToList();

        //    //platformOverviewRM.LoggedPrecacheSearchItems = failedPrecacheSearches;


        //    //// now fetch the precachesearchitems that should have been created
        //    //sql = "SELECT COUNT(*) FROM " +
        //    //"[" + PrecacheDbTable.PrecacheSearchItem.GetSchemaName() + "].[" + PrecacheDbTable.PrecacheSearchItem.GetTableName() + "] psic" +
        //    //"WHERE psic.applicationMode = " + (int)request.Connections.Keys.First() +
        //    //"AND psic.IsDuplicate = 0";


        //    var finalResult = new List<PlatformOverviewResponseModel>();
        //    finalResult.Add(platformOverviewRM);

        //    return finalResult;

        //    //return result.Select(t => _mapper.Map<PlatformOverview, PlatformOverviewResponseModel>(t));

        //    //return result.Select(t => _mapper.Map<PrecacheSearch, PrecacheSearchResponseModel>(t)).Take(10);

        //    //return new List<PrecacheSearchResponseModel>() { new PrecacheSearchResponseModel() { Id = 10 } };
        //}

        //public async Task<IEnumerable<PlatformOverviewResponseModel>> GetAsync(PlatformSettingsRequestModel request)
        //{


        //    var arg = _mapper.Map<PlatformSettingsModel>(request);
        //    var appMode = request.Connections.Keys.First().GetAttribute<ApplicationModeIdAttribute>().ApplicationModeId;

        //    //replace this with a more complex where query
        //    var sql = "SELECT top 50 psi.[ID]" +
        //        "      ,[Data]" +
        //        "      ,[Data_Length]" +
        //        "      ,[CreatedDate]" +
        //        "      ,[LastUpdateDate]" +
        //        "      ,[Filter]" +
        //        "      ,[DashBoardSearchType]" +
        //        "      ,[SearchId]" +
        //        "      ,[SearchVersion]" +
        //        "      ,[HomepageSearchId]" +
        //        "      ,[ApplicationMode]" +
        //        "      ,[PrecacheKey]" +
        //        "      ,[AreaNo]" +
        //        "      ,[SiteId]" +
        //        "      ,[HomePageSearchType]" +
        //        "      ,[DynamicPrecacheSearchId]" +
        //        "      ,[AreaSearchName]" +
        //        "      ,[PrecacheIntegrityKey]" +
        //        "      ,psi.[IsDeleted]" +
        //        "	  ,cs.Sitename" +
        //        "	  ,cs.Id as SiteId" +
        //        "  FROM [" + PrecacheDbTable.PrecacheSearchItem.GetSchemaName() + "].[" + PrecacheDbTable.PrecacheSearchItem.GetTableName() + "] psi"
        //         + " JOIN  " +
        //         "[" + PrecacheDbTable.Clientsite.GetSchemaName() + "].[" + PrecacheDbTable.Clientsite.GetTableName() + "] cs ON SiteID = cs.ID" +
        //        "  WHERE psi.applicationMode = " + appMode + " and psi.IsDeleted = 0";

        //    arg.Sql = sql;


        //    var result = await _service.GetAsync(arg);


        //    var platformOverviewRM = new PlatformOverviewResponseModel();

        //    platformOverviewRM.ApplicationMode = result.Select(x => x.ApplicationMode).FirstOrDefault();

        //    //get the distinct precacheSites
        //    var precacheSites = new List<PrecacheSite>();

        //    //now get all the distinct siteids and the associated
        //    var siteGrpBy = result.GroupBy(x => new { x.SiteId, x.Name, x.ApplicationMode }).Select(y => y.Key);


        //    //noew build the precachesites and the related precachesearches
        //    foreach (var key in siteGrpBy)
        //    {

        //        var precachesite = new PrecacheSite(); ;
        //        var precacheSearches = new List<PrecacheSearch>();

        //        precachesite.SiteId = key.SiteId;
        //        precachesite.Name = key.Name;
        //        precachesite.ApplicationMode = key.ApplicationMode;

        //        precacheSearches = result.Where(x => x.SiteId == key.SiteId).Select(x => new PrecacheSearch()
        //        {
        //            Id = x.Id,
        //            AreaNo = x.AreaNo,
        //            CreatedDate = x.CreatedDate,
        //            LastUpdateDate = x.LastUpdateDate,
        //            IsDeleted = x.IsDeleted,
        //            DashboardSearchType = x.DashboardSearchType,
        //            SearchId = x.SearchId,
        //            HomepageSearchId = x.HomepageSearchId,
        //            ApplicationMode = (int)x.ApplicationMode,
        //            PrecacheKey = x.PrecacheKey,
        //            SiteId = x.SiteId,
        //            HomePageSearchType = x.HomePageSearchType,
        //            DynamicPrecacheSearchId = x.DynamicPrecacheSearchId,
        //            AreaSearchName = x.AreaSearchName,
        //            PrecacheIntegrityKey = x.PrecacheIntegrityKey
        //        }).ToList();


        //        precachesite.PrecacheSearches = precacheSearches;

        //        platformOverviewRM.PrecacheSites.Add(precachesite);
        //    }

        //    //// now fetch the precachesearchitems that should have been created
        //    //sql = "SELECT COUNT(*) FROM " +
        //    //"[" + PrecacheDbTable.PrecacheSearchItem.GetSchemaName() + "].[" + PrecacheDbTable.PrecacheSearchItem.GetTableName() + "] psic" +
        //    //"WHERE psic.applicationMode = " + (int)request.Connections.Keys.First() +
        //    //"AND psic.IsDuplicate = 0";


        //    var finalResult = new List<PlatformOverviewResponseModel>();
        //    finalResult.Add(platformOverviewRM);

        //    return finalResult;

        //    //return result.Select(t => _mapper.Map<PlatformOverview, PlatformOverviewResponseModel>(t));

        //    //return result.Select(t => _mapper.Map<PrecacheSearch, PrecacheSearchResponseModel>(t)).Take(10);

        //    //return new List<PrecacheSearchResponseModel>() { new PrecacheSearchResponseModel() { Id = 10 } };
        //}

        public async Task<PlatformOverviewResponseModel> GetById(PlatformSettingsRequestModel request, int id)
        {
            //use the type here to work out which column is the id column and pass to the base service class

            //need to get the id column for the type in question

            //var arg = _mapper.Map<PlatformSettingsModel>(request);
            var arg = _mapper.Map<PlatformSettingsQuery>(request);
            arg.Where.Add("id", id.ToString());

            return _mapper.Map<PrecacheSearchPlus, PlatformOverviewResponseModel>(await _precacheSearchPlusService.GetById(arg));
        }

       

    }
}
