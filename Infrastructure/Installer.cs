using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
//using PrecacheManagerServer.BLL.Services;
//using PrecacheManagerServer.BLL.Repositorys;
//using PrecacheManagerServer.BLL.Services;
using PrecacheManagerServer.DAL.Models;
//using PrecacheManagerServer.BLL.Repositorys;
//using PrecacheManagerServer.BLL.Services;

namespace PrecacheManagerServer.API.Infrastructure
{
    public class Installer
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            //services.AddDbContext<DataContext>();

            services.AddTransient<BLL.Repositorys.IBaseRepository<PrecacheSearch>, BLL.Repositorys.BaseRepository<PrecacheSearch>>();
            services.AddTransient<BLL.Services.IBaseService<PrecacheSearch>, BLL.Services.BaseService<PrecacheSearch>>();
            services.AddTransient<BLL.Services.IPrecacheSearchService, BLL.Services.PrecacheSearchService>();
            //services.AddTransient<IErrorHandler, ErrorHandler>();
        }
    }
}
