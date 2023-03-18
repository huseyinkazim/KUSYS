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
        public string IsUser() { return "OK"; }

        [HttpGet()]
        [Authorize(Roles = "US-2")]
        public string IsUS() { return "OK"; }
        [HttpGet()]
        [Authorize(Roles = "Admin,US-2")]
        public string IsUserOrAdmin()
        {
            var liste = ClaimInformation.GetClaimInfos(HttpContext.User);
            var userName = ClaimInformation.GetClaimUserName(HttpContext.User);
            var liste2 = ClaimInformation.GetClaimRoles(HttpContext.User);
            var user = userManager.GetUserName(HttpContext.User);


            return "OK";
        }

        [HttpGet()]
        [Authorize(Roles = "Admin")]
        public string IsAdmin() { return "OK"; }

    }

}
