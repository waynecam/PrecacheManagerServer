using System;
using System.Collections.Generic;
using System.Text;

namespace PrecacheManagerServer.Shared.Enums
{
    public enum PrecacheDbTable
    {
        [TableName("N/A")]
        [SchemaName("N/A")]
        NoTable,

        [TableName("PrecacheSearchItem")]
        [SchemaName("dbo")]
        PrecacheSearchItem,
        [TableName("PrecacheSearchItemsCreated")]
        [SchemaName("dbo")]
        PrecacheSearchItemsCreated,
        [TableName("DynamicPrecacheSearches")]
        [SchemaName("dbo")]
        DynamicPrecacheSearches,
        //[TableName("LoggedPrecacheSearchItem")]
        //[SchemaName("dbo")]
        //LoggedPrecacheSearchItem,
        [TableName("Precacher_LoggedPrecacheSearchItem")]
        [SchemaName("dbo")]
        LoggedPrecacheSearchItem,
        [TableName("DashboardPrecache")]
        [SchemaName("dbo")]
        DashboardPrecache,
        [TableName("UserCategoryFocus")]
        [SchemaName("dbo")]
        UserCategoryFocus,
        [TableName("Clientsite")]
        [SchemaName("dbo")]
        Clientsite

    }


    public class TableNameAttribute : Attribute
    {
        public string TableName;
        public TableNameAttribute(string tableName)
        {
            TableName = tableName;
        }
    }

    public class SchemaNameAttribute : Attribute
    {
        public string SchemaName;
        public SchemaNameAttribute(string schemaName)
        {
            SchemaName = schemaName;
        }
    }

    public static class PrecacheEnumExtensions
    {
        public static string GetTableName(this Enum value)
        {
            var type = value.GetType();

            string name = Enum.GetName(type, value);
            if (name == null) { return null; }

            var field = type.GetField(name);
            if (field == null) { return null; }

            var attr = Attribute.GetCustomAttribute(field, typeof(TableNameAttribute)) as TableNameAttribute;
            if (attr == null) { return null; }

            return attr.TableName;
        }


        public static string GetSchemaName(this Enum value)
        {
            var type = value.GetType();

            string name = Enum.GetName(type, value);
            if (name == null) { return null; }

            var field = type.GetField(name);
            if (field == null) { return null; }

            var attr = Attribute.GetCustomAttribute(field, typeof(SchemaNameAttribute)) as SchemaNameAttribute;
            if (attr == null) { return null; }

            return attr.SchemaName;
        }
    }
}
