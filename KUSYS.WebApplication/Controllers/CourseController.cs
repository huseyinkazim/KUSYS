using KUSYS.WebApplication.Models;
using KUSYS.WebApplication.Models.Const;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using KUSYS.UI.UIManager;
using KUSYS.WebApplication.Controllers.Base;

namespace KUSYS.WebApplication.Controllers
{
	public class CourseController : BaseController
	{
		private readonly IProxyManager _proxyManager;
		public CourseController(IProxyManager proxyManager)
		{
			_proxyManager = proxyManager;
		}
		public ActionResult Index()
		{
			var response = _proxyManager.SendRequest<List<CourseDTO>>(ApiUrl.Courses, null, HttpMethod.Get);

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

	}
}
