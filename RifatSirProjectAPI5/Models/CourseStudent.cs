using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RifatSirProjectAPI5.Models
{
    public class CourseStudent
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public String CourseId { get; set; }
        public String BatchNo { get; set; }
        public int StudentBSSERoll { get; set; }
        public String StudentName { get; set; }

    }
}
