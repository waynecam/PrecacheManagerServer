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
using PrecacheManagerServer.API.Controllers;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace PrecacheServerManager.Test.PrecacheServerManager.Api.Tests
{
   public class AuthenticationControllerTests :APITestbase
    {

        [Fact]
        public void GetAuthenticatedUserJWTTokenTest()
        {
            var mockUserManagementService = new Mock<IUserManagementService>();

            mockUserManagementService.Setup(a => a.GetUser(It.IsAny<string>(), It.IsAny<string>())).Returns(GetFakeUserResponseModel());

            var mockTokenManagement = new Mock<IOptions<TokenManagement>>();

            var mockTokenAuthenticationService = new Mock<ITokenAuthenticateService>();
            TokenRequest tokenRequest = GetFakeTokenRequest();
            List<Claim> claim = GetFakeClaims(tokenRequest);

            string token = string.Empty;

            mockTokenAuthenticationService.Setup(a => a.IsAuthenticated(It.IsAny<TokenRequest>(), out token)).Callback(() =>
            {
                token = MockJwtTokens.GenerateJwtToken(claim);
            }).Returns(true);


            var authenticationController = new AuthenticationController(mockTokenAuthenticationService.Object);


            var result = authenticationController.RequestToken(tokenRequest);


            token.ShouldNotBe(null);

            result.ShouldBeOfType<OkObjectResult>();


        }

        [Fact]
        public void FailedAuthenticatedUserNoJWTGeneratedTest()
        {
            var mockUserManagementService = new Mock<IUserManagementService>();

            mockUserManagementService.Setup(a => a.GetUser(It.IsAny<string>(), It.IsAny<string>())).Returns(() => null);

            var mockTokenManagement = new Mock<IOptions<TokenManagement>>();

            var mockTokenAuthenticationService = new Mock<ITokenAuthenticateService>();
            TokenRequest tokenRequest = GetFakeFailedTokenRequest();


            string token = string.Empty;
            List<Claim> claim = GetFakeClaims(tokenRequest);


            mockTokenAuthenticationService.Setup(a => a.IsAuthenticated(It.IsAny<TokenRequest>(), out token)).Callback(() =>
            {
                token = string.Empty;
            }).Returns(false);

            var authenticationController = new AuthenticationController(mockTokenAuthenticationService.Object);

            var result = authenticationController.RequestToken(tokenRequest);


            result.ShouldBeOfType<BadRequestObjectResult>();


        }


        #region helper methods

        private static List<Claim> GetFakeClaims(TokenRequest tokenRequest)
        {
            return new List<Claim>()
            {
                new Claim(ClaimTypes.Name, tokenRequest.Username),
                new Claim(ClaimTypes.Email, "wayne.campbell@test.com")
            };
        }

        private static TokenRequest GetFakeTokenRequest()
        {

            //var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(mockTokenManagement.Object.Value.Secret));
            //var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var tokenRequest = new TokenRequest();
            tokenRequest.Username = "testUserName";
            tokenRequest.Password = "testPassword";
            return tokenRequest;
        }

        private static TokenRequest GetFakeFailedTokenRequest()
        {

            //var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(mockTokenManagement.Object.Value.Secret));
            //var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var tokenRequest = new TokenRequest();
            tokenRequest.Username = "testUserName";
            tokenRequest.Password = "failingTestPassword";
            return tokenRequest;
        }


        private static UserResponseModel GetFakeUserResponseModel()
        {
            var userResponseModel = new UserResponseModel();
            userResponseModel.ApplicationModes = new List<ApplicationMode>() { ApplicationMode.GermanyMedia };
            return userResponseModel;
        }

        #endregion
    }
}
