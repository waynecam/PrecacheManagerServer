using PrecacheManagerServer.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrecacheManagerServer.API.Services
{
    public interface ITokenAuthenticateService
    {
        bool IsAuthenticated(TokenRequest request, out string token);
    }
}
