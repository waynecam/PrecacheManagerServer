using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace PrecacheManagerServer.DAL.Contexts
{
   public interface IDBContext
    {
        Task<List<T>> ExecuteQueryGetResult<T>(string sql, SqlConnection conn);

        Task AddOrUpdate(string sql, List<SqlParameter> parameters, SqlConnection conn);
    }
}
