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
		public StudentController(ICourseService courseService, IStudentService studentService, UserManager<IdentityUser> userManager)
		{
			this.studentManager = new StudentManager(courseService, studentService);
			this.userManager = userManager;
		}
		[HttpPost()]
		[Authorize(Roles = "Admin")]
		[ServiceFilter(typeof(StudentAddValidationFilterAttribute))]
		public ServiceResponse<StudentDTO> StudentAdd([FromBody] StudentDTO dtoObject)
		{
			return studentManager.StudentAdd(dtoObject);
		}
		[HttpPost()]
		[Authorize(Roles = "Admin,US-1,US-2,US-3")]
		[ServiceFilter(typeof(StudentSelfUserValidationFilterAttribute))]
		[ServiceFilter(typeof(StudentValidationFilterAttribute))]
		public string StudentUpdate([FromBody] StudentDTO dtoObject) => JsonConvert.SerializeObject(studentManager.StudentUpdate(dtoObject), new JsonSerializerSettings()
		{
			ReferenceLoopHandling = ReferenceLoopHandling.Ignore
		});
		[Authorize(Roles = "Admin")]
		public string GetAllStudents() => JsonConvert.SerializeObject(studentManager.GetAllStudents(), new JsonSerializerSettings()
		{
			ReferenceLoopHandling = ReferenceLoopHandling.Ignore
		});
		[ServiceFilter(typeof(StudentSelfUserValidationFilterAttribute))]
		public string GetStudentByIdWithCourses([FromBody] StudentDTO dtoObject) => JsonConvert.SerializeObject(studentManager.GetStudentByIdWithCourses(dtoObject.Id), new JsonSerializerSettings()
		{
			ReferenceLoopHandling = ReferenceLoopHandling.Ignore
		});

		[ServiceFilter(typeof(StudentSelfUserValidationFilterAttribute))]
		public string GetStudentByIdWithUser([FromBody] StudentDTO dtoObject) => JsonConvert.SerializeObject(studentManager.GetStudentByIdWithUser(dtoObject.Id), new JsonSerializerSettings()
		{
			ReferenceLoopHandling = ReferenceLoopHandling.Ignore
		});

		[ServiceFilter(typeof(StudentSelfUserValidationFilterAttribute))]
		public string GetCourseById([FromBody] StudentDTO dtoObject) => JsonConvert.SerializeObject(studentManager.GetStudentByIdWithCourses(dtoObject.Id), new JsonSerializerSettings()
		{
			ReferenceLoopHandling = ReferenceLoopHandling.Ignore
		});

		public string GetUserStudentId([FromBody] IdentityUser user) => JsonConvert.SerializeObject(studentManager.GetStudentbyUserId(user.Id), new JsonSerializerSettings()
		{
			ReferenceLoopHandling = ReferenceLoopHandling.Ignore
		});

	}
}
