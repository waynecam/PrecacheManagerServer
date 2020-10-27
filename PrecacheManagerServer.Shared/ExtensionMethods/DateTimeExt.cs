using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace PrecacheManagerServer.Shared.ExtensionMethods
{

    
    public static class DateTimeExt
    {
        public static DateTime FormattedDateAndTime(this DateTime dateTime)
        {
            var s = String.Format("{0:d/M/yyyy HH:mm:ss}", dateTime);

            return DateTime.ParseExact(s, "d/M/yyyy hh:mm:ss", CultureInfo.InvariantCulture);


        }

    }
}
