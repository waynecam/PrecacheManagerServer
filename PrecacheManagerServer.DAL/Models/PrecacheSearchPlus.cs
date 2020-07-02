using System;
using System.Collections.Generic;
using System.Text;
using PrecacheManagerServer.DAL.Enums;

namespace PrecacheManagerServer.DAL.Models
{
    /// <summary>
    /// Precachesserach details plus additional applicationmode and sitelevel meta detail
    /// </summary>
    public class PrecacheSearchPlus :BaseEntity
    {
            public ApplicationMode ApplicationMode { get; set; }

            public string PlatformDescription { get { return ApplicationMode.ToString(); } }

            //public List<PrecacheSite> PrecacheSites { get; set; }

            //precachesite properties
            public int SiteId { get; set; }

            public string Name { get; set; }

            //precachesearch properties
            public int Id { get; set; }
            public DateTime CreatedDate { get; set; }
            public DateTime LastUpdateDate { get; set; }

            public bool IsDeleted { get; set; }

            public int DashboardSearchType { get; set; }

            public int SearchId { get; set; }


            public int SearchVersion { get; set; }

            public int HomepageSearchId { get; set; }

            public string PrecacheKey { get; set; }
            public int AreaNo { get; set; }

            public int HomePageSearchType { get; set; }

            public int DynamicPrecacheSearchId { get; set; }
            public string AreaSearchName { get; set; }

            public Guid PrecacheIntegrityKey { get; set; }









    }
}
