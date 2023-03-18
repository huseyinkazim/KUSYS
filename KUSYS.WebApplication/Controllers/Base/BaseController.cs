using KUSYS.UI.UIManager;
using KUSYS.WebApplication.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KUSYS.WebApplication.Controllers.Base
{
	[Authorize]
	public class BaseController : Controller
	{
		protected IActionResult ResponseToView<T>(ServiceResponse<T> response, string reDirectUrl = null, bool withResult = true, string defaultSuccessMessage = "İşleminiz Başarılı", string defauktFailMessage = "İşleminiz Başarısız")
		{
			if (response == null || !response.IsSuccess)
			{
				ViewBag.isSuccess = false;
				ViewBag.Message = string.IsNullOrEmpty(response.Error) ? defauktFailMessage : response.Error;
				return LocalRedirect("/");
			}
			else
			{
				TempData["isSuccess"] = true;
				TempData["Message"] = defaultSuccessMessage;
				if (reDirectUrl == null && withResult)
					return View(response.Result);
				else if(reDirectUrl == null && !withResult)
					return View();
				else
					return LocalRedirect(reDirectUrl);
			}
		}

	}
}
