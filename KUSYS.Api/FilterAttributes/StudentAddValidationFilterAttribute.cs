using KUSYS.Api.Extensions;
using KUSYS.Business;
using KUSYS.Data.DTO;
using KUSYS.Data.Interface.Service;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace KUSYS.Api.FilterAttributes
{
	public class StudentAddValidationFilterAttribute : IActionFilter
	{
		private readonly UserManager<IdentityUser> _userManager;
		private readonly StudentManager _studentManager;
		public StudentAddValidationFilterAttribute(UserManager<IdentityUser> userManager, IStudentService studentService, ICourseService courseService)
		{
			_userManager = userManager;
			_studentManager = new StudentManager(courseService, studentService);
		}
		//Admin harici kullanıcılar için id üzerinden baskalrının bilgilerine erişim engeli
		public void OnActionExecuting(ActionExecutingContext context)
		{
			var User = context.HttpContext.User;
			var roles = ClaimInformation.GetClaimRoles(User);
			var userId = ClaimInformation.GetClaimUserId(User);

			StudentDTO studentDTO = null;
			if (context.ActionArguments.ContainsKey("dtoObject"))
				studentDTO = ((StudentDTO)context.ActionArguments["dtoObject"]);



			var user = _userManager.FindByNameAsync($"{studentDTO.FirstName}.{studentDTO.LastName}").Result; 
			

			if (user != null)
			{
				context.Result = new BadRequestObjectResult("User Error");
				return;
			}
			

		}
		public void OnActionExecuted(ActionExecutedContext context)
		{

		}


	}
}
