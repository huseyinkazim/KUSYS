using KUSYS.Api.Extensions;
using KUSYS.Business;
using KUSYS.Data.DTO;
using KUSYS.Data.Interface.Service;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace KUSYS.Api.FilterAttributes
{
	public class StudentValidationFilterAttribute : IActionFilter
	{
		private readonly UserManager<IdentityUser> _userManager;
		private readonly StudentManager _studentManager;

		public StudentValidationFilterAttribute(UserManager<IdentityUser> userManager, IStudentService studentService, ICourseService courseService)
		{
			_userManager = userManager;
			_studentManager = new StudentManager(courseService, studentService);
		}
		//Only change FirstName LastName
		public void OnActionExecuting(ActionExecutingContext context)
		{
			var User = context.HttpContext.User;
			var roles = ClaimInformation.GetClaimRoles(User);
			var userId = ClaimInformation.GetClaimUserId(User);
			StudentDTO studentDTO = null, student = null;

			if (context.ActionArguments.ContainsKey("dtoObject"))
				studentDTO = context.ActionArguments["dtoObject"] as StudentDTO;

			if (studentDTO != null && studentDTO.Id != 0)
			{
				var response = _studentManager.GetStudentById(studentDTO.Id);
				student = response.IsSuccess ? response.Result : null;
				if (student == null)
				{
					context.Result = new BadRequestObjectResult("Student not found");
					return;
				}

			}
			else
			{
				context.Result = new BadRequestObjectResult("Student is null");
				return;
			}
		}
		public void OnActionExecuted(ActionExecutedContext context)
		{

		}

	}
}
