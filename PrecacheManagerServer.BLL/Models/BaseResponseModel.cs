using System;
using System.ComponentModel.DataAnnotations;

namespace PrecacheManagerServer.BLL.Models
{
    public class BaseResponseModel
    {
     [Required] 
        public int Id { get; set; }
    }
}
