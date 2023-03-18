using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace KUSYS.WebApplication.UIHandler
{
	public class IdentityUserManager
	{
		public IdentityUserManager()
		{

		}
		public bool IsSignedIn(ClaimsPrincipal principal)
		{
			if (principal == null)
			{
				throw new ArgumentNullException(nameof(principal));
			}
			return principal.Identities != null &&
				principal.Identities.Any(i => i.AuthenticationType == CookieAuthenticationDefaults.AuthenticationScheme);
		}

		public bool IsAdmin(ClaimsPrincipal principal)
		{

			var claimsIdentity = principal.Identity as ClaimsIdentity;
			var items = claimsIdentity.Claims.Where(i => i.Type == ClaimTypes.Role);

			if (items.Any(i => i.Value == "Admin")) { return true; }

			return false;
		}
		public bool IsUS123(ClaimsPrincipal principal)
		{

			var claimsIdentity = principal.Identity as ClaimsIdentity;
			var items = claimsIdentity.Claims.Where(i => i.Type == ClaimTypes.Role);

			if (items.Any(i => i.Value == "US-1" || i.Value == "US-2" || i.Value == "US-3")) { return true; }

			return false;
		}
		public bool IsUS4(ClaimsPrincipal principal)
		{

			var claimsIdentity = principal.Identity as ClaimsIdentity;
			var items = claimsIdentity.Claims.Where(i => i.Type == ClaimTypes.Role);

			if (items.Any(i => i.Value == "US-4")) { return true; }

			return false;
		}
	}
}
