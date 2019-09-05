using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace PrecacheManagerServer.DAL.Contexts
{

    /// <summary>
    /// This class wil return a dbcontext - containing details of a dynamically  built sqlconnection
    /// </summary>
    public class DBContext
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
            //return mapper.Map<IDataReader, List<T>>(d.CreateDataReader());


            var rows = new List<DataRow>(dt.Rows.OfType<DataRow>());

            List<T> result;

            result = mapper.Map<List<DataRow>, List<T>>(rows);

            return result;
        }
    }
}
