

using KUSYS.Data.DTO;
using KUSYS.Data.Interface.Service;

namespace KUSYS.Business
{
	public class CourseManager
	{
		private readonly ICourseService _courseService;
		public CourseManager(ICourseService courseService)
		{
			_courseService = courseService;
		}

		public ServiceResponse<CourseDTO> CourseAdd(CourseDTO dtoObject) => _courseService.Add(dtoObject);
		public ServiceResponse<CourseDTO> CourseUpdate(CourseDTO dtoObject) => _courseService.Update(dtoObject);
		public ServiceResponse<CourseDTO> CourseDelete(CourseDTO dtoObject) => _courseService.Delete(dtoObject);
		public ServiceResponse<List<CourseDTO>> GetAllCourses() => _courseService.GetAll();
		public ServiceResponse<CourseDTO> GetCourseByIdWithStudent(string id) => _courseService.GetCourseByIdWithStudent(id);
		public ServiceResponse<CourseDTO> GetCourseById(string id) => _courseService.GetById(id);
	}
}
