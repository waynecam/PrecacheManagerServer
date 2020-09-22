using PrecacheManagerServer.BLL.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace PrecacheManagerServer.BLL.Models
{
    public class IMembershipRequestModel
    {
         Dictionary<ApplicationMode, string> Connections { get; set; }

    }
}
