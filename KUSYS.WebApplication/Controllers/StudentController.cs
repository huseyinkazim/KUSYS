using KUSYS.UI.UIManager;
using KUSYS.WebApplication.Controllers.Base;
using KUSYS.WebApplication.Models;
using KUSYS.WebApplication.Models.Const;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.IdentityModel.Tokens.Jwt;

namespace KUSYS.WebApplication.Controllers
{
	public class StudentController : BaseController
	{
		private readonly IProxyManager _proxyManager;
		public StudentController(IProxyManager proxyManager)
		{
			_proxyManager = proxyManager;
		}

		//Create Student
		[Authorize(Roles = "Admin")]
		public IActionResult Create()
		{
			var response = _proxyManager.SendRequest<List<CourseDTO>>(ApiUrl.Courses, null, HttpMethod.Get);

			if (response == null || !response.IsSuccess)
			{
				ViewBag.Message = response.Error;
				return LocalRedirect("/");
			}
			else
			{
				ViewBag.Courses = response.Result.Select(i => new SelectListItem(i.CourseName, i.CourseId)).ToList();
				ViewBag.Roles = new List<SelectListItem> {
					new SelectListItem("US-1","US-1"),
					new SelectListItem("US-2","US-2"),
					new SelectListItem("US-3","US-3"),
					new SelectListItem("US-4","US-4")
				};

				return View();
			}
		}
		[Authorize(Roles = "Admin")]
		[HttpPost]
		public IActionResult Create(StudentDto studentDto)
		{
			ModelState.Remove("Course");
			if (!ModelState.IsValid)
			{
				return View();
			}
			var response = _proxyManager.SendRequest<StudentDto>(ApiUrl.StudentAdd, studentDto, HttpMethod.Post);

			if (response == null || !response.IsSuccess)
			{
				ViewBag.Message = response.Error;

			}

			return LocalRedirect("/");

		}
		[Authorize(Roles = "Admin")]
		public IActionResult Details()
		{
			var response = _proxyManager.SendRequest<List<StudentDto>>(ApiUrl.StudentGetAll, null, HttpMethod.Get);

			if (response == null || !response.IsSuccess)
			{
				ViewBag.Message = response.Error;
				return LocalRedirect("/");
			}
			else
			{
				return View(response.Result);
			}
		}
		[Authorize(Roles = "Admin")]
		public IActionResult Detail(int Id)
		{
			var studentDto = new StudentDto { Id = Id };

			var response = _proxyManager.SendRequest<StudentDto>(ApiUrl.StudentGetCourseById, studentDto, HttpMethod.Post);
			if (response == null || !response.IsSuccess)
			{
				ViewBag.Message = response.Error;
				return LocalRedirect("/");
			}
			else
			{
				return View(response.Result);
			}
		}

		public IActionResult GetDetail()
		{
			var studentDto = userOfStudent();
			var courses = getCourses();
			if (studentDto == null || courses == null)
				return LocalRedirect("/");
			var response = _proxyManager.SendRequest<StudentDto>(ApiUrl.StudentGetCourseById, studentDto, HttpMethod.Post);
			if (response == null || !response.IsSuccess)
			{
				ViewBag.Message = response.Error;
				return LocalRedirect("/");
			}
			else
			{
				ViewBag.Courses = courses.Select(i => new SelectListItem(i.CourseName, i.CourseId)).ToList();

				return View(response.Result);
			}
		}
		[Authorize(Roles = "US-1,US-2,US-3")]
		public IActionResult DoUpdate()
		{
			var studentDto = userOfStudent();
			if (studentDto == null)
				return LocalRedirect("/");

			var response = _proxyManager.SendRequest<StudentDto>(ApiUrl.StudentGetCourseById, studentDto, HttpMethod.Post);
			var courses = getCourses();
			if (response == null || !response.IsSuccess)
			{
				ViewBag.Message = response.Error;
				return LocalRedirect("/");
			}
			else
			{
				ViewBag.Courses = courses.Select(i => new SelectListItem(i.CourseName, i.CourseId)).ToList();

				return View(response.Result);
			}

		}
		[Authorize(Roles = "Admin")]
		public IActionResult Update(int Id)
		{
			var studentDto = new StudentDto { Id = Id };

			var response = _proxyManager.SendRequest<StudentDto>(ApiUrl.StudentGetCourseById, studentDto, HttpMethod.Post);
			var courses = getCourses();
			if (response == null || !response.IsSuccess || courses == null)
			{
				ViewBag.Message = response.Error;
				return LocalRedirect("/");
			}
			else
			{
				ViewBag.Courses = courses.Select(i => new SelectListItem(i.CourseName, i.CourseId)).ToList();

				return View(response.Result);
			}

		}

		[HttpPost]
		[Authorize(Roles = "Admin,US-1,US-2,US-3")]

		public IActionResult Update(StudentDto studentDto)
		{
			var response = _proxyManager.SendRequest<StudentDto>(ApiUrl.StudentUpdate, studentDto, HttpMethod.Post);

			if (response == null || !response.IsSuccess)
			{
				ViewBag.Message = response.Error;
			}

			return LocalRedirect("/");

		}

		private StudentDto userOfStudent()
		{
			var handler = new JwtSecurityTokenHandler();
			var jsonToken = handler.ReadToken(TokenDto.TokenStatic);
			var tokenS = jsonToken as JwtSecurityToken;
			var user = tokenS.Claims.FirstOrDefault(i => i.Type == "sub");
			var sendData = new { Id = user.Value };
			var response = _proxyManager.SendRequest<StudentDto>(ApiUrl.GetUserStudentId, sendData, HttpMethod.Post);
			if (response == null || !response.IsSuccess)
			{
				ViewBag.Message = response.Error;
				return null;
			}
			return response.Result;
		}
		private List<CourseDTO> getCourses()
		{
			var response = _proxyManager.SendRequest<List<CourseDTO>>(ApiUrl.Courses, null, HttpMethod.Get);
			if (response == null || !response.IsSuccess)
			{
				return null;
			}
			else
			{
				return response.Result;
			}
		}
	}
}
