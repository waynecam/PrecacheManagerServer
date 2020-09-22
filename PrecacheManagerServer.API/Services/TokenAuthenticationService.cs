using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PrecacheManagerServer.API.Models;
using PrecacheManagerServer.BLL.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace PrecacheManagerServer.API.Services
{
    public class TokenAuthenticationService : ITokenAuthenticateService
    {
        private readonly IUserManagementService _userManagementService;
        private readonly TokenManagement _tokenManagement;

        public TokenAuthenticationService(IUserManagementService service, IOptions<TokenManagement> tokenManagement)
        {
            _userManagementService = service;
            _tokenManagement = tokenManagement.Value;
        }
        public bool IsAuthenticated(TokenRequest request, out string token)
        {

            token = string.Empty;
            UserResponseModel user = _userManagementService.GetUser(request.Username, request.Password);

            //if (!_userManagementService.IsValidUser(request.Username, request.Password))
            //{
            //    return false;
            //}

            if (user == null) return false;



            //var claim = new[]
            //{
            //    new Claim(ClaimTypes.Name, request.Username),
            //    new Claim(ClaimTypes.Email, "wayne.campbell@test.com")
            //};


            //foreach( var appMode in user.ApplicationModes)
            //{
            //    claim.Append(new Claim(appMode.ToString(), appMode.ToString()));
            //}


            var claim = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, request.Username),
                new Claim(ClaimTypes.Email, "wayne.campbell@test.com")
            };


            foreach (var appMode in user.ApplicationModes)
            {
               
                claim.Add(new Claim(appMode.ToString(), appMode.ToString()));
            }


            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenManagement.Secret));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var jwtToken = new JwtSecurityToken(
                _tokenManagement.Issuer,
                _tokenManagement.Audience,
                claim,
                expires: DateTime.Now.AddMinutes(_tokenManagement.AccessExpiration),
                signingCredentials: credentials
            );
            token = new JwtSecurityTokenHandler().WriteToken(jwtToken);
            return true;

        }
    }
}
