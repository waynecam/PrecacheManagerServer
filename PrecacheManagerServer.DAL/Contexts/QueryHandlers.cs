using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace PrecacheManagerServer.DAL.Contexts
{
    public static class QueryHandlers
    {
        public static async Task<List<T>> ExecuteQueryGetResult<T>(string sql, SqlConnection conn, IMapper mapper)
        {

            return await Task.Run(() =>
             {
                 var dt = new DataTable();
                 var result = new List<T>();


                 conn.Open();

                 using (var command = new SqlCommand(sql, conn))
                 {
                     var adp = new SqlDataAdapter(command);

                     adp.Fill(dt);

                 }
                 //return dt;

                 result = DtToObjectMapper<T>(dt, mapper);

                 return result;
             });

           
        }



        public static List<T> DtToObjectMapper<T>(DataTable dt, IMapper mapper)
        {
            //Mapper.CreateMap<IDataReader, T>();
            return mapper.Map<IDataReader, List<T>>(dt.CreateDataReader());
        }
    }
}
