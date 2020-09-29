//using PrecacheManagerServer.DAL.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using PrecacheManagerServer.Shared.Enums;

namespace PrecacheManagerServer.DAL.Models
{
   public class User :BaseEntity
    {
        public string UserName { get; set; }
        public string Password { get; set; }

        public IEnumerable<ApplicationMode> ApplicationModes { get; set; }

    }
}
