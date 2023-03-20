using KUSYS.WebApplication.Controllers.Base;
using KUSYS.WebApplication.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NuGet.Common;
using System.Diagnostics;
using System.Net.Http.Headers;
using System.Net.Http;
using Newtonsoft.Json;
using Microsoft.DotNet.MSIdentity.Shared;
using KUSYS.WebApplication.Models.Const;
using KUSYS.UI.UIManager;

namespace KUSYS.WebApplication.Controllers
{
	[AllowAnonymous]
	public class HomeController : BaseController
	{
		private readonly IProxyManager _proxyManager;
		public HomeController(IProxyManager proxyManager)
		{
			_proxyManager = proxyManager;
		}
		public async Task<IActionResult> Index()
		{
			
				return View();
		}


	}
}