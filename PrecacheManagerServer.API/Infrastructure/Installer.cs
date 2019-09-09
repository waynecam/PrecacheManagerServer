using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using PrecacheManagerServer.API.Models;
using PrecacheManagerServer.BLL.Repositorys;
using PrecacheManagerServer.BLL.Services;
using PrecacheManagerServer.DAL.Contexts;
using PrecacheManagerServer.DAL.Mappers;
using PrecacheManagerServer.DAL.Models;


namespace PrecacheManagerServer.API.Infrastructure
{
    public class Installer
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            //services.AddDbContext<DataContext>();

            services.AddTransient<IBaseRepository<PrecacheSearch>,BaseRepository<PrecacheSearch>>();
            services.AddTransient<IBaseService<PrecacheSearch>,BaseService<PrecacheSearch>>();
            services.AddTransient<IPrecacheSearchService,PrecacheSearchService>();

            services.AddTransient<IBaseRepository<PlatformOverview>, BaseRepository<PlatformOverview>>();
            services.AddTransient<IBaseService<PlatformOverview>, BaseService<PlatformOverview>>();
            services.AddTransient<IPlatformOverviewService, PlatformOverviewService>();

            services.AddTransient<IPlatformSettings, PlatformSettings>();
            services.AddTransient<IDataMapper, DataMapper>();
            services.AddTransient<IDBContext, DBContext>();
            //services.AddTransient<IErrorHandler, ErrorHandler>();
        }
    }
}
