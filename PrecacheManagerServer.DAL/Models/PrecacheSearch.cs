using System;
using System.Collections.Generic;
using System.Text;

namespace PrecacheManagerServer.DAL.Models
{
   public class PrecacheSearch : BaseEntity
    {
        private int _id;
        public int Id { get => _id; set => _id = value; }
    }
}
