using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using PrecacheManagerServer.BLL.Models;
using System.Linq;
using PrecacheManagerServer.DAL.Models;

namespace PrecacheManagerServer.BLL.Services
{
    public class PrecacheSearchService : IPrecacheSearchService
    {

        private readonly IBaseService<PrecacheSearch> _service;
        private readonly IMapper _mapper;


        public PrecacheSearchService(IBaseService<PrecacheSearch> service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PrecacheSearchResponseModel>> GetAsync()
        {
            var result = await _service.GetAsync();
            
            return result.Select(t => _mapper.Map<PrecacheSearch, PrecacheSearchResponseModel>(t));
        }

        public async Task<PrecacheSearchResponseModel> GetById(int id)
        {
            return _mapper.Map<PrecacheSearch, PrecacheSearchResponseModel>(await _service.GetById(id));
        }

        public IEnumerable<PrecacheSearchResponseModel> Where(string sql)
        {
            var whereResult = _service.Where(sql).ToList();
            return _mapper.Map<List<PrecacheSearch>, List<PrecacheSearchResponseModel>>(whereResult).AsEnumerable();
        }
    }
}
