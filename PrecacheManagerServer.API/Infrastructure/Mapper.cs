﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using PrecacheManagerServer.DAL.Models;
using PrecacheManagerServer.BLL.Models;
using PrecacheManagerServer.API.Models;
using System.Data;
using PrecacheRerunApi = PrecacheManagerServer.API.Models.PrecacheRerun;
using PrecacheRerunDal= PrecacheManagerServer.DAL.Models.PrecacheRerun;
using User = PrecacheManagerServer.DAL.Models.User;
using PrecacheManagerServer.Shared.Models;

namespace PrecacheManagerServer.API.Infrastructure
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Add as many of these lines as you need to map your objects
            CreateMap<PrecacheSearch, PrecacheSearchResponseModel>();
            CreateMap<PrecacheSearchResponseModel, PrecacheSearch>();

            //API > BLL
            CreateMap<PlatformSettings, PlatformSettingsRequestModel>();
            //BLL > DAL
            //CreateMap<PlatformSettingsRequestModel, PlatformSettingsModel>();
            CreateMap<PlatformSettingsRequestModel, PlatformSettingsQuery>();

            //API > BLL
            CreateMap(typeof(PlatformSettingRequestsModelAddOrUpdate<>), typeof(PlatformSettingsModelAddOrUpdate<>));
            //BLL > DAL
            CreateMap(typeof(PlatformSettingsModelAddOrUpdate<>), typeof(PlatformSettingRequestsModelAddOrUpdate<>));

            //API > BLL
            CreateMap(typeof(PlatformSettingRequestsModelAddOrUpdate<>), typeof(PlatformSettingsQueryAddUpdate<>));
            //BLL > DAL
            CreateMap(typeof(PlatformSettingsQueryAddUpdate<>), typeof(PlatformSettingRequestsModelAddOrUpdate<>));

            //var config = new MapperConfiguration(cfg =>
            //    cfg.CreateMap(typeof(PlatformSettingRequestsModelAddOrUpdate<>), typeof(PlatformSettingsModelAddOrUpdate<>)));

            //API > BLL
            CreateMap<PrecacheSearchItemsCreated, PrecacheSearchItemsCreatedResponseModel>();
            //BLL > DAL
            CreateMap<PrecacheSearchItemsCreatedResponseModel, PrecacheSearchItemsCreated>();


            //API > BLL
            CreateMap<LoggedPrecacheSearchItem, LoggedPrecacheSearchItemResponseModel>();
            //BLL > DAL
            CreateMap<LoggedPrecacheSearchItemResponseModel, LoggedPrecacheSearchItem>();

            //API > BLL
            CreateMap<PrecacheRerunApi, PrecacheRerunDal>();
            //BLL > DAL
            CreateMap<PrecacheRerunDal, PrecacheRerunApi>();


            //API > BLL
            CreateMap<User, UserResponseModel>();
            //BLL > DAL
            CreateMap<UserResponseModel, User>();



            //CreateMap<IDataReader, PrecacheSearch>();
            //CreateMap<PrecacheSearch, IDataReader>();

            DtToPrecacheSearchMapper();
            DtToPlatformOverviewMapper();
            DtToPrecacheSearchItemCreatedMapper();
            DtToLoggedPrecacheSearchItemMapper();


            ////https://stackoverflow.com/questions/18432173/auto-mapper-mapping-of-nested-object-within-a-collection
            //IMappingExpression<DataRow, PlatformOverview> platformOverviewMappingExpression;
            //platformOverviewMappingExpression = CreateMap<DataRow, PlatformOverview>();
            //platformOverviewMappingExpression.ForMember(d => d.ApplicationMode, o => o.MapFrom(s => s["ApplicationMode"]));
            //platformOverviewMappingExpression.ForMember(d => d.PrecacheSites, o => o.MapFrom(reader => new List<PrecacheSite>() { new PrecacheSite()
            //{
            //    Name = reader["Sitename"].ToString(),
            //    SiteId = Convert.ToInt32(reader["SiteId"].ToString()),
            //    ApplicationMode = (ApplicationMode)Convert.ToInt32(reader["ApplicationMode"].ToString())
            //} }));




            ////is this neeeded?
            ////https://stackoverflow.com/questions/49152317/how-to-map-a-simple-poco-into-a-complex-object-hierachy-using-automapper
            //IMappingExpression<IDataReader, PrecacheSite> precacheSiteMappingExpression;
            //precacheSiteMappingExpression = CreateMap<IDataReader, PrecacheSite>();
            //precacheSiteMappingExpression.ForMember(d => d.SiteId, o => o.MapFrom(s => s["SiteId"]));
            //precacheSiteMappingExpression.ForMember(d => d.Name, o => o.MapFrom(s => s["Sitename"]));
            //precacheSiteMappingExpression.ForMember(d => d.ApplicationMode, o => o.MapFrom(s => s["ApplicationMode"]));

            ////is this needed?
            //CreateMap<PlatformOverview, PrecacheSite>();
            //CreateMap<PrecacheSite, PlatformOverview>();







            //CreateMap<PlatformOverview, PlatformOverviewResponseModel>();
            //CreateMap<PlatformOverviewResponseModel, PlatformOverview>();



            //CreateMap<Course, CourseResponseModel>();
            //CreateMap<CourseResponseModel, Course>();

            //CreateMap<UserModel, AppUser>();
            //CreateMap<AppUser, UserModel>();

            //CreateMap<UserManager<UserModel>, UserManager<AppUser>>();
            //CreateMap<UserManager<AppUser>, UserManager<UserModel>>();
        }

        private void DtToPlatformOverviewMapper()
        {
            //https://stackoverflow.com/questions/35414228/using-automapper-to-map-a-datatable-to-an-object-dto
            IMappingExpression<DataRow, PrecacheSearchPlus> platformOverviewMappingExpression = CreateMap<DataRow, PrecacheSearchPlus>();
            platformOverviewMappingExpression.ForMember(d => d.Name, o => o.MapFrom(s => s["Sitename"]));
            platformOverviewMappingExpression.ForMember(d => d.Id, o => o.MapFrom(s => s["Id"]));
            platformOverviewMappingExpression.ForMember(d => d.CreatedDate, o => o.MapFrom(s => s["CreatedDate"]));
            platformOverviewMappingExpression.ForMember(d => d.LastUpdateDate, o => o.MapFrom(s => s["LastUpdateDate"]));
            platformOverviewMappingExpression.ForMember(d => d.IsDeleted, o => o.MapFrom(s => s["IsDeleted"]));
            platformOverviewMappingExpression.ForMember(d => d.DashboardSearchType, o => o.MapFrom(s => s["DashboardSearchType"]));
            platformOverviewMappingExpression.ForMember(d => d.SearchId, o => o.MapFrom(s => s["SearchId"]));
            platformOverviewMappingExpression.ForMember(d => d.SearchVersion, o => o.MapFrom(s => s["SearchVersion"]));
            platformOverviewMappingExpression.ForMember(d => d.ApplicationMode, o => o.MapFrom(s => s["ApplicationMode"]));
            platformOverviewMappingExpression.ForMember(d => d.PrecacheKey, o => o.MapFrom(s => s["PrecacheKey"]));
            platformOverviewMappingExpression.ForMember(d => d.AreaNo, o => o.MapFrom(s => s["AreaNo"]));
            platformOverviewMappingExpression.ForMember(d => d.SiteId, o => o.MapFrom(s => s["SiteId"]));
            platformOverviewMappingExpression.ForMember(d => d.HomePageSearchType, o => o.MapFrom(s => s["HomePageSearchType"]));
            platformOverviewMappingExpression.ForMember(d => d.DynamicPrecacheSearchId, o => o.MapFrom(s => s["DynamicPrecacheSearchId"]));
            platformOverviewMappingExpression.ForMember(d => d.AreaSearchName, o => o.MapFrom(s => s["AreaSearchName"]));
            platformOverviewMappingExpression.ForMember(d => d.PrecacheIntegrityKey, o => o.MapFrom(s => s["PrecacheIntegrityKey"]));
        }

        private void DtToPrecacheSearchMapper()
        {
            //https://stackoverflow.com/questions/35414228/using-automapper-to-map-a-datatable-to-an-object-dto
            IMappingExpression<DataRow, PrecacheSearch> precacheSearchMappingExpression = CreateMap<DataRow, PrecacheSearch>();
            precacheSearchMappingExpression.ForMember(d => d.Id, o => o.MapFrom(s => s["Id"]));
            precacheSearchMappingExpression.ForMember(d => d.CreatedDate, o => o.MapFrom(s => s["CreatedDate"]));
            precacheSearchMappingExpression.ForMember(d => d.LastUpdateDate, o => o.MapFrom(s => s["LastUpdateDate"]));
            precacheSearchMappingExpression.ForMember(d => d.IsDeleted, o => o.MapFrom(s => s["IsDeleted"]));
            precacheSearchMappingExpression.ForMember(d => d.DashboardSearchType, o => o.MapFrom(s => s["DashboardSearchType"]));
            precacheSearchMappingExpression.ForMember(d => d.SearchId, o => o.MapFrom(s => s["SearchId"]));
            precacheSearchMappingExpression.ForMember(d => d.SearchVersion, o => o.MapFrom(s => s["SearchVersion"]));
            precacheSearchMappingExpression.ForMember(d => d.ApplicationMode, o => o.MapFrom(s => s["ApplicationMode"]));
            precacheSearchMappingExpression.ForMember(d => d.PrecacheKey, o => o.MapFrom(s => s["PrecacheKey"]));
            precacheSearchMappingExpression.ForMember(d => d.AreaNo, o => o.MapFrom(s => s["AreaNo"]));
            precacheSearchMappingExpression.ForMember(d => d.SiteId, o => o.MapFrom(s => s["SiteId"]));
            precacheSearchMappingExpression.ForMember(d => d.HomePageSearchType, o => o.MapFrom(s => s["HomePageSearchType"]));
            precacheSearchMappingExpression.ForMember(d => d.DynamicPrecacheSearchId, o => o.MapFrom(s => s["DynamicPrecacheSearchId"]));
            precacheSearchMappingExpression.ForMember(d => d.AreaSearchName, o => o.MapFrom(s => s["AreaSearchName"]));
            precacheSearchMappingExpression.ForMember(d => d.PrecacheIntegrityKey, o => o.MapFrom(s => s["PrecacheIntegrityKey"]));
        }


        private void DtToPrecacheSearchItemCreatedMapper()
        {
            //https://stackoverflow.com/questions/35414228/using-automapper-to-map-a-datatable-to-an-object-dto
            IMappingExpression<DataRow, PrecacheSearchItemsCreated> precacheSearchMappingExpression = CreateMap<DataRow, PrecacheSearchItemsCreated>();
            precacheSearchMappingExpression.ForMember(d => d.ID, o => o.MapFrom(s => s["Id"]));
            precacheSearchMappingExpression.ForMember(d => d.CreatedDate, o => o.MapFrom(s => s["CreatedDate"]));
            precacheSearchMappingExpression.ForMember(d => d.LastUpdateDate, o => o.MapFrom(s => s["LastUpdateDate"]));
            precacheSearchMappingExpression.ForMember(d => d.DashBoardSearchType, o => o.MapFrom(s => s["DashboardSearchType"]));
            precacheSearchMappingExpression.ForMember(d => d.SearchId, o => o.MapFrom(s => s["SearchId"]));
            precacheSearchMappingExpression.ForMember(d => d.SearchVersion, o => o.MapFrom(s => s["SearchVersion"]));
            precacheSearchMappingExpression.ForMember(d => d.ApplicationMode, o => o.MapFrom(s => s["ApplicationMode"]));
            precacheSearchMappingExpression.ForMember(d => d.AreaNo, o => o.MapFrom(s => s["AreaNo"]));
            precacheSearchMappingExpression.ForMember(d => d.SiteId, o => o.MapFrom(s => s["SiteId"]));
            precacheSearchMappingExpression.ForMember(d => d.HomePageSearchType, o => o.MapFrom(s => s["HomePageSearchType"]));
            precacheSearchMappingExpression.ForMember(d => d.DynamicPrecacheSearchId, o => o.MapFrom(s => s["DynamicPrecacheSearchId"] == DBNull.Value ? -1 : s["DynamicPrecacheSearchId"]));
            precacheSearchMappingExpression.ForMember(d => d.PrecacheIntegrityKey, o => o.MapFrom(s => s["PrecacheIntegrityKey"]));

            precacheSearchMappingExpression.ForMember(d => d.IsDuplicate, o => o.MapFrom(s => s["IsDuplicate"]));
            precacheSearchMappingExpression.ForMember(d => d.HomepageSearchId, o => o.MapFrom(s => s["HomepageSearchId"]));
        }


        private void DtToLoggedPrecacheSearchItemMapper()
        {
            //https://stackoverflow.com/questions/35414228/using-automapper-to-map-a-datatable-to-an-object-dto
            IMappingExpression<DataRow, LoggedPrecacheSearchItem> LoggedPrecacheSearchItemMappingExpression = CreateMap<DataRow, LoggedPrecacheSearchItem>();
            LoggedPrecacheSearchItemMappingExpression.ForMember(d => d.ID, o => o.MapFrom(s => s["Id"]));
            LoggedPrecacheSearchItemMappingExpression.ForMember(d => d.CreatedDate, o => o.MapFrom(s => s["CreatedDate"]));
            LoggedPrecacheSearchItemMappingExpression.ForMember(d => d.LastUpdateDate, o => o.MapFrom(s => s["LastUpdateDate"] == DBNull.Value ? null : s["LastUpdateDate"]));
            LoggedPrecacheSearchItemMappingExpression.ForMember(d => d.SearchId, o => o.MapFrom(s => s["SearchId"]));
            LoggedPrecacheSearchItemMappingExpression.ForMember(d => d.SearchVersion, o => o.MapFrom(s => s["SearchVersion"]));
            LoggedPrecacheSearchItemMappingExpression.ForMember(d => d.ApplicationMode, o => o.MapFrom(s => s["ApplicationMode"]));
            LoggedPrecacheSearchItemMappingExpression.ForMember(d => d.AreaNo, o => o.MapFrom(s => s["AreaNo"]));
            LoggedPrecacheSearchItemMappingExpression.ForMember(d => d.SiteId, o => o.MapFrom(s => s["SiteId"]));
            LoggedPrecacheSearchItemMappingExpression.ForMember(d => d.HomePageSearchType, o => o.MapFrom(s => s["HomePageSearchType"]));
            LoggedPrecacheSearchItemMappingExpression.ForMember(d => d.HomepageSearchId, o => o.MapFrom(s => s["HomepageSearchId"]));
            LoggedPrecacheSearchItemMappingExpression.ForMember(d => d.ErrorMessage, o => o.MapFrom(s => s["ErrorMessage"]));
            LoggedPrecacheSearchItemMappingExpression.ForMember(d => d.DashBoardSearchTypeEnum, o => o.MapFrom(s => s["DashBoardSearchTypeEnum"]));
            LoggedPrecacheSearchItemMappingExpression.ForMember(d => d.PrecacheIntegrityKey, o => o.MapFrom(s => s["PrecacheIntegrityKey"]));
        }
    }
}
 