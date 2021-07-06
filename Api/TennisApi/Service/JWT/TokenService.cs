using TennisApi.Shared.Auth;
using TennisApi.Shared.Configs;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;

namespace TennisApi.Service.JWT
{
    public static class TokenService
    {
        public static string GenerateToken(TokenModel model, AuthConfig config)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(config.ClientSecret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("Email", model.Email),
                    new Claim("Nome", model.Nome),
                    new Claim("Status", model.Status),
                    new Claim("ID", model.IDLogin.ToString()),
                    new Claim("DataLogin", DateTime.UtcNow.ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public static TokenModel ReadToken(ClaimsPrincipal claims)
        {
            var token = new TokenModel
            {
                IDLogin = Guid.Parse(claims.FindFirst("ID").Value),
                Email = claims.FindFirst("Email").Value,
                Nome = claims.FindFirst("Nome").Value,
                Status = claims.FindFirst("Status").Value
            };

            return token;
        }

        //public static TokenModel ReadToken(string  accessToken, AuthConfig config)
        //{
        //    var tokenHandler = new JwtSecurityTokenHandler();
        //    var key = Encoding.ASCII.GetBytes(config.ClientSecret);
        //    var validations = new TokenValidationParameters
        //    {
        //        ValidateIssuerSigningKey = true,
        //        IssuerSigningKey = new SymmetricSecurityKey(key),
        //        ValidateIssuer = false,
        //        ValidateAudience = false
        //    };
        //    var claims = tokenHandler.ValidateToken(accessToken, validations, out var tokenSecure);

        //    var token = new TokenModel
        //    {
        //        IDLogin = Guid.Parse(claims.FindFirst("Sid").Value),
        //        Email = claims.FindFirst("Email").Value,
        //        Nome = claims.FindFirst("Name").Value,
        //        IDStatus = int.Parse(claims.FindFirst("Status").Value),
        //        AccessToken = accessToken
        //    };

        //    return token;
        //}
    }
}
