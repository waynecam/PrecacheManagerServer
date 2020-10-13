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

namespace PrecacheServerManager.Test.PrecacheServerManager.Api.Tests
{
    public class PlatformOverviewTests
    {

        
        public PlatformOverviewTests()
        {
            //some initialize stuff here
        }
        [Theory]
        [InlineData(ApplicationMode.GermanyMedia, 2)]
        [InlineData(ApplicationMode.International, 3)]
        [InlineData(ApplicationMode.Australia, 1)]
        public async Task ShouldReturnGermanyInternationalAustraliaPlatformOverviewsWithPrecacheSites(ApplicationMode applicationMode, int expectedPrecacheSiteCount)
        {
            var mockPlatformOverviewService = new Mock<IPlatformOverviewService>();

            mockPlatformOverviewService.CallBase = true;

            var mockServiceProvider = new Mock<IServiceProvider>();

            var mockPlatformSettings = new Mock<IPlatformSettings>();

            //mockPlatformOverviewService.Setup(x => x.GetAsync(It.IsAny<PlatformSettingsRequestModel>())).Returns(Task.FromResult(GetPlatformOverviewResponse() as IEnumerable<PlatformOverviewResponseModel>));
            //mockPlatformOverviewService.Setup(x => x.GetAsync(It.IsAny<PlatformSettingsRequestModel>())).ReturnsAsync(GetPlatformOverviewResponse());

            //mockPlatformOverviewService.Setup(a => a.GetAsync(It.IsAny<PlatformSettingsRequestModel>())).ReturnsAsync((ApplicationMode appMode) =>
            //{
            //    return GetPlatformOverviewResponse(applicationMode);
            //});


            mockPlatformOverviewService.Setup(a => a.GetAsync(It.IsAny<PlatformSettingsRequestModel>())).ReturnsAsync(GetPlatformOverviewResponse(applicationMode));

            var mockController = new Mock<PlatformOverviewController>(mockServiceProvider.Object, mockPlatformOverviewService.Object, mockPlatformSettings.Object);

            mockController.SetupGet(x => x.CurrentUser).Returns(GetTestUser());

            

            mockController.CallBase = true;

        

            var result = await mockController.Object.GetPlatformOverviews();

            var platformOverviewResponseModelList = (List<PlatformOverviewResponseModel>) result.Data;

            platformOverviewResponseModelList.ShouldNotBe(null);

            platformOverviewResponseModelList.ShouldBeOfType(typeof(List<PlatformOverviewResponseModel>));

            var platformOverview = platformOverviewResponseModelList[0];

            platformOverview.ApplicationMode.ShouldBe(applicationMode);

            platformOverview.PrecacheSites.Count.ShouldBe(expectedPrecacheSiteCount);



        }



        public PrecacheManagerServer.API.Models.User GetTestUser()
        {
            var u = new PrecacheManagerServer.API.Models.User();

            u.ApplicationModes = new List<ApplicationMode>() { ApplicationMode.GermanyMedia };
            u.PlatformSettings = new PlatformSettings() { ConnectionStrings = new Dictionary<ApplicationMode, string>()};
            u.PlatformSettings.ConnectionStrings.Add(ApplicationMode.GermanyMedia, "");
            u.UserName = "Test";

            return u;
        }


        public List<PlatformOverviewResponseModel> GetPlatformOverviewResponse(ApplicationMode appMode)
        {
            var result = new List<PlatformOverviewResponseModel>();

            var responseModel = new PlatformOverviewResponseModel();

            switch (appMode)
            {
                case ApplicationMode.GermanyMedia:
                    responseModel.ApplicationMode = ApplicationMode.GermanyMedia;
                    responseModel.PrecacheSites = new List<PrecacheSite>()
                    {
                        new PrecacheSite()
                        {
                            ApplicationMode = ApplicationMode.GermanyMedia
                        },
                        new PrecacheSite()
                        {
                            ApplicationMode = ApplicationMode.GermanyMedia
                        }

                    };
                    break;
                case ApplicationMode.International:
                    responseModel.ApplicationMode = ApplicationMode.International;
                    responseModel.PrecacheSites = new List<PrecacheSite>()
                    {
                        new PrecacheSite()
                        {
                            ApplicationMode = ApplicationMode.International
                        },
                        new PrecacheSite()
                        {
                            ApplicationMode = ApplicationMode.International
                        }
                        ,
                        new PrecacheSite()
                        {
                            ApplicationMode = ApplicationMode.International
                        }

                    };
                    break;
                case ApplicationMode.Australia:
                    responseModel.ApplicationMode = ApplicationMode.Australia;
                    responseModel.PrecacheSites = new List<PrecacheSite>()
                    {
                        new PrecacheSite()
                        {
                            ApplicationMode = ApplicationMode.Australia
                        }
                    };
                    break;
            }

            
            

            result.Add(responseModel);

            return result;
        }


    }
}
