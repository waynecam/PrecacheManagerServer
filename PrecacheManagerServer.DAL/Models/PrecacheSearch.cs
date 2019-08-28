using System;
using System.Collections.Generic;
using System.Text;

namespace PrecacheManagerServer.DAL.Models
{
   public class PrecacheSearch : IPrecacheSearch
    {
        private int _id;
        public int Id { get => _id; set => _id = value; }
    }
}
