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
    public class LoggedPrecacheSearchItemServiceTests : BLLTestbase
    {

        [Theory]
        [InlineData(ApplicationMode.GermanyMedia)]
        public async Task GetLoggedPrecacheSearchesByApplicationModeAndSiteIdServiceTest(ApplicationMode appMode)
        {

            var appModeId = appMode.GetAttribute<ApplicationModeIdAttribute>().ApplicationModeId;
            //setup mock mapper
            mockMapper = new Mock<IMapper>();
            SetupFakeMapper(mockMapper.Object);
            mockMapper.Setup(a => a.Map<PlatformSettingsQuery>(It.IsAny<PlatformSettingsRequestModel>())).Returns(new PlatformSettingsQuery() { ConnectionStrings = GetTestPlatformConnStrings() });
            mockMapper.Setup(a => a.Map<LoggedPrecacheSearchItemResponseModel>(It.IsAny<LoggedPrecacheSearchItem>())).Returns(GetFakeLoggedPrecacheItemResponseModelResults(appMode));

            //set up fake Rdquest
            var fakeRequest = GetFakeRequest(appMode);

            //set up mockloggedprecacheitem DAL service and results
            var mockLoggedPrecacheItemService = new Mock<IBaseService<LoggedPrecacheSearchItem>>();
            mockLoggedPrecacheItemService.Setup(x => x.GetAsync(It.IsAny<PlatformSettingsQuery>())).ReturnsAsync(GetFakeLoggedPrecacheItemResults(appMode));

            var loggedPrecacheSeachItemService = new LoggedPrecacheSearchItemService(mockLoggedPrecacheItemService.Object, mockMapper.Object);

            var result = await loggedPrecacheSeachItemService.GetAsync(fakeRequest);

            result.ShouldNotBe(null);

            result.Count().ShouldBe(1);

            result.FirstOrDefault().ApplicationMode.ShouldBe((int)appMode);
            result.FirstOrDefault().ApplicationModeId.ShouldBe(appMode.GetAttribute<ApplicationModeIdAttribute>().ApplicationModeId);

        }

        private LoggedPrecacheSearchItemResponseModel GetFakeLoggedPrecacheItemResponseModelResults(ApplicationMode appMode)
        {
            var result = new LoggedPrecacheSearchItemResponseModel() { ApplicationMode = (int)appMode };

            return result;
        }

        private IEnumerable<LoggedPrecacheSearchItem> GetFakeLoggedPrecacheItemResults(ApplicationMode appMode)
        {
            var result = new List<LoggedPrecacheSearchItem>();

            result.Add(new LoggedPrecacheSearchItem() { ApplicationMode = (int)appMode});

            return result;
        }

        private PlatformSettingsRequestModel GetFakeRequest(ApplicationMode appMode)
        {

            //set up fake request
            var platformSettingsRequestModel = new PlatformSettingsRequestModel();
            var constrings = GetTestPlatformConnStrings().Where(a => a.Key == appMode).Select(b => b);
            platformSettingsRequestModel.ConnectionStrings = constrings.ToDictionary(a => a.Key, a => a.Value);
            return platformSettingsRequestModel;
        }
    }
}
