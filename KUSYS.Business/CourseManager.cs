

using KUSYS.Data.DTO;
using KUSYS.Data.Interface.Service;

namespace KUSYS.Business
{
    public class CourseManager
    {
        private readonly ICourseService _courseService;
        private readonly IStudentService _studentService;
        public CourseManager(ICourseService courseService, IStudentService studentService)
        {
            _courseService = courseService;
            _studentService = studentService;
        }

        public ServiceResponse<CourseDTO> CourseAdd(CourseDTO dtoObject)
        {
            return _courseService.Add(dtoObject);
        }

        public ServiceResponse<CourseDTO> CourseUpdate(CourseDTO dtoObject)
        {
            return _courseService.Update(dtoObject);
        }

        public ServiceResponse<CourseDTO> CourseDelete(CourseDTO dtoObject)
        {
            return _courseService.Delete(dtoObject);
        }

        public ServiceResponse<List<CourseDTO>> GetAllCourses()
        {
            return _courseService.GetAll();
        }
        public ServiceResponse<CourseDTO> GetCourseByIdWithStudent(string id)
        {
            return _courseService.GetCourseByIdWithStudent(id);
        }
        public ServiceResponse<CourseDTO> GetCourseById(string id)
        {
            return _courseService.GetById(id);
        }
    }
}
