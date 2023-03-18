using KUSYS.Api.Controllers.Base;
using KUSYS.Business;
using KUSYS.Data.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Reflection.PortableExecutable;

namespace KUSYS.Api.Controllers
{

    public class TokenController : WebApiBaseController
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly TokenManager _tokenManager;

        public TokenController(UserManager<IdentityUser> userManager, IConfiguration Configuration)
        {
            _userManager = userManager;
            _tokenManager = new TokenManager(Configuration);
        }
        [AllowAnonymous]
        [HttpPost]
        public IActionResult GetToken([FromBody] LoginDto login)
        {
            //LoginDto login = JsonConvert.DeserializeObject<LoginDto>(stuff["LoginDto"].ToString());

            if (login == null)
                return BadRequest("");
            var user = _userManager.FindByNameAsync(login.Username).Result;

            if (user != null && _userManager.CheckPasswordAsync(user, login.Password).Result)
            {
                var Token = _tokenManager.GenerateToken(user, _userManager.GetRolesAsync(user).Result.ToList());

                return Ok(new ServiceResponse<TokenDto>(new TokenDto { Token = Token }));
            }
            else
            {
                return Unauthorized();
            }
        }



    }
}
