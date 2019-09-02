﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using PrecacheManagerServer.DAL.Models;
using PrecacheManagerServer.BLL.Models;

namespace PrecacheManagerServer.API.Infrastructure
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Add as many of these lines as you need to map your objects
            CreateMap<PrecacheSearch, PrecacheSearchResponseModel>();
            CreateMap<PrecacheSearchResponseModel,PrecacheSearch>();

            //CreateMap<Course, CourseResponseModel>();
            //CreateMap<CourseResponseModel, Course>();

            //CreateMap<UserModel, AppUser>();
            //CreateMap<AppUser, UserModel>();

            //CreateMap<UserManager<UserModel>, UserManager<AppUser>>();
            //CreateMap<UserManager<AppUser>, UserManager<UserModel>>();
        }
    }
}