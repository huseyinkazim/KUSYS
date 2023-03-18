using KUSYS.Api.Extensions;
using KUSYS.Business;
using KUSYS.Data.DTO;
using KUSYS.Data.Interface.Service;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace KUSYS.Api.FilterAttributes
{
	public class StudentSelfUserValidationFilterAttribute : IActionFilter
	{
		private readonly UserManager<IdentityUser> _userManager;
		private readonly StudentManager _studentManager;
		public StudentSelfUserValidationFilterAttribute(UserManager<IdentityUser> userManager, IStudentService studentService, ICourseService courseService)
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
			int Id = 0;
			if (context.ActionArguments.ContainsKey("dtoObject"))
				Id = ((StudentDTO)context.ActionArguments["dtoObject"]).Id;

			if (roles != null && Id != 0)
			{
				if (roles.Contains("Admin"))
				{
					return;
				}
				else
				{
					var respose = _studentManager.GetStudentByIdWithUser(Id);
					var student = respose.Result;

					if (!respose.IsSuccess || student == null || student.User == null)
					{
						context.Result = new BadRequestObjectResult("User Error");
						return;
					}
					if (student.User.Id != userId)
					{
						context.Result = new BadRequestObjectResult("Different User Error");
						return;
					}

				}
			}
			else
			{
				context.Result = new BadRequestObjectResult("User not have role");
				return;
			}
		}
		public void OnActionExecuted(ActionExecutedContext context)
		{

		}


	}
}
