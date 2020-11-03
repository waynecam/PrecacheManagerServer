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
    public class PlatformOverviewServiceTests :BLLTestbase
    {

        private const string GERMANY_TESTSITE_NAME = "Germany Test SiteName";
        private const string INTERNATIONAL_TESTSITE_NAME = "Int Test SiteName";
        private const string AUSTRALIA_TESTSITE_NAME = "Aus Test SiteName";

        [Theory]
        [InlineData(ApplicationMode.GermanyMedia,1, 1,1,2, "Germany", GERMANY_TESTSITE_NAME)]
        [InlineData(ApplicationMode.International,1, 1, 2, 1, "International", INTERNATIONAL_TESTSITE_NAME)]
        //[InlineData(ApplicationMode.Australia,1, 1, 1, 1, AUSTRALIA_TESTSITE_NAME)]
        public async Task ShouldReturnGermanyInternationalAustraliaPlatformOverviewsWithPrecacheSitesServiceTest(ApplicationMode appMode,
            int expectedPlatformOverviewModelCount,
            int expectedPrecacheSearchPlusCount, 
            int expectedPrecacheSearchCreatedCount,
            int expectedLoggedPrecacheSearchCount,
            string applicationModeDescription,
            string testSiteName
            )
        {
            //arrange
            var appModeId = appMode.GetAttribute<ApplicationModeIdAttribute>().ApplicationModeId;

            var platformSettingsRequestModel = new PlatformSettingsRequestModel();
            platformSettingsRequestModel.ConnectionStrings = GetTestPlatformConnStrings();

            mockPrecacheSearchPlusService = new Mock<IBaseService<PrecacheSearchPlus>>();
            mockPrecacheSearchPlusService.Setup(a => a.GetAsync(It.IsAny<PlatformSettingsQuery>())).ReturnsAsync(GetFakePrecacheSearchPlusResult(appModeId));

            mockLoggedPrecacheSearchService = new Mock<IBaseService<LoggedPrecacheSearchItem>>();
            mockLoggedPrecacheSearchService.Setup(a => a.GetAsync(It.IsAny<PlatformSettingsQuery>())).ReturnsAsync(GetFakeLoggedprecacheSearchResults(appModeId));
            
            mockPrecacheSearchesCreatedService = new Mock<IBaseService<PrecacheSearchItemsCreated>>();
            mockPrecacheSearchesCreatedService.Setup(a => a.GetAsync(It.IsAny<PlatformSettingsQuery>())).ReturnsAsync(GetFakePrecacheSearchItemsCreatedResults(appModeId));
        
            mockMapper = new Mock<IMapper>();

            SetupFakeMapper(mockMapper.Object);

            mockMapper.Setup(a => a.Map<PlatformSettingsQuery>(It.IsAny<PlatformSettingsRequestModel>())).Returns(new PlatformSettingsQuery() { ConnectionStrings = GetTestPlatformConnStrings() });
            IPlatformOverviewService platformOverviewService = new PlatformOverviewService(mockPrecacheSearchPlusService.Object,
                mockLoggedPrecacheSearchService.Object, mockPrecacheSearchesCreatedService.Object, mockMapper.Object);


            //Act
            var result = await platformOverviewService.GetAsync(platformSettingsRequestModel);

            //Assertions
            result.ShouldNotBe(null);

            result.Count().ShouldBe(expectedPlatformOverviewModelCount);

            var platformOverview = result.ToList()[0];

            platformOverview.PlatformDescription.ShouldBe(applicationModeDescription);

            platformOverview.PrecacheSites.Count().ShouldBe(1);

            var precacheSite = platformOverview.PrecacheSites.FirstOrDefault();

            precacheSite.Name.ShouldBe(testSiteName);

            precacheSite.PrecacheSearches.Count().ShouldBe(expectedPrecacheSearchPlusCount);

            precacheSite.PrecacheSearchItemsCreated.Count().ShouldBe(expectedPrecacheSearchCreatedCount);

            precacheSite.LoggedPrecacheSearchItems.Count().ShouldBe(expectedLoggedPrecacheSearchCount);

        }

      



        #region helper methods

       


        private IEnumerable<PrecacheSearchPlus> GetFakePrecacheSearchPlusResult(int appModeId)
        {
            var result = new List<PrecacheSearchPlus>();

            result.Add(new PrecacheSearchPlus() { Id = 1, ApplicationMode = ApplicationMode.GermanyMedia, Name = GERMANY_TESTSITE_NAME });
            result.Add(new PrecacheSearchPlus() { Id = 1, ApplicationMode = ApplicationMode.International, Name = INTERNATIONAL_TESTSITE_NAME });
            result.Add(new PrecacheSearchPlus() { Id = 1, ApplicationMode = ApplicationMode.Australia, Name = AUSTRALIA_TESTSITE_NAME});

            return result.Where(a => ((int)a.ApplicationMode == appModeId)).Select(b => b);

        }

        private IEnumerable<PrecacheSearchItemsCreated> GetFakePrecacheSearchItemsCreatedResults(int appModeId)
        {
            var result = new List<PrecacheSearchItemsCreated>();
            result.Add(new PrecacheSearchItemsCreated() { ID = 1, ApplicationMode = (int)ApplicationMode.GermanyMedia });
            result.Add(new PrecacheSearchItemsCreated() { ID = 1, ApplicationMode = (int)ApplicationMode.International });
            result.Add(new PrecacheSearchItemsCreated() { ID = 2, ApplicationMode = (int)ApplicationMode.International });
            result.Add(new PrecacheSearchItemsCreated() { ID = 2, ApplicationMode = (int)ApplicationMode.Australia });

            return result.Where(a => a.ApplicationMode == (int)appModeId).Select(b => b); ;
        }

        private IEnumerable<LoggedPrecacheSearchItem> GetFakeLoggedprecacheSearchResults(int appModeId)
        {
            var result = new List<LoggedPrecacheSearchItem>();
            result.Add(new LoggedPrecacheSearchItem() { ID = 1, ApplicationMode = (int)ApplicationMode.GermanyMedia });
            result.Add(new LoggedPrecacheSearchItem() { ID = 2, ApplicationMode = (int)ApplicationMode.GermanyMedia });
            result.Add(new LoggedPrecacheSearchItem() { ID = 1, ApplicationMode = (int)ApplicationMode.International });
            result.Add(new LoggedPrecacheSearchItem() { ID = 2, ApplicationMode = (int)ApplicationMode.Australia });

            return result.Where(a => a.ApplicationMode == (int)appModeId).Select(b => b); ;
        }



        #endregion

    }
}
