using System;
using System.Collections.Generic;
using System.Text;
using PrecacheManagerServer.DAL.Enums;

namespace PrecacheManagerServer.DAL.Models
{
    public interface IPrecacheSite
    {
        int SiteId { get; set; }

        string Name { get; set; }

        List<PrecacheSearch> PrecacheSearches { get; set; }

        ApplicationMode ApplicationMode { get; set; }
    }
}
