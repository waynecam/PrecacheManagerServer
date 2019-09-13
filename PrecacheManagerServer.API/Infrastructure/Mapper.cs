using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using PrecacheManagerServer.DAL.Models;
using PrecacheManagerServer.BLL.Models;
using PrecacheManagerServer.API.Models;
using System.Data;
using PrecacheManagerServer.DAL.Enums;

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
            CreateMap<PlatformSettingsRequestModel, PlatformSettingsModel>();
            //CreateMap<IDataReader, PrecacheSearch>();
            //CreateMap<PrecacheSearch, IDataReader>();

            DtToPrecacheSearchMapper();
            DtToPlatformOverviewMapper();



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
            IMappingExpression<DataRow, PlatformOverview> platformOverviewMappingExpression = CreateMap<DataRow, PlatformOverview>();
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
    }
}
