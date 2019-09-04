using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using PrecacheManagerServer.BLL.Models;

namespace PrecacheManagerServer.BLL.Services
{
    public interface IPrecacheSearchService
    {

        Task<IEnumerable<PrecacheSearchResponseModel>> GetAsync(PlatformSettingsRequestModel request);
        Task<PrecacheSearchResponseModel> GetById(int id);

        IEnumerable<PrecacheSearchResponseModel> Where(string sql);
    }
}
