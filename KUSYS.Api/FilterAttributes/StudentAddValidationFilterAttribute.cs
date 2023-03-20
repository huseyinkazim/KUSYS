using KUSYS.Api.Extensions;
using KUSYS.Business;
using KUSYS.Data.DTO;
using KUSYS.Data.Interface.Service;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Text.RegularExpressions;

namespace KUSYS.Api.FilterAttributes
{
	public class StudentAddValidationFilterAttribute : IActionFilter
	{
		private readonly UserManager<IdentityUser> _userManager;
		private readonly StudentManager _studentManager;
		public StudentAddValidationFilterAttribute(UserManager<IdentityUser> userManager, IStudentService studentService)
		{
			_userManager = userManager;
			_studentManager = new StudentManager(studentService);
		}
		//Admin harici kullanıcılar için id üzerinden baskalrının bilgilerine erişim engeli
		public void OnActionExecuting(ActionExecutingContext context)
		{
			var User = context.HttpContext.User;

			StudentDTO studentDTO = null;
			if (context.ActionArguments.ContainsKey("dtoObject"))
				studentDTO = ((StudentDTO)context.ActionArguments["dtoObject"]);


			if (studentDTO == null || string.IsNullOrEmpty(studentDTO.FirstName) || string.IsNullOrEmpty(studentDTO.LastName))
			{
				context.Result = new BadRequestObjectResult("FirstName LastName cannot empty");
				return;
			}
			if(!englishCharacterControl(studentDTO))
			{
				context.Result = new BadRequestObjectResult("FirstName LastName must contain just english chracter");
				return;
			}

			var user = _userManager.FindByNameAsync(userNameOfStudent(studentDTO)).Result;


			if (user != null)
			{
				context.Result = new BadRequestObjectResult("User Error");
				return;
			}

		}
		public void OnActionExecuted(ActionExecutedContext context)
		{

		}
		private string userNameOfStudent(StudentDTO studentDTO)
		{
			return $"{studentDTO.FirstName.Replace(" ", "")}.{studentDTO.LastName.Replace(" ", "")}".ToLower();
		}
		private bool englishCharacterControl(StudentDTO studentDTO) => Regex.IsMatch(studentDTO.FirstName, "^[a-zA-Z0-9]*$") && Regex.IsMatch(studentDTO.LastName, "^[a-zA-Z0-9]*$");

	}
}
