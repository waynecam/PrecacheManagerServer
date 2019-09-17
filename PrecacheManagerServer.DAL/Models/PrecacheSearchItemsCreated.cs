using System;
using System.Collections.Generic;
using System.Text;

namespace PrecacheManagerServer.DAL.Models
{
    public class PrecacheSearchItemsCreated : BaseEntity
    {
        public int ID { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? LastUpdateDate { get; set; }

        public int DashBoardSearchType { get; set; }

        public int SearchId { get; set; }

        public int SearchVersion { get; set; }

        public int HomepageSearchId { get; set; }

        public int ApplicationMode { get; set; }

        public int AreaNo { get; set; }

        public string AreaSearchName { get; set; }

        public int SiteId { get; set; }

        public int HomePageSearchType { get; set; }

        public int? DynamicPrecacheSearchId { get; set; }

        public Guid PrecacheIntegrityKey { get; set; }

        public bool? IsDuplicate { get; set; }

    }
}
