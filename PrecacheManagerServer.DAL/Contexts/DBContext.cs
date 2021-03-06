﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using PrecacheManagerServer.DAL.Mappers;

namespace PrecacheManagerServer.DAL.Contexts
{

    /// <summary>
    /// This class wil return a dbcontext - containing details of a dynamically  built sqlconnection
    /// </summary>
    public class DBContext : IDBContext
    {

        private readonly IDataMapper _dataMapper;
        public DBContext(IDataMapper dataMapper)
        {
            _dataMapper = dataMapper;
        }
        //public static async Task<List<T>> ExecuteQueryGetResult<T>(string sql, SqlConnection conn, IMapper mapper)
        //{

        //    return await Task.Run(() =>
        //    {
        //        var dt = new DataTable();
        //        var result = new List<T>();


        //        conn.Open();

        //        using (var command = new SqlCommand(sql, conn))
        //        {
        //            var adp = new SqlDataAdapter(command);

        //            adp.Fill(dt);

        //        }
        //        //return dt;

        //        //result = DtToObjectMapper<T>(dt, mapper);
        //        result = _dataMapper.DtToObjectMapper<T>(dt);

        //        return result;
        //    });


        //}

        public async Task<List<T>> ExecuteQueryGetResult<T>(string sql, SqlConnection conn)
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

                //result = DtToObjectMapper<T>(dt, mapper);
                result = _dataMapper.DtToObjectMapper<T>(dt);

                return result;
            });


        }

        public async Task AddOrUpdate(string sql, List<SqlParameter> parameters, SqlConnection conn)
        {
            await Task.Run(() =>
            {
                    using (var command = new SqlCommand(sql, conn))
                    {
                        command.Parameters.AddRange(parameters.ToArray());
                        command.ExecuteNonQuery();
                    }

                    return true;
               
            });
        }
      public async Task<bool> AddOrUpdateSP(string sql, List<SqlParameter> parameters, SqlConnection conn)
            {


               return await Task.Run(() =>
                {
                    try
                    {
                        using (var command = new SqlCommand(sql, conn))
                        {
                            conn.Open();
                            command.CommandType = CommandType.StoredProcedure;
                            command.CommandText = sql;
                            command.Parameters.AddRange(parameters.ToArray());
                            var result = command.ExecuteNonQuery();

                        }
                        return true;
                    }catch(Exception ex)
                    {
                        return false;
                    }
                });
            }


        //public static List<T> DtToObjectMapper<T>(DataTable dt, IMapper mapper)
        //{
        //    //return mapper.Map<IDataReader, List<T>>(d.CreateDataReader());


        //    var rows = new List<DataRow>(dt.Rows.OfType<DataRow>());

        //    List<T> result;

        //    result = mapper.Map<List<DataRow>, List<T>>(rows);

        //    return result;
        //}
    }
}
