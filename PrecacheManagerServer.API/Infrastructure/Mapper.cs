using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using PrecacheManagerServer.DAL.Models;
using PrecacheManagerServer.BLL.Models;
using PrecacheManagerServer.API.Models;
using System.Data;

namespace PrecacheManagerServer.API.Infrastructure
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Add as many of these lines as you need to map your objects
            CreateMap<PrecacheSearch, PrecacheSearchResponseModel>();
            CreateMap<PrecacheSearchResponseModel,PrecacheSearch>();

            //API > BLL
            CreateMap<PlatformSettings, PlatformSettingsRequestModel>();
            //BLL > DAL
            CreateMap<PlatformSettingsRequestModel, PlatformSettingsModel>();
            //CreateMap<IDataReader, PrecacheSearch>();
            //CreateMap<PrecacheSearch, IDataReader>();

            https://stackoverflow.com/questions/35414228/using-automapper-to-map-a-datatable-to-an-object-dto
            IMappingExpression<DataRow, PrecacheSearch> mappingExpression;
            mappingExpression = CreateMap<DataRow, PrecacheSearch>();
            mappingExpression.ForMember(d => d.Id, o => o.MapFrom(s => s["Id"]));
            mappingExpression.ForMember(d => d.CreatedDate, o => o.MapFrom(s => s["CreatedDate"]));
            mappingExpression.ForMember(d => d.LastUpdateDate, o => o.MapFrom(s => s["LastUpdateDate"]));
            mappingExpression.ForMember(d => d.IsDeleted, o => o.MapFrom(s => s["IsDeleted"]));
            mappingExpression.ForMember(d => d.DashboardSearchType, o => o.MapFrom(s => s["DashboardSearchType"]));
            mappingExpression.ForMember(d => d.SearchId, o => o.MapFrom(s => s["SearchId"]));
            mappingExpression.ForMember(d => d.SearchVersion, o => o.MapFrom(s => s["SearchVersion"]));
            mappingExpression.ForMember(d => d.ApplicationMode, o => o.MapFrom(s => s["ApplicationMode"]));
            mappingExpression.ForMember(d => d.PrecacheKey, o => o.MapFrom(s => s["PrecacheKey"]));
            mappingExpression.ForMember(d => d.AreaNo, o => o.MapFrom(s => s["AreaNo"]));
            mappingExpression.ForMember(d => d.SiteId, o => o.MapFrom(s => s["SiteId"]));
            mappingExpression.ForMember(d => d.HomePageSearchType, o => o.MapFrom(s => s["HomePageSearchType"]));
            mappingExpression.ForMember(d => d.DynamicPrecacheSearchId, o => o.MapFrom(s => s["DynamicPrecacheSearchId"]));
            mappingExpression.ForMember(d => d.AreaSearchName, o => o.MapFrom(s => s["AreaSearchName"]));
            mappingExpression.ForMember(d => d.PrecacheIntegrityKey, o => o.MapFrom(s => s["PrecacheIntegrityKey"]));



          
            CreateMap<PlatformOverview, PlatformOverviewResponseModel>();
            CreateMap<PlatformOverviewResponseModel, PlatformOverview>();



            //CreateMap<Course, CourseResponseModel>();
            //CreateMap<CourseResponseModel, Course>();

            //CreateMap<UserModel, AppUser>();
            //CreateMap<AppUser, UserModel>();

            //CreateMap<UserManager<UserModel>, UserManager<AppUser>>();
            //CreateMap<UserManager<AppUser>, UserManager<UserModel>>();
        }
    }
}
