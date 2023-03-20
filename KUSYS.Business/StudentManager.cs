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
		private readonly IStudentService _studentService;
		public StudentManager(IStudentService studentService)
		{
			_studentService = studentService;
		}

		public ServiceResponse<StudentDTO> StudentAdd(StudentDTO dtoObject) => _studentService.Add(dtoObject);
		public ServiceResponse<StudentDTO> StudentUpdate(StudentDTO dtoObject) => _studentService.Update(dtoObject);
		public ServiceResponse<StudentDTO> StudentDelete(StudentDTO dtoObject) => _studentService.Delete(dtoObject);
		public ServiceResponse<List<StudentDTO>> GetAllStudents() => _studentService.GetAll();
		public ServiceResponse<StudentDTO> GetStudentByIdWithCourses(int id) => _studentService.GetStudentByIdWithCourses(id);
		public ServiceResponse<StudentDTO> GetStudentByIdWithUser(int id) => _studentService.GetStudentByIdWithUser(id);
		public ServiceResponse<StudentDTO> GetStudentById(int id) => _studentService.GetById(id);
		public ServiceResponse<StudentDTO> GetStudentbyUserId(string id) => _studentService.GetStudentbyUserId(id);

	}
}
