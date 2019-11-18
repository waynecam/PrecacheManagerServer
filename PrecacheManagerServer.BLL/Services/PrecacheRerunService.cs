using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using PrecacheManagerServer.BLL.Models;
using PrecacheManagerServer.DAL.Models;

namespace PrecacheManagerServer.BLL.Services
{
    public class PrecacheRerunService
    {
        private readonly IBaseService<PrecacheRerun> _service;
        private readonly IMapper _mapper;

        public PrecacheRerunService(IBaseService<PrecacheRerun> service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        public void AddOrUpdate<T>(PlatformSettingRequestsModelAddOrUpdate<T> request)
        {
            
        }

    }
}
