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
using System.Linq;
using PrecacheManagerServer.Shared.Enums.Extensions;

namespace PrecacheServerManager.Test.PrecacheServerManager.Api.Tests
{
    public class PrecacheSearchControllerTests :TestBase
    {




        [Theory]
        //[InlineData(ApplicationMode.GermanyMedia,20, 2)]
        //[InlineData(ApplicationMode.International,21, 1)]
        [InlineData(ApplicationMode.Australia,22, 3)]
        public async Task ShouldReturnPrecacheSearchesByApplicationModeAndSiteIdTest(ApplicationMode applicationMode,int siteId, int expectedPrecacheSearchesCount)
        {

            var mockPrecacheSearchService = new Mock<IPrecacheSearchService>();

            mockPrecacheSearchService.Setup(a => a.GetAsync(It.IsAny<PlatformSettingsRequestModel>())).ReturnsAsync(GetTestPrecacheSearchResponseModels(applicationMode, siteId));
            
            var mockPrecacheSearchModelController = new Mock<PrecacheSearchController>(mockServiceProvider.Object, mockPrecacheSearchService.Object, mockPlatformSettings.Object);

            mockPrecacheSearchModelController.Setup(a => a.CurrentUser).Returns(GetTestUser(applicationMode));

            mockPrecacheSearchModelController.CallBase = true;

            var applicationModeId = (int)applicationMode.GetAttribute<ApplicationModeIdAttribute>().ApplicationModeId;

            var result = await mockPrecacheSearchModelController.Object.GetByApplicationModeAndSiteId(applicationModeId, siteId);

            var precacheSearchResponseModelList = (List<PrecacheSearchResponseModel>)result.Data;

            precacheSearchResponseModelList.ShouldNotBe(null);

            precacheSearchResponseModelList.ShouldBeOfType(typeof(List<PrecacheSearchResponseModel>));


            precacheSearchResponseModelList.All(a => a.ApplicationMode == (int)applicationMode).ShouldBeTrue();

            precacheSearchResponseModelList.Count.ShouldBe(expectedPrecacheSearchesCount);

        }


        #region    Helper Methods

        public IEnumerable<PrecacheSearchResponseModel> GetTestPrecacheSearchResponseModels(ApplicationMode appMode, int siteId)
        {

            var result = new List<PrecacheSearchResponseModel>();
           
            switch (appMode)
            {
                case ApplicationMode.GermanyMedia:
                    var responseModelGer1 = new PrecacheSearchResponseModel() { ApplicationMode = (int)ApplicationMode.GermanyMedia, Id =1, SiteId= 20 };
                    var responseModelGer2 = new PrecacheSearchResponseModel() { ApplicationMode = (int)ApplicationMode.GermanyMedia, Id= 2, SiteId=30 };
                    var responseModelGer3 = new PrecacheSearchResponseModel() { ApplicationMode = (int)ApplicationMode.GermanyMedia, Id= 3 , SiteId =20};

                    result.Add(responseModelGer1);
                    result.Add(responseModelGer2);
                    result.Add(responseModelGer3);

                    break;
                case ApplicationMode.International:
                    var responseModelInt1 = new PrecacheSearchResponseModel() { ApplicationMode = (int)ApplicationMode.International, SiteId = 21 };
                    var responseModelInt2 = new PrecacheSearchResponseModel() { ApplicationMode = (int)ApplicationMode.International, SiteId = 2 };

                    result.Add(responseModelInt1);
                    result.Add(responseModelInt2);
               
                    break;
                case ApplicationMode.Australia:

                    var responseModelAus1 = new PrecacheSearchResponseModel() { ApplicationMode = (int)ApplicationMode.Australia, SiteId = 22 };
                    var responseModelAus2 = new PrecacheSearchResponseModel() { ApplicationMode = (int)ApplicationMode.Australia, SiteId = 22 };
                    var responseModelAus3 = new PrecacheSearchResponseModel() { ApplicationMode = (int)ApplicationMode.Australia, SiteId = 22 };

                    result.Add(responseModelAus1);
                    result.Add(responseModelAus2);
                    result.Add(responseModelAus3);
                    break;
            }


            result = result.Where(a => a.SiteId == siteId).Select(b => b).ToList();

            return result;

        }


        #endregion

    }
}
