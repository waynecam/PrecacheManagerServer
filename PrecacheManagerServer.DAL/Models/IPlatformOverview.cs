using System;
using System.Collections.Generic;
using System.Text;
//using PrecacheManagerServer.DAL.Enums;
using PrecacheManagerServer.Shared.Enums;

namespace PrecacheManagerServer.DAL.Models
{
    public interface IPlatformOverview
    {

        //ApplicationMode ApplicationMode { get; set; }

        //string PlatformDescription { get; }

        //List<PrecacheSite> PrecacheSites { get; set; }


        ApplicationMode ApplicationMode { get; set; }

       string PlatformDescription { get; }

        //public List<PrecacheSite> PrecacheSites { get; set; }

        //precachesite properties
       int SiteId { get; set; }

        string Name { get; set; }

        //precachesearch properties

   
        int Id { get; set; }
        DateTime CreatedDate { get; set; }
        DateTime LastUpdateDate { get; set; }

      bool IsDeleted { get; set; }

        int DashboardSearchType { get; set; }

        int SearchId { get; set; }


        int SearchVersion { get; set; }

         int HomepageSearchId { get; set; }

        string PrecacheKey { get; set; }
        int AreaNo { get; set; }

        int HomePageSearchType { get; set; }

        int DynamicPrecacheSearchId { get; set; }
        string AreaSearchName { get; set; }

        Guid PrecacheIntegrityKey { get; set; }
    }
}
