using System;
using System.Collections.Generic;
using System.Text;

namespace PrecacheManagerServer.DAL.Models
{
    public  class PrecacheRerun :BaseEntity
    {

        public int SiteId { get; set; }
        public int HomePageSearchId { get; set; }

        public int HomepageSearchType { get; set; }

        public int SearchId { get; set; }
    }
}
