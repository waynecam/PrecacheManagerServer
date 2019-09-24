using System;
using System.Collections.Generic;
using System.Text;

namespace PrecacheManagerServer.DAL.Models
{
    interface ILoggedPrecacheSearchItem
    {

         int ID { get; set; }

         int HomePageSearchType { get; set; }

         int HomepageSearchId { get; set; }

         int SiteId { get; set; }

         DateTime CreatedDate { get; set; }

         DateTime? LastUpdateDate { get; set; }

         int DashBoardSearchTypeEnum { get; set; }

         int SearchId { get; set; }

         int SearchVersion { get; set; }

         int ApplicationMode { get; set; }

         int AreaNo { get; set; }

        string ErrorMessage { get; set; }
    }
}
