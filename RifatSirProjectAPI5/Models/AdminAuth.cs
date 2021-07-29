using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RifatSirProjectAPI5.Models
{
    public class AdminAuth
    {
        public string username { get; set; }
        public string password { get; set; }
    }
}
