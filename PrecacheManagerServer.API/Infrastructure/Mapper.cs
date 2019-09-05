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


            //CreateMap<Course, CourseResponseModel>();
            //CreateMap<CourseResponseModel, Course>();

            //CreateMap<UserModel, AppUser>();
            //CreateMap<AppUser, UserModel>();

            //CreateMap<UserManager<UserModel>, UserManager<AppUser>>();
            //CreateMap<UserManager<AppUser>, UserManager<UserModel>>();
        }
    }
}
