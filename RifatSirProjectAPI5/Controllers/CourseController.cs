using Microsoft.AspNetCore.Mvc;
using RifatSirProjectAPI5.Models;
using RifatSirProjectAPI5.Repository;
using System;
using System.Collections.Generic;

namespace RifatSirProjectAPI5.Controllers
{
    public class CourseController : Controller
    {
        private readonly CourseBasicInfoRepository _courseBasicInfoRepository = new CourseBasicInfoRepository();
        private readonly AdminBasicInfoRepository _adminBasicInfoRepository = new AdminBasicInfoRepository();
        private readonly CourseStudentRepository _courseStudentRepository = new CourseStudentRepository();
        private readonly StudentBasicInfoRepository _studentBasicInfoRepository = new StudentBasicInfoRepository();

        [HttpPost("surji/course/createCourse")]
        public IActionResult CreateCourse([FromBody] CourseBasicInfo courseBasicInfo)
        {
            var checkAccount = _courseBasicInfoRepository.GetByCourseIdAndBatchNo(courseBasicInfo.CourseId, courseBasicInfo.BatchNo);

            if (checkAccount == null)
            {
                if (courseBasicInfo.Teacher1UserName != "") 
                {
                    AdminBasicInfo adminBasicInfo = _adminBasicInfoRepository.GetByUserName(courseBasicInfo.Teacher1UserName);
                    if (adminBasicInfo != null)
                    {
                        courseBasicInfo.Teacher1Name = adminBasicInfo.TeacherName;
                    }
                    else 
                    {
                        return Ok("T1 not founded");
                    }
                }

                if (courseBasicInfo.Teacher2UserName != "")
                {
                    AdminBasicInfo adminBasicInfo = _adminBasicInfoRepository.GetByUserName(courseBasicInfo.Teacher2UserName);
                    if (adminBasicInfo != null)
                    {
                        courseBasicInfo.Teacher2Name = adminBasicInfo.TeacherName;
                    }
                    else
                    {
                        return Ok("T2 not founded");
                    }
                }

                var addedStudent = _courseBasicInfoRepository.Add(courseBasicInfo);

                return Ok(true);
            }
            else
            {
                return Ok("already exists");
            }
        }


        [HttpGet("surji/course/getAllCourse")]
        public IActionResult getAllCourseList()
        {
            return Ok(_courseBasicInfoRepository.GetAll());
        }

        [HttpPost("surji/course/updateCourseBasicInfo")]
        public IActionResult updateCourseBasicInfo([FromBody] CourseBasicInfo courseBasicInfo)
        {
            if (courseBasicInfo.Teacher1UserName != "")
            {
                AdminBasicInfo adminBasicInfo = _adminBasicInfoRepository.GetByUserName(courseBasicInfo.Teacher1UserName);
                if (adminBasicInfo != null)
                {
                    courseBasicInfo.Teacher1Name = adminBasicInfo.TeacherName;
                }
                else
                {
                    return Ok("T1 not founded");
                }
            }

            if (courseBasicInfo.Teacher2UserName != "")
            {
                AdminBasicInfo adminBasicInfo = _adminBasicInfoRepository.GetByUserName(courseBasicInfo.Teacher2UserName);
                if (adminBasicInfo != null)
                {
                    courseBasicInfo.Teacher2Name = adminBasicInfo.TeacherName;
                }
                else
                {
                    return Ok("T2 not founded");
                }
            }

            _courseBasicInfoRepository.Update(courseBasicInfo);
            return Ok(true);

        }
        
        [HttpPost("surji/course/deleteCourse")]
        public IActionResult deleteCourse([FromBody] CourseBasicInfo courseBasicInfo)
        {
            _courseStudentRepository.DeleteAllStudentFromCourse(courseBasicInfo.CourseId, courseBasicInfo.BatchNo);
            _courseBasicInfoRepository.Delete(courseBasicInfo.CourseId, courseBasicInfo.BatchNo);
            return Ok(true);
        }


        [HttpPost("surji/course/addStudentToCourse")]
        public IActionResult addStudentToCourse([FromBody] CourseStudent courseStudent)
        {
            var checkAccount = _courseStudentRepository.GetByCourseIdAndBatchNoAndRoll(courseStudent.CourseId, courseStudent.BatchNo, courseStudent.StudentBSSERoll);

            if (checkAccount == null)
            {
                StudentBasicInfo studentBasicInfo = _studentBasicInfoRepository.GetByBSSEROLL(courseStudent.StudentBSSERoll);

                if (studentBasicInfo != null)
                {
                    courseStudent.StudentName = studentBasicInfo.StudentName;
                    _courseStudentRepository.Add(courseStudent);

                    return Ok(true);
                }
                else 
                {
                    return Ok("Roll Not Found");
                }
            }
            else
            {
                return Ok("already exists");
            }
        }


        [HttpPost("surji/course/getAllStudentOfCourse")]
        public IActionResult getAllStudentOfCourse([FromBody] CourseStudent courseStudent)
        {
            return Ok(_courseStudentRepository.GetByCourseIdAndBatchNo(courseStudent.CourseId, courseStudent.BatchNo));
        }


        [HttpPost("surji/course/deleteStudentFromCourse")]
        public IActionResult deleteSingleStudentFromCourse([FromBody] CourseStudent courseStudent)
        {
            _courseStudentRepository.DeleteSingleStudentFromCourse(courseStudent.CourseId, courseStudent.BatchNo, courseStudent.StudentBSSERoll);
            return Ok(true);
        }

        [HttpGet("surji/course/getAllCourseByTeacherUserName")]
        public IActionResult getAllCourseByTeacherUserName(string username)
        {
            return Ok(_courseBasicInfoRepository.GetByTeacherUserName(username));
        }

        [HttpGet("surji/course/getAllCourseByStudentRoll")]
        public IActionResult getAllCourseByStudentRoll(int bsseroll)
        {
            List<CourseStudent> obj = _courseStudentRepository.GetByBSSEROLL(bsseroll);
            List<CourseBasicInfo> obj2 = new List<CourseBasicInfo>();

            foreach (CourseStudent s in obj)
            {
                obj2.Add(_courseBasicInfoRepository.GetByCourseIdAndBatchNo(s.CourseId, s.BatchNo));
            }

            return Ok(obj2);
        }

    }
}
