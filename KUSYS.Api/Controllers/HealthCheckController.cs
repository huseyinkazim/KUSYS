using KUSYS.Api.Controllers.Base;
using KUSYS.Api.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace KUSYS.Api.Controllers
{
	//[Route("api/[controller]/[action]")]
	public class HealthCheckController : WebApiBaseController
	{
		private readonly UserManager<IdentityUser> userManager;

		public HealthCheckController(UserManager<IdentityUser> userManager)
		{
			this.userManager = userManager;
		}

		[HttpGet()]
		[AllowAnonymous]

		public string IsOk() { return "OK"; }

		[HttpGet()]
		[Authorize]
		[NonAction]
		public string IsUser() { return "OK"; }

		[HttpGet()]
		[NonAction]
		[Authorize(Roles = "US-2")]
		public string IsUS() { return "OK"; }
		[HttpGet()]
		[NonAction]
		[Authorize(Roles = "Admin,US-2")]
		public string IsUserOrAdmin()
		{
			return "OK";
		}

		[HttpGet()]
		[NonAction]
		[Authorize(Roles = "Admin")]
		public string IsAdmin() { return "OK"; }

	}

}
