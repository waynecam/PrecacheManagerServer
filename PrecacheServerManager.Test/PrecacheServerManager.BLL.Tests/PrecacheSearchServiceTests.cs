using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Moq;
using PrecacheManagerServer.BLL.Services;
using PrecacheManagerServer.Controllers;
using PrecacheManagerServer.API.Models;
using PrecacheManagerServer.Shared.Enums;
using PrecacheManagerServer.Shared.Models;
using PrecacheManagerServer.BLL.Models;
using PrecacheManagerServer.DAL.Models;
using System.Threading.Tasks;
using Shouldly;
using AutoMapper;
using PrecacheManagerServer.API.Infrastructure;
using System.Linq;
using PrecacheManagerServer.Shared.Enums.Extensions;

namespace PrecacheServerManager.Test.PrecacheServerManager.BLL.Tests
{
    public class PrecacheSearchServiceTests :BLLTestbase
    {

        [Theory]
        [InlineData(ApplicationMode.GermanyMedia, 22)]
        public async Task GetPrecacheSearchesByApplicationModeAndSiteIdServiceTest(ApplicationMode appMode, int siteId)
        {

            var appModeId = appMode.GetAttribute<ApplicationModeIdAttribute>().ApplicationModeId;
            PlatformSettingsRequestModel platformSettingsRequestModel = GetFakeRequest(appMode, siteId);

            //setup mock mapper
            mockMapper = new Mock<IMapper>();
            SetupFakeMapper(mockMapper.Object);
            mockMapper.Setup(a => a.Map<PlatformSettingsQuery>(It.IsAny<PlatformSettingsRequestModel>())).Returns(new PlatformSettingsQuery() { ConnectionStrings = GetTestPlatformConnStrings(), Where = GetFakeWhereConditions(siteId) });
            mockMapper.Setup(a => a.Map<PrecacheSearchResponseModel>(It.IsAny<PrecacheSearch>())).Returns(GetFakePrecacheSearchResponseModel(appMode, siteId));

            //set up mock DAL precacheService layer
            var mockPrecacheSearchService = new Mock<IBaseService<PrecacheSearch>>();
            mockPrecacheSearchService.Setup(a => a.GetAsync(It.IsAny<PlatformSettingsQuery>())).ReturnsAsync(GetFakePrecacheSearchResults(appMode, siteId));

            //create the real BLL precacheService layer
            var precacheSearchService = new PrecacheSearchService(mockPrecacheSearchService.Object, mockMapper.Object);

            //act
            var result = await precacheSearchService.GetAsync(platformSettingsRequestModel);

            var finalResult = result.ToList();

            //assert
            finalResult.ShouldNotBe(null);

            finalResult.Count().ShouldBe(1);

            finalResult[0].SiteId.ShouldBe(siteId);

            finalResult[0].ApplicationMode.ShouldBe((int)appMode);

        }



        #region helper methods
        private PlatformSettingsRequestModel GetFakeRequest(ApplicationMode appMode, int siteId)
        {

            //set up fake request
            var platformSettingsRequestModel = new PlatformSettingsRequestModel();
            var constrings = GetTestPlatformConnStrings().Where(a => a.Key == appMode).Select(b => b);
            platformSettingsRequestModel.ConnectionStrings = constrings.ToDictionary(a => a.Key, a => a.Value);
            platformSettingsRequestModel.Where = GetFakeWhereConditions(siteId);
            return platformSettingsRequestModel;
        }

        private static PrecacheSearchResponseModel GetFakePrecacheSearchResponseModel(ApplicationMode appMode, int siteId)
        {
            return new PrecacheSearchResponseModel() { ApplicationMode = (int)appMode, Id = 1, SiteId = siteId };
        }

        private Dictionary<string, string> GetFakeWhereConditions(int siteId)
        {

            var where = new Dictionary<string, string>();

            where.Add("siteid", siteId.ToString());

            return where;

        }

        public void testing<T>(TableNameAttribute data)
        {

        }

        private IEnumerable<PrecacheSearch> GetFakePrecacheSearchResults(ApplicationMode appMode, int siteId)
        {
            var result = new List<PrecacheSearch>();

            var pr1 = new PrecacheSearch() { ApplicationMode = (int)appMode, Id = 1, SiteId = siteId};

            result.Add(pr1);

            return result as IEnumerable<PrecacheSearch>;

        }

        #endregion



    }
}
