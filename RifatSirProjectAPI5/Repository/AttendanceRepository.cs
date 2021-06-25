using RifatSirProjectAPI5.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RifatSirProjectAPI5.Repository
{
    public class AttendanceRepository: DatabaseRepository
    {
        public Attendance Add(Attendance attendance)
        {
            databaseContext.AttendanceTable.Add(attendance);
            databaseContext.SaveChanges();
            return attendance;
        }

        public List<Attendance> GetByCourseIdAndBatchNo(string CourseId, string BatchNo)
        {
            return databaseContext.AttendanceTable.Where(Attendance => Attendance.CourseId == CourseId && Attendance.BatchNo == BatchNo).OrderByDescending(Attendance => Attendance.DateAndTime).ToList();
        }

        public List<Attendance> GetByCourseIdAndBatchNoAndDateTime(string CourseId, string BatchNo, DateTime DateAndTime)
        {
            return databaseContext.AttendanceTable.Where(Attendance => Attendance.CourseId == CourseId && Attendance.BatchNo == BatchNo && Attendance.DateAndTime == DateAndTime).ToList();
        }

        public List<Attendance> GetByCourseIdAndBatchNoAndBSSEROLL(string CourseId, string BatchNo, int BSSEROLL)
        {
            return databaseContext.AttendanceTable.Where(Attendance => Attendance.CourseId == CourseId && Attendance.BatchNo == BatchNo && Attendance.BSSEROLL == BSSEROLL).ToList();
        }

        public Attendance GetByCourseIdAndBatchNoAndBSSEROLLAndDateTime(string CourseId, string BatchNo, int BSSEROLL, DateTime DateAndTime)
        {
            return databaseContext.AttendanceTable.SingleOrDefault(Attendance => Attendance.CourseId == CourseId && Attendance.BatchNo == BatchNo && Attendance.BSSEROLL == BSSEROLL && Attendance.DateAndTime == DateAndTime);
        }

        public Attendance GetById(int Id)
        {
            return databaseContext.AttendanceTable.SingleOrDefault(Attendance => Attendance.Id == Id);
        }

        public bool Delete(int Id)
        {
            databaseContext.AttendanceTable.Remove(GetById(Id));
            databaseContext.SaveChanges();
            return true;
        }
    }
}
