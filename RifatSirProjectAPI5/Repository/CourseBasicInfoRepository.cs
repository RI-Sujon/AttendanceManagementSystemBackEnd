using RifatSirProjectAPI5.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RifatSirProjectAPI5.Repository
{
    public class CourseBasicInfoRepository: DatabaseRepository 
    {
        public CourseBasicInfo Add(CourseBasicInfo courseBasicInfo)
        {
            databaseContext.CourseBasicInfoTable.Add(courseBasicInfo);
            databaseContext.SaveChanges();
            return courseBasicInfo;
        }

        public List<CourseBasicInfo> GetAll()
        {
            return databaseContext.CourseBasicInfoTable.OrderByDescending(CourseBasicInfo => CourseBasicInfo.Id).ToList();
        }

        public CourseBasicInfo GetByCourseIdAndBatchNo(string CourseId, string BatchNo)
        {
            return databaseContext.CourseBasicInfoTable.SingleOrDefault(CourseBasicInfo => CourseBasicInfo.CourseId == CourseId && CourseBasicInfo.BatchNo == BatchNo);
        }

        public object GetByTeacherUserName (string UserName)
        {
            return databaseContext.CourseBasicInfoTable.Where(CourseBasicInfo => CourseBasicInfo.Teacher1UserName == UserName || CourseBasicInfo.Teacher2UserName == UserName).OrderByDescending(CourseBasicInfo => CourseBasicInfo.Id).ToList();
        }

        public CourseBasicInfo Update(CourseBasicInfo courseBasicInfo)
        {
            databaseContext.CourseBasicInfoTable.Update(courseBasicInfo);
            databaseContext.SaveChanges();
            return courseBasicInfo;
        }

        public bool Delete(string CourseId, string BatchNo)
        {
            databaseContext.CourseBasicInfoTable.Remove(GetByCourseIdAndBatchNo(CourseId, BatchNo));
            databaseContext.SaveChanges();
            return true;
        }
    }
}
