using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace PrecacheManagerServer.Enums
{
    public enum DashBoardSearchTypeEnum
    {
        None,
        [ViewName("_DashboardChartResults")]
        Chart,
        [ViewName("_DashboardLeagueTableResults")]
        LeagueTable,
        [ViewName("_DashboardFullScheduleResults")]
        ScheduleReport,
        [ViewName("_DashboardPivotTableResults")]
        Spend,
        [ViewName("_DashboardWallResults")]
        Creative,
        [ViewName("_DashboardInvalidDates")]
        InvalidDates
    }



    [AttributeUsage(AttributeTargets.Field)]
    public class ViewName : Attribute
    {
        private string m_name;
        public ViewName(string name)
        {
            this.m_name = name;
        }
        public static string Get(Type tp, string name)
        {
            MemberInfo[] mi = tp.GetMember(name);
            if (mi != null && mi.Length > 0)
            {
                ViewName attr = Attribute.GetCustomAttribute(mi[0],
                    typeof(ViewName)) as ViewName;
                if (attr != null)
                {
                    return attr.m_name;
                }
            }
            return null;
        }
        public static string Get(object enm)
        {
            if (enm != null)
            {
                MemberInfo[] mi = enm.GetType().GetMember(enm.ToString());
                if (mi != null && mi.Length > 0)
                {
                    ViewName attr = Attribute.GetCustomAttribute(mi[0],
                        typeof(ViewName)) as ViewName;
                    if (attr != null)
                    {
                        return attr.m_name;
                    }
                }
            }
            return null;
        }
    }
}
