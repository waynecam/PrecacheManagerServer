using System;
using System.Collections.Generic;
using System.Text;
using PrecacheManagerServer.DAL.Enums;

namespace PrecacheManagerServer.DAL.Models
{
    public interface IPlatformOverview
    {

        ApplicationMode ApplicationMode { get; set; }

        string PlatformDescription { get; }

        List<PrecacheSite> PrecacheSites { get; set; }
    }
}
