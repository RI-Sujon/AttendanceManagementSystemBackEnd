using RifatSirProjectAPI5.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RifatSirProjectAPI5.Repository
{
    public class CoursePostsRepository: DatabaseRepository
    {
        public CoursePosts Add(CoursePosts coursePosts)
        {
            databaseContext.CoursePostsTable.Add(coursePosts);
            databaseContext.SaveChanges();
            return coursePosts;
        }

        public List<CoursePosts> GetByCourseIdAndBatchNo(string CourseId, string BatchNo)
        {
            return databaseContext.CoursePostsTable.Where(CoursePosts => CoursePosts.CourseId == CourseId && CoursePosts.BatchNo == BatchNo).OrderByDescending(CoursePosts => CoursePosts.DateAndTime).ToList();
        }

        public CoursePosts GetById(int Id)
        {
            return databaseContext.CoursePostsTable.SingleOrDefault(CoursePosts => CoursePosts.Id == Id);
        }

        public CoursePosts GetByCourseIdAndBatchNoAndDateTime(string CourseId, string BatchNo, DateTime DateAndTime)
        {
            return databaseContext.CoursePostsTable.SingleOrDefault(CoursePosts => CoursePosts.CourseId == CourseId && CoursePosts.BatchNo == BatchNo && CoursePosts.DateAndTime== DateAndTime);
        }

        public CoursePosts Update(CoursePosts coursePosts)
        {
            databaseContext.CoursePostsTable.Update(coursePosts);
            databaseContext.SaveChanges();
            return coursePosts;
        }

        public bool Delete(int Id)
        {
            databaseContext.CoursePostsTable.Remove(GetById(Id)) ;
            databaseContext.SaveChanges();
            return true;
        }
    }
}
