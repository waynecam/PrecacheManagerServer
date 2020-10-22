﻿using System;
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
    public class PrecacheRerunControllerTests :TestBase
    {
        [Theory]
        [InlineData(ApplicationMode.GermanyMedia, "c3c9279c-c43f-4677-8722-638fd258ee1c", true)]
        [InlineData(ApplicationMode.GermanyMedia, "2d021543-0140-4577-b5fa-ec48a777273f", false)]
        public async Task RerunPrecacheSearchTest(ApplicationMode appMode, string precacheIntegrityKey, bool expectedResult)
        {

            var mockPrecacheRerunService = new Mock<IPrecacheRerunService>();

            mockPrecacheRerunService.Setup(a => a.AddOrUpdateSP(It.IsAny<PlatformSettingRequestsModelAddOrUpdate<PrecacheManagerServer.API.Models.PrecacheRerun>>())).ReturnsAsync(expectedResult);

            var mockPlatformsEttings = new Mock<IPlatformSettings>();

            var mockPrecacheRerunController = new Mock<PrecacheRerunController>(mockServiceProvider.Object, mockPrecacheRerunService.Object, mockPlatformSettings.Object);

            mockPrecacheRerunController.CallBase = true;

            mockPrecacheRerunController.Setup(a => a.CurrentUser).Returns(GetTestUser(appMode));

            var precacheRerun = GetTestPrecacheRerun(precacheIntegrityKey, appMode);

            var appModeId = appMode.GetAttribute<ApplicationModeIdAttribute>().ApplicationModeId;


            var result = await mockPrecacheRerunController.Object.RerunFailedPrecacheSearch(appModeId, precacheRerun);


            result.Data.ShouldBe(expectedResult);


        }

        #region Helper Methods
        private PrecacheManagerServer.API.Models.PrecacheRerun GetTestPrecacheRerun(string precacheIntegrityKey, ApplicationMode applicationMode)
        {
            var precacheRerun = new PrecacheManagerServer.API.Models.PrecacheRerun();


            

            switch (precacheIntegrityKey)
            {
                case "c3c9279c-c43f-4677-8722-638fd258ee1c":
                    precacheRerun.PrecacheIntegrityKey = Guid.Parse(precacheIntegrityKey);
                    precacheRerun.Applicationmode = applicationMode.GetAttribute<ApplicationModeIdAttribute>().ApplicationModeId;
                    break;
                case "2d021543-0140-4577-b5fa-ec48a777273f":
                    //this will cause a failure

                    break;

            }



            return precacheRerun;

        }

        #endregion



    }
}
