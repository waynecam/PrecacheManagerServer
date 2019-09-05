using System;
using System.Collections.Generic;
using System.Text;
using PrecacheManagerServer.BLL.Models;

namespace PrecacheManagerServer.BLL.Models
{
    public class PrecacheSearchResponseModel
    {
        public int Id { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime LastUpdateDate { get; set; }

        public bool IsDeleted { get; set; }

        public int DashboardSearchType { get; set; }

        public int SearchId { get; set; }


        public int SearchVersion { get; set; }

        public int HomepageSearchId { get; set; }

        public int ApplicationMode { get; set; }

        public string PrecacheKey { get; set; }
        public int AreaNo { get; set; }

        public int SiteId { get; set; }

        public int HomePageSearchType { get; set; }

        public int DynamicPrecacheSearchId { get; set; }
        public string AreaSearchName { get; set; }

        public Guid PrecacheIntegrityKey { get; set; }
    }
}
