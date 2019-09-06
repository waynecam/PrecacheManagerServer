using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using AutoMapper;

namespace PrecacheManagerServer.DAL.Mappers
{
    public class DataMapper : IDataMapper
    {


        private readonly IMapper _mapper;
        public DataMapper(IMapper mapper)
        {
            _mapper = mapper;
        }

        /// <summary>
        /// Map Datable to an POCO
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dt"></param>
        /// <param name="mapper"></param>
        /// <returns></returns>
        //public static List<T> DtToObjectMapper<T>(DataTable dt, IMapper mapper)
        //{
        //    //return mapper.Map<IDataReader, List<T>>(d.CreateDataReader());


        //    var rows = new List<DataRow>(dt.Rows.OfType<DataRow>());

        //    List<T> result;

        //    result = mapper.Map<List<DataRow>, List<T>>(rows);

        //    return result;
        //}

        /// <summary>
        /// Map Datable result to an POCO
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dt"></param>
        /// <param name="mapper"></param>
        /// <returns></returns>
        public List<T> DtToObjectMapper<T>(DataTable dt)
        {
            //return mapper.Map<IDataReader, List<T>>(d.CreateDataReader());


            var rows = new List<DataRow>(dt.Rows.OfType<DataRow>());

            List<T> result;

            result = _mapper.Map<List<DataRow>, List<T>>(rows);

            return result;
        }


    }
}
