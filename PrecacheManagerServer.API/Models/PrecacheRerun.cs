using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrecacheManagerServer.API.Models
{
        public class PrecacheRerun
        {

            public int SiteId { get; set; }
            public int HomePageSearchId { get; set; }

            public int HomepageSearchType { get; set; }

            public int SearchId { get; set; }

            public int Applicationmode { get; set; }

            public Guid PrecacheIntegrityKey { get; set; }
    }
}
