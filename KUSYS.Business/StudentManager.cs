using KUSYS.Data.DTO;
using KUSYS.Data.Interface.Service;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUSYS.Business
{
    public class StudentManager
    {
        private readonly ICourseService _courseService;
        private readonly IStudentService _studentService;
        public StudentManager(ICourseService courseService, IStudentService studentService)
        {
            _courseService = courseService;
            _studentService = studentService;
        }

        public ServiceResponse<StudentDTO> StudentAdd(StudentDTO dtoObject)
        {
            return _studentService.Add(dtoObject);
        }

        public ServiceResponse<StudentDTO> StudentUpdate(StudentDTO dtoObject)
        {
            return _studentService.Update(dtoObject);
        }

        public ServiceResponse<StudentDTO> StudentDelete(StudentDTO dtoObject)
        {
            return _studentService.Delete(dtoObject);
        }
        public ServiceResponse<List<StudentDTO>> GetAllStudents()
        {
            return _studentService.GetAll();
        }
        public ServiceResponse<StudentDTO> GetStudentByIdWithCourses(int id)
        {
            return _studentService.GetStudentByIdWithCourses(id);
        }
        public ServiceResponse<StudentDTO> GetStudentByIdWithUser(int id)
        {
            return _studentService.GetStudentByIdWithUser(id);
        }
        public ServiceResponse<StudentDTO> GetStudentById(int id)
        {
            return _studentService.GetById(id);
        }
		public ServiceResponse<StudentDTO> GetStudentbyUserId(string id)
		{
			return _studentService.GetStudentbyUserId(id);
		}
		
	}
}
