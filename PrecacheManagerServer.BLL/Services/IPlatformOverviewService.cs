using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using PrecacheManagerServer.BLL.Models;

namespace PrecacheManagerServer.BLL.Services
{
    public interface IPlatformOverviewService
    {

            Task<IEnumerable<PlatformOverviewResponseModel>> GetAsync(PlatformSettingsRequestModel request);
            Task<PlatformOverviewResponseModel> GetById(PlatformSettingsRequestModel request, int id);
       

    }
}
