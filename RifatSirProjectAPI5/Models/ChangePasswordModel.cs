using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RifatSirProjectAPI5.Models
{
    public class ChangePasswordModel
    {
        public string usernameOrRoll { get; set; }
        public string oldPassword { get; set; }
        public string newPassword { get; set; }

    }
}
