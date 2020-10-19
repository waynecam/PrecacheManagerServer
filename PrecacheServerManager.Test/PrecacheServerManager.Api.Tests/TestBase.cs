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
    public class TestBase
    {
        internal Mock<IServiceProvider> mockServiceProvider;
        internal Mock<IPlatformSettings> mockPlatformSettings;

        public TestBase()
        {
           mockServiceProvider = new Mock<IServiceProvider>();
           mockPlatformSettings = new Mock<IPlatformSettings>();
        }


        public PrecacheManagerServer.API.Models.User GetTestUser(ApplicationMode appMode)
        {
            var u = new PrecacheManagerServer.API.Models.User();

            u.ApplicationModes = new List<ApplicationMode>() { appMode };
            u.PlatformSettings = new PlatformSettings() { ConnectionStrings = new Dictionary<ApplicationMode, string>() };
            u.PlatformSettings.ConnectionStrings.Add(appMode, "");
            u.UserName = "TestUser@test.com";

            return u;
        }
    }
}
