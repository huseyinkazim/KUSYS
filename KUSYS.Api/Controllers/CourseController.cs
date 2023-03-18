
using KUSYS.Api.Controllers.Base;
using KUSYS.Business;
using KUSYS.Data.DTO;
using KUSYS.Data.Interface.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace KUSYS.Api.Controllers
{
    public class CourseController : WebApiBaseController
    {
        private readonly CourseManager courseManager;
        public CourseController(ICourseService courseService, IStudentService studentService)
        {

            courseManager = new CourseManager(courseService, studentService);
        }

        //[HttpPost()]
        //[Authorize(Roles = "Admin")]
        //public ServiceResponse<CourseDTO> AddCourse(CourseDTO courseDTO) => courseManager.CourseAdd(courseDTO);

        //[HttpPost()]
        //[Authorize(Roles = "Admin")]
        //public ServiceResponse<CourseDTO> UpdateCourse(CourseDTO courseDTO) => courseManager.CourseUpdate(courseDTO);

        //[HttpPost()]
        //[Authorize(Roles = "Admin")]
        //public ServiceResponse<CourseDTO> DeletCourse(CourseDTO courseDTO) => courseManager.CourseUpdate(courseDTO);

        //[HttpGet()]
        //[Authorize(Roles = "Admin")]
        //public ServiceResponse<CourseDTO> GetCourseById(string id) => courseManager.GetCourseById(id);

        [HttpGet()]
        public ServiceResponse<List<CourseDTO>> GetCoursesAll() => courseManager.GetAllCourses();
    }
}
