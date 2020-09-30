using System;
using System.Collections.Generic;
using System.Text;

namespace PrecacheManagerServer.Shared.Enums.Extensions
{
    public class ApplicationModeFriendlyDescriptionAttribute : Attribute
    {
        internal ApplicationModeFriendlyDescriptionAttribute(string applicationModeFriendlyDescription)
        {
            this.ApplicationModeFriendlyDescription = applicationModeFriendlyDescription;
        }
        public string ApplicationModeFriendlyDescription { get; private set; }
    }
}
