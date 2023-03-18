using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace KUSYS.Business
{
    public class TokenManager
    {
        private TokenValidationParameters tokenValidationParameters { set; get; }
        private SymmetricSecurityKey signingKey { set; get; }
        public TokenManager(IConfiguration Configuration)
        {
            signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration["Keys:UserAuthSecretKey"]));
            tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = signingKey,
                ValidIssuer = Configuration["Keys:Issue"],
                ValidAudience = Configuration["Keys:Audience"],
                ValidateLifetime = true
            };
        }
        public string GenerateToken(IdentityUser user,List<string> roles)
        {
            var list = new List<Claim> { 
                new Claim(JwtRegisteredClaimNames.Sub, user.Id), 
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()) ,
                new Claim(ClaimTypes.Name,user.UserName) ,
                new Claim("userId",user.Id) 
            };

            foreach(var item in roles)
            {
                list.Add(new Claim(ClaimTypes.Role, item));
            }
            var token = new JwtSecurityToken(
                   issuer: tokenValidationParameters.ValidIssuer,
                   audience: tokenValidationParameters.ValidAudience,
                   claims: list,
                   expires: DateTime.Now.AddMinutes(30),
                   signingCredentials: new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256)
               );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
