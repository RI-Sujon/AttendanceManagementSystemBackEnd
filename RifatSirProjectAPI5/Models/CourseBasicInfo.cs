using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RifatSirProjectAPI5.Models
{
    public class CourseBasicInfo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string CourseId { get; set; }
        public string CourseName { get; set; }
        public string Teacher1Name { get; set; }
        public string Teacher1UserName { get; set; }
        public string Teacher2Name { get; set; }
        public string Teacher2UserName { get; set; }
        public string BatchNo { get; set; }

    }
}
