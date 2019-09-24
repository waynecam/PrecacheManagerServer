using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using PrecacheManagerServer.BLL.Models;

namespace PrecacheManagerServer.BLL.Services
{
    public interface ILoggedPrecacheSearchItemService
    {
        Task<IEnumerable<LoggedPrecacheSearchItemResponseModel>> GetAsync(PlatformSettingsRequestModel request);
        Task<LoggedPrecacheSearchItemResponseModel> GetById(PlatformSettingsRequestModel request, int id);
    }
}
