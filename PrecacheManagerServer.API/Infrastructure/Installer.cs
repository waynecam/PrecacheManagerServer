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
using PrecacheRerun = PrecacheManagerServer.DAL.Models.PrecacheRerun;
using PrecacheManagerServer.Shared.Models;

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

            services.AddTransient<IBaseRepository<PrecacheSearchPlus>, BaseRepository<PrecacheSearchPlus>>();
            services.AddTransient<IBaseService<PrecacheSearchPlus>, BaseService<PrecacheSearchPlus>>();
            services.AddTransient<IPlatformOverviewService, PlatformOverviewService>();

            services.AddTransient<IBaseRepository<PrecacheSearchItemsCreated>, BaseRepository<PrecacheSearchItemsCreated>>();
            services.AddTransient<IBaseService<PrecacheSearchItemsCreated>, BaseService<PrecacheSearchItemsCreated>>();
            services.AddTransient<IPrecacheSearchItemsCreatedService, PrecacheSearchItemsCreatedService>();

            services.AddTransient<IBaseRepository<LoggedPrecacheSearchItem>, BaseRepository<LoggedPrecacheSearchItem>>();
            services.AddTransient<IBaseService<LoggedPrecacheSearchItem>, BaseService<LoggedPrecacheSearchItem>>();
            services.AddTransient<ILoggedPrecacheSearchItemService, LoggedPrecacheSearchItemService>();

            services.AddTransient<IPlatformSettings, PlatformSettings>();
            services.AddTransient<IDataMapper, DataMapper>();
            services.AddTransient<IDBContext, DBContext>();


            services.AddTransient<IBaseRepository<PrecacheRerun>, BaseRepository<PrecacheRerun>>();
            services.AddTransient<IBaseService<PrecacheRerun>, BaseService<PrecacheRerun>>();
            services.AddTransient<IPrecacheRerunService, PrecacheRerunService>();
            services.AddTransient<IMembershipService, MemberShipService>();


            //services.AddTransient<IErrorHandler, ErrorHandler>();
        }
    }
}
