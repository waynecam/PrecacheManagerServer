using System;
using System.Collections.Generic;
using System.Text;

namespace PrecacheManagerServer.Shared.Enums.Extensions
{
    public class ApplicationModeIdAttribute : Attribute
    {

        internal ApplicationModeIdAttribute(int applicationModeId)
        {
            this.ApplicationModeId = applicationModeId;
        }
        public int ApplicationModeId { get; private set; }

    }
}
