using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PrecacheManagerServer.API.Models
{
        public class PrecacheRerun
        {
            
            [Required]
            [Range(0, int.MaxValue)]
            public int SiteId { get; set; }
            public int HomePageSearchId { get; set; }

            public int HomepageSearchType { get; set; }

            public int SearchId { get; set; }

            [Required]
            [Range(0, int.MaxValue)]
            public int Applicationmode { get; set; }

            [Required]
            public Guid PrecacheIntegrityKey { get; set; }
    }
}
