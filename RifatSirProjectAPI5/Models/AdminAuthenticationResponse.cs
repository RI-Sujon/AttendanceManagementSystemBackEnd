using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RifatSirProjectAPI5.Models
{
    public class AdminAuthenticationResponse
    {
        public AdminBasicInfo adminBasicInfo { get; set; }
        public string token { get; set; }
    }
}
