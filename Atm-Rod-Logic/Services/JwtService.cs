using Atm_Rod_Entities.Entity;
using Atm_Rod_Entities.Interface.Services;
using Atm_Rod_Entities.Response;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Atm_Rod_Logic.Services
{
    public class JwtService : IJwtService
    {
        private const string ClaimCardNumber = "card_number";
        private const int DefautMinutes = 30;
        private readonly IConfiguration _config;
        public JwtService(IConfiguration config)
        {
            _config = config;
        }
        public ResponseLogin GenerateToken(int Number)
        {
            var claims = new[]
             {
                 new Claim(ClaimCardNumber, Number.ToString())
            };
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var expires = Int32.TryParse(_config["Jwt:DurationInMinutes"], out int parsedExpires) ? parsedExpires : DefautMinutes;

            var jwt = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(expires),
                signingCredentials: credentials);

            var token =  new JwtSecurityTokenHandler().WriteToken(jwt);
            return new ResponseLogin(token, expires);
        }

        public bool ValidateToken(string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var securityToken = tokenHandler.ReadToken(token) as JwtSecurityToken;

                var validationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"])),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };

                SecurityToken validatedToken;
                var principal = tokenHandler.ValidateToken(token, validationParameters, out validatedToken);

                var cardNumberClaim = principal.FindFirst(ClaimCardNumber)?.Value;
                var nameClaim = principal.FindFirst(JwtRegisteredClaimNames.Name)?.Value;
                return true;

            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
