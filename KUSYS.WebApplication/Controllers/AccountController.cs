using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using KUSYS.WebApplication.Models;
using KUSYS.UI.UIManager;
using KUSYS.WebApplication.Models.Const;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using KUSYS.WebApplication.Controllers.Base;
using Microsoft.AspNetCore.Authorization;

namespace KUSYS.UI.Controllers
{
	public class AccountController : BaseController
	{
		private readonly IProxyManager _proxyManager;
		public AccountController(IProxyManager proxyManager)
		{
			_proxyManager = proxyManager;
		}

		[AllowAnonymous]
		public IActionResult Login()
		{
			return View();
		}
		[HttpPost]
		[AllowAnonymous]

		public async Task<IActionResult> Login(LoginDto loginUser)
		{
			var response = _proxyManager.SendRequest<TokenDto>(ApiUrl.LoginUrl, loginUser, HttpMethod.Post);

			if (response == null || !response.IsSuccess)
			{
				ViewBag.Message = response.Error;
				return View(loginUser);
			}
			else
			{
				var list = generateClaims();
				var expiration = long.Parse(list.FirstOrDefault(i => i.Type == "exp").Value);
				TokenDto.expDate = DateTimeOffset.FromUnixTimeMilliseconds(expiration).DateTime;
				ClaimsIdentity identity = new ClaimsIdentity(list, CookieAuthenticationDefaults.AuthenticationScheme);
				ClaimsPrincipal principal = new ClaimsPrincipal(identity);
				await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

				return LocalRedirect("/");
			}
		}
		public async Task<IActionResult> LogOut()
		{
			TokenDto.TokenStatic = string.Empty;
			await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
			return LocalRedirect("/");

		}
		private List<Claim> generateClaims()
		{
			var handler = new JwtSecurityTokenHandler();
			var jsonToken = handler.ReadToken(TokenDto.TokenStatic);
			var tokenS = jsonToken as JwtSecurityToken;
			return tokenS.Claims.ToList();
		}
	}
}
