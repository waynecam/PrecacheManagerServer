using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using PrecacheManagerServer.BLL.Models;

namespace PrecacheManagerServer.BLL.Services
{
    public interface IPrecacheSearchItemsCreatedService
    {
        Task<IEnumerable<PrecacheSearchItemsCreatedResponseModel>> GetAsync(PlatformSettingsRequestModel request);
        Task<PrecacheSearchItemsCreatedResponseModel> GetById(PlatformSettingsRequestModel request, int id);
    }
}
