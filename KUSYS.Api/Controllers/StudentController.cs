using KUSYS.Api.Controllers.Base;
using KUSYS.Api.Extensions;
using KUSYS.Api.FilterAttributes;
using KUSYS.Business;
using KUSYS.Data.DTO;
using KUSYS.Data.Interface.Service;
using KUSYS.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data;
using System.Text;

namespace KUSYS.Api.Controllers
{
	public class StudentController : WebApiBaseController
	{
		private readonly StudentManager studentManager;
		private readonly UserManager<IdentityUser> userManager;
		public StudentController(IStudentService studentService, UserManager<IdentityUser> userManager)
		{
			this.studentManager = new StudentManager(studentService);
			this.userManager = userManager;
		}
		[HttpPost()]
		[Authorize(Roles = "Admin")]
		[ServiceFilter(typeof(StudentAddValidationFilterAttribute))]
		public ServiceResponse<StudentDTO> StudentAdd([FromBody] StudentDTO dtoObject)
		//=> studentManager.StudentAdd(dtoObject);
		{
			var sa = ModelState;
			return studentManager.StudentAdd(dtoObject);
		}

		[HttpPost()]
		[Authorize(Roles = "Admin,US-1,US-2,US-3")]
		[ServiceFilter(typeof(StudentSelfUserValidationFilterAttribute))]
		[ServiceFilter(typeof(StudentValidationFilterAttribute))]
		public ServiceResponse<StudentDTO> StudentUpdate([FromBody] StudentDTO dtoObject)
			=> studentManager.StudentUpdate(dtoObject);

		[Authorize(Roles = "Admin")]
		[HttpGet]
		public ServiceResponse<List<StudentDTO>> GetAllStudents()
			=> studentManager.GetAllStudents();

		[HttpGet]
		[ServiceFilter(typeof(StudentSelfUserValidationFilterAttribute))]
		public ServiceResponse<StudentDTO> GetStudentByIdWithCourses([FromBody] StudentDTO dtoObject)
			=> studentManager.GetStudentByIdWithCourses(dtoObject.Id);

		[HttpGet]
		[ServiceFilter(typeof(StudentSelfUserValidationFilterAttribute))]
		public ServiceResponse<StudentDTO> GetStudentByIdWithUser([FromBody] StudentDTO dtoObject)
			=> studentManager.GetStudentByIdWithUser(dtoObject.Id);

		[HttpGet]
		[ServiceFilter(typeof(StudentSelfUserValidationFilterAttribute))]
		public ServiceResponse<StudentDTO> GetCourseById([FromBody] StudentDTO dtoObject)
			=> studentManager.GetStudentByIdWithCourses(dtoObject.Id);

		[HttpGet]
		public ServiceResponse<StudentDTO> GetUserStudentId([FromBody] IdentityUser user)
			=> studentManager.GetStudentbyUserId(user.Id);

	}
}
