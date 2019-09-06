using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace PrecacheManagerServer.DAL.Mappers
{
    public interface IDataMapper
    {
        List<T> DtToObjectMapper<T>(DataTable dt);
    }
}
