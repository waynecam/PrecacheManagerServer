﻿using System;
using System.Collections.Generic;
using System.Text;
using PrecacheManagerServer.DAL.Models;
using AutoMapper;
using System.Data;

namespace PrecacheManagerServer.DAL.Mappers
{
    /// <summary>
    /// Mapping tool to map entoty types to a specific dbtable for generating SQL queries
    /// </summary>
    public static class DBMapper
    {
        public static Dictionary<Type, string> Mapper = new Dictionary<Type, string>()
        {
            { typeof(IPrecacheSearch), "PrecacheSearchItem"}
        };

    }

   
}
