﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PrecacheManagerServer.Shared.Enums.Extensions
{
    public static class EnumExtensions
    {
        //public static TAttribute GetAttribute<TAttribute>(this Enum value)
        //    where TAttribute : Attribute
        //{
        //    var type = value.GetType();
        //    var name = Enum.GetName(type, value);
        //    return type.GetField(name) // I prefer to get attributes this way
        //        .GetCustomAttributes(false)
        //        .OfType<TAttribute>()
        //        .SingleOrDefault();
        //}


        public static TAttribute GetAttribute<TAttribute>(this Enum value)
         where TAttribute : Attribute
        {
            var enumType = value.GetType();
            var name = Enum.GetName(enumType, value);
            return enumType.GetField(name).GetCustomAttributes(false).OfType<TAttribute>().SingleOrDefault();
        }
    }
}
