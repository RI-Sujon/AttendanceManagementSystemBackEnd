﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RifatSirProjectAPI5.Models
{
    public class StudentAuth
    {
        public int BSSEROLL { get; set; }
        public string Email { get; set; }
        public string password { get; set; }
    }
}
