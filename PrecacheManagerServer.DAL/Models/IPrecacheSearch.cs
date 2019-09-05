using System;

namespace PrecacheManagerServer.DAL.Models
{
    public interface IPrecacheSearch :IBaseEntity
    {
        DateTime CreatedDate { get; set; }
        DateTime LastUpdateDate { get; set; }

         bool IsDeleted { get; set; }

       int DashboardSearchType { get; set; }

        int SearchId { get; set; }


        int SearchVersion { get; set; }

         int HomepageSearchId { get; set; }

         int ApplicationMode { get; set; }

         string PrecacheKey { get; set; }
        int AreaNo { get; set; }

        int SiteId { get; set; }

        int HomePageSearchType { get; set; }

       int DynamicPrecacheSearchId { get; set; }
        string AreaSearchName { get; set; }

         Guid PrecacheIntegrityKey { get; set; }
    }
}