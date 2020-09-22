using PrecacheManagerServer.BLL.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace PrecacheManagerServer.BLL.Models
{
    public class UserResponseModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }

        public IEnumerable<ApplicationMode> ApplicationModes { get; set; }
    }
}
