using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RifatSirProjectAPI5.Models
{
    public class CoursePosts
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DateTime DateAndTime { get; set; }
        public string CourseId { get; set; }
        public string BatchNo { get; set; }
        public string PostGiverName { get; set; }
        public string TeacherUserName { get; set; }
        public int StudentRollNo { get; set; }
        public string PostType { get; set; }
        public string Post { get; set; }
    }
}
