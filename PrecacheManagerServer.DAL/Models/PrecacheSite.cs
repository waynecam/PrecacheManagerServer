using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PrecacheManagerServer.DAL.Enums;
using PrecacheManagerServer.Enums;

namespace PrecacheManagerServer.DAL.Models
{
    public class PrecacheSite : IPrecacheSite
    {
        public int SiteId { get; set; }

        public string Name { get; set; }

        public List<PrecacheSearch> PrecacheSearches { get; set; }

       public ApplicationMode ApplicationMode { get; set; }
    }
}
