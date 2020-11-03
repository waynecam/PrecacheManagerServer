using AutoMapper;
using Moq;
using PrecacheManagerServer.API.Infrastructure;
using PrecacheManagerServer.BLL.Services;
using PrecacheManagerServer.DAL.Models;
using PrecacheServerManager.Test.Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace PrecacheServerManager.Test.PrecacheServerManager.BLL.Tests
{
    public class BLLTestbase : SharedTestbase
    {

        protected Mock<IBaseService<PrecacheSearchPlus>> mockPrecacheSearchPlusService;
        protected Mock<IBaseService<LoggedPrecacheSearchItem>> mockLoggedPrecacheSearchService;
        protected Mock<IBaseService<PrecacheSearchItemsCreated>> mockPrecacheSearchesCreatedService;
        protected Mock<IMapper> mockMapper;

        protected void SetupFakeMapper(IMapper mapper)
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            mapper = mappingConfig.CreateMapper();
        }


    }
}
