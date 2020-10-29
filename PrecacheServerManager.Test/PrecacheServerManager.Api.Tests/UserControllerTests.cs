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
using System.Security.Claims;
using System.Security.Principal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using PrecacheManagerServer.API.Services;

namespace PrecacheServerManager.Test.PrecacheServerManager.Api.Tests
{
    public class UserControllerTests :APITestbase
    {


        [Fact]
        public void BuildCurrentUserTest()
        {
            mockServiceProvider = new Mock<IServiceProvider>();

            var mockPlatformServiceConfigs = new Mock<IPlatformConfigService>();

            mockPlatformServiceConfigs.Setup(a => a.ConnectionStrings).Returns(GetTestPlatformConnStrings());

            mockServiceProvider.Setup(a => a.GetService(typeof(IPlatformConfigService))).Returns(mockPlatformServiceConfigs.Object);



            var userController = new UserController(mockServiceProvider.Object);

            //https://stackoverflow.com/questions/41400030/mock-httpcontext-for-unit-testing-a-net-core-mvc-controller
            userController.ControllerContext.HttpContext = GetMockHttpContext().Object;



            userController.BuildCurrentUser();

            userController.CurrentUser.ApplicationModes.Count().ShouldBe(2);

            userController.CurrentUser.PlatformSettings.ConnectionStrings.Count().ShouldBe(2);






        }


        #region helper methods

        public Mock<HttpContext> GetMockHttpContext()
        {

            //https://stackoverflow.com/questions/49330154/mock-user-identity-in-asp-net-core-for-unit-testing/49330263
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, "Test Name")
            };

            claims.Add(new Claim(ApplicationMode.International.ToString(), ApplicationMode.International.ToString()));
            claims.Add(new Claim(ApplicationMode.GermanyMedia.ToString(), ApplicationMode.GermanyMedia.ToString()));

            var identity = new ClaimsIdentity(claims, "Test");
            var claimsPrincipal = new ClaimsPrincipal(identity);

            var mockPrincipal = new Mock<IPrincipal>();

            mockPrincipal.Setup(a => a.Identity).Returns(identity);

            mockPrincipal.Setup(a => a.IsInRole(It.IsAny<string>())).Returns(true);

            var mockHttpContext = new Mock<HttpContext>();

            mockHttpContext.Setup(a => a.User).Returns(claimsPrincipal);

            return mockHttpContext;
        }

        #endregion


    }





}
