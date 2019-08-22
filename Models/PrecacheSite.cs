using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PrecacheManagerServer.Enums;

namespace PrecacheManagerServer.Models
{
    public class PrecacheSite
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public List<PrecacheSearch> PrecacheSearches { get; set; }

       public ApplicationMode ApplicationMode { get; set; }
    }
}
