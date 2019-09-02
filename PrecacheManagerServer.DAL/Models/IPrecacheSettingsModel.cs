using System;
using System.Collections.Generic;
using System.Text;
using PrecacheManagerServer.Enums;


namespace PrecacheManagerServer.DAL.Models
{
    public interface IPrecacheSettingsModel
    {
        string PrecacheKey { get; }
        DateTime CreatedDate { get; set; }
        DateTime LastUpdateDate { get; set; }
        bool IsDeleted { get; set; }
        int AreaNo { get; set; }
        DashBoardSearchTypeEnum DashBoardSearchType { get; set; }
        int SearchId { get; set; }
        int SearchVersion { get; set; }

        int HomepageSearchId { get; set; }

        ApplicationMode ApplicationMode { get; set; }

        int SiteId { get; set; }

        HomePageSearchType HomePageSearchType { get; set; }

        int? DynamicPrecacheSearchId { get; set; }

        string AreaSearchName { get; set; }

        Guid PrecacheIntegrityKey { get; set; }
    }
}
