using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PrecacheManagerServer.Enums;

namespace PrecacheManagerServer.Models
{
    public class PlatformOverview
    {

        public ApplicationMode ApplicationMode { get; set; }

        public List<Site> Sites { get; set; }



    }
}
