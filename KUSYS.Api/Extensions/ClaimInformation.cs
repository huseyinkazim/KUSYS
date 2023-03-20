using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using KUSYS.Api.Model;

namespace KUSYS.Api.Extensions
{
    public class ClaimInformation
    {
       
        public static List<string> GetClaimRoles(ClaimsPrincipal User)
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var liste = new List<ClaimInfo>();
            var items = claimsIdentity.Claims.Where(i => i.Type == ClaimTypes.Role);

            if (!items.Any()) { return null; }


            return items.Select(i => i.Value).ToList();
        }

           public static string GetClaimUserId(ClaimsPrincipal User)
        {

            var claimsIdentity = User.Identity as ClaimsIdentity;
            var liste = new List<ClaimInfo>();
            var item = claimsIdentity.Claims.FirstOrDefault(i => i.Type == "userId");

            return item != null ? item.Value : "";

        }

    }

}
