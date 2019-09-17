using System;
using System.Collections.Generic;
using System.Text;

namespace PrecacheManagerServer.DAL.Models
{
    public interface IPrecacheSearchItemsCreated
    {
         int ID { get; set; }

         DateTime? CreatedDate { get; set; }

         DateTime? LastUpdateDate { get; set; }

         int DashBoardSearchType { get; set; }

         int SearchId { get; set; }

         int SearchVersion { get; set; }

         int HomepageSearchId { get; set; }

         int ApplicationMode { get; set; }

         int AreaNo { get; set; }

         string AreaSearchName { get; set; }

         int SiteId { get; set; }

         int HomePageSearchType { get; set; }

         int? DynamicPrecacheSearchId { get; set; }

         Guid PrecacheIntegrityKey { get; set; }

         bool? IsDuplicate { get; set; }
    }
}
