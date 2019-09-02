//using System;
//using System.Collections.Generic;
//using System.Text;
//using PrecacheManagerServer.Enums;
//using PrecacheManagerServer.DAL.Models;

//namespace PrecacheManagerServer.DAL.Models
//{

//    public class PrecacheSettingsModel : IPrecacheSettingsModel
//    {
//        public PrecacheSettingsModel(string precacheKey, int areaNo, int searchId, int searchVersion, int homepageSearchId, DashBoardSearchTypeEnum dashBoardSearchType,
//           ApplicationMode applicationMode, int siteId, HomePageSearchType homePageSearchType, Guid precacheIntegrityKey, string areaSearchName = null, int? dynamicPrecacheSearchId = null)
//        {
//            PrecacheKey = precacheKey;
//            AreaNo = areaNo;
//            SearchId = searchId;
//            SearchVersion = searchVersion;
//            HomepageSearchId = homepageSearchId;
//            DashBoardSearchType = dashBoardSearchType;
//            ApplicationMode = applicationMode;
//            SiteId = siteId;
//            HomePageSearchType = homePageSearchType;
//            DynamicPrecacheSearchId = dynamicPrecacheSearchId;
//            AreaSearchName = areaSearchName;
//            PrecacheIntegrityKey = precacheIntegrityKey;


//        }

//        public string PrecacheKey { get; private set; }
//        public DateTime CreatedDate { get; set; }
//        public DateTime LastUpdateDate { get; set; }
//        public bool IsDeleted { get; set; }
//        public int AreaNo { get; set; }
//        public DashBoardSearchTypeEnum DashBoardSearchType { get; set; }
//        public int SearchId { get; set; }
//        public int SearchVersion { get; set; }
//        public int HomepageSearchId { get; set; }

//        public ApplicationMode ApplicationMode { get; set; }

//        public HomePageSearchType HomePageSearchType { get; set; }
//        public int? DynamicPrecacheSearchId { get; set; }
//        public int SiteId { get; set; }

//        public string AreaSearchName { get; set; }

//        public Guid PrecacheIntegrityKey { get; set; }
//    }
//}
