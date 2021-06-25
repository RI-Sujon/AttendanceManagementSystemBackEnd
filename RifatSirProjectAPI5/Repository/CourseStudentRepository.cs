using RifatSirProjectAPI5.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RifatSirProjectAPI5.Repository
{
    public class CourseStudentRepository: DatabaseRepository
    {
        public CourseStudent Add(CourseStudent courseStudent)
        {
            databaseContext.CourseStudentRelationTable.Add(courseStudent);
            databaseContext.SaveChanges();
            return courseStudent;
        }

        /*public List<CourseStudent> GetAll()
        {
            return databaseContext.CourseStudentRelationTable.ToList();
        }*/

        public CourseStudent GetByCourseIdAndBatchNoAndRoll(string CourseId, string BatchNo, int StudentRoll)
        {
            return databaseContext.CourseStudentRelationTable.SingleOrDefault(CourseStudent => CourseStudent.CourseId == CourseId && CourseStudent.BatchNo == BatchNo && CourseStudent.StudentBSSERoll == StudentRoll);
        }

        public object GetByCourseIdAndBatchNo(string CourseId, string BatchNo)
        {
            return databaseContext.CourseStudentRelationTable.Where(CourseStudent => CourseStudent.CourseId == CourseId && CourseStudent.BatchNo == BatchNo).OrderBy(CourseStudent => CourseStudent.StudentBSSERoll).ToList();
        }

        public List<CourseStudent> GetByBSSEROLL(int BSSEROLL)
        {
            return databaseContext.CourseStudentRelationTable.Where(CourseStudent => CourseStudent.StudentBSSERoll == BSSEROLL).ToList();
        }

        public bool DeleteSingleStudentFromCourse(string CourseId, string BatchNo, int StudentRoll)
        {
            databaseContext.CourseStudentRelationTable.Remove(GetByCourseIdAndBatchNoAndRoll(CourseId, BatchNo, StudentRoll));
            databaseContext.SaveChanges();
            return true;
        }

        public bool DeleteAllStudentFromCourse(string CourseId, string BatchNo)
        {
            var results = databaseContext.CourseStudentRelationTable.Where(CourseStudent => CourseStudent.CourseId == CourseId && CourseStudent.BatchNo == BatchNo).ToList();
            foreach (var s in results)
            {
                databaseContext.CourseStudentRelationTable.Remove(s);
            }
            databaseContext.SaveChanges();
            return true;
        }
    }
}
