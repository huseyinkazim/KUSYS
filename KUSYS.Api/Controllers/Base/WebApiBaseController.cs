using Azure.Core;
using KUSYS.Api.Extensions;
using KUSYS.Business;
using KUSYS.Data.DTO;
using KUSYS.Data.Interface.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;

namespace KUSYS.Api.Controllers.Base
{
    [Authorize]
    [Route("api/[controller]/[action]")]
	[ApiController]
	public class WebApiBaseController : ControllerBase
    {
        public WebApiBaseController()
        {

        }
    }


}
