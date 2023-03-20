
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
        public CourseController(ICourseService courseService)
        {
            courseManager = new CourseManager(courseService);
        }
        [ResponseCache(Duration =300)]
        [HttpGet()]
        public ServiceResponse<List<CourseDTO>> GetCoursesAll() => courseManager.GetAllCourses();
    }
}
