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
using PrecacheManagerServer.API.Controllers;

namespace PrecacheServerManager.Test.PrecacheServerManager.Api.Tests
{
    public class LoggedPrecacheSearchItemControllerTests :APITestbase
    {

        [Theory]
        [InlineData(ApplicationMode.GermanyMedia,20, 1)]
        [InlineData(ApplicationMode.International, 21, 2)]
        //[InlineData(ApplicationMode.Australia, 22, 3)]
        public async Task GetLoggedPrecacheSearchItemByApplicationModeIdAndSiteIdTest(ApplicationMode applicationMode, int siteId, int expectedLoggedPrecacheSearchItemCount)
       {
            mockServiceProvider = new Mock<IServiceProvider>();

            mockPlatformSettings = new Mock<IPlatformSettings>();

            var mockLoggedPrecacheSearchItemService = new Mock<ILoggedPrecacheSearchItemService>();

            mockLoggedPrecacheSearchItemService.Setup(a => a.GetAsync(It.IsAny<PlatformSettingsRequestModel>())).ReturnsAsync(GetLoggedPrecacheSearchResults(applicationMode, siteId));

            var mockloggedPrecacheSearchItemcontroller = new Mock<LoggedPrecacheSearchItemController>(mockServiceProvider.Object, mockLoggedPrecacheSearchItemService.Object, mockPlatformSettings.Object);

            mockloggedPrecacheSearchItemcontroller.Setup(a => a.CurrentUser).Returns(GetTestUser(applicationMode));

            var applicationModeId = (int)applicationMode.GetAttribute<ApplicationModeIdAttribute>().ApplicationModeId;

            var result = await mockloggedPrecacheSearchItemcontroller.Object.GetByApplicationModeAndSiteId(applicationModeId, siteId);


            var loggedPrecacheSearchResponseModelList = (List<LoggedPrecacheSearchItemResponseModel>)result.Data;

            loggedPrecacheSearchResponseModelList.ShouldNotBe(null);

            loggedPrecacheSearchResponseModelList.ShouldBeOfType(typeof(List<LoggedPrecacheSearchItemResponseModel>));


            loggedPrecacheSearchResponseModelList.All(a => a.ApplicationMode == (int)applicationMode).ShouldBeTrue();

            loggedPrecacheSearchResponseModelList.Count.ShouldBe(expectedLoggedPrecacheSearchItemCount);



        }


        #region helper Methods
        public IEnumerable<LoggedPrecacheSearchItemResponseModel> GetLoggedPrecacheSearchResults(ApplicationMode appMode, int siteId)
        {
            var result = new List<LoggedPrecacheSearchItemResponseModel>();

            switch (appMode)
            {
                case ApplicationMode.GermanyMedia:
                    var responseModelGer1 = new LoggedPrecacheSearchItemResponseModel() { ApplicationMode = (int)ApplicationMode.GermanyMedia, SiteId = 20 };
                    var responseModelGer2 = new LoggedPrecacheSearchItemResponseModel() { ApplicationMode = (int)ApplicationMode.GermanyMedia,  SiteId = 30 };
                    var responseModelGer3 = new LoggedPrecacheSearchItemResponseModel() { ApplicationMode = (int)ApplicationMode.GermanyMedia, SiteId = 10 };

                    result.Add(responseModelGer1);
                    result.Add(responseModelGer2);
                    result.Add(responseModelGer3);

                    break;
                case ApplicationMode.International:
                    var responseModelInt1 = new LoggedPrecacheSearchItemResponseModel() { ApplicationMode = (int)ApplicationMode.International, SiteId = 21 };
                    var responseModelInt2 = new LoggedPrecacheSearchItemResponseModel() { ApplicationMode = (int)ApplicationMode.International, SiteId = 21 };

                    result.Add(responseModelInt1);
                    result.Add(responseModelInt2);

                    break;
                case ApplicationMode.Australia:

                    var responseModelAus1 = new LoggedPrecacheSearchItemResponseModel() { ApplicationMode = (int)ApplicationMode.Australia, SiteId = 22 };
                    var responseModelAus2 = new LoggedPrecacheSearchItemResponseModel() { ApplicationMode = (int)ApplicationMode.Australia, SiteId = 22 };
                    var responseModelAus3 = new LoggedPrecacheSearchItemResponseModel() { ApplicationMode = (int)ApplicationMode.Australia, SiteId = 22 };

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
