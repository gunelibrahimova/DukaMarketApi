using Core.Entity.Models;
using Core.Security.Models;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Security.TokenHandler
{
    public class TokenGenerator
    {
        private readonly JWTConfig _jWTConfig;

        public TokenGenerator(JWTConfig jWTConfig)
        {
            _jWTConfig = jWTConfig;
        }

        public string Token(K205User user)
        {
            var jwtTokenhandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("qwertyuiopasdfghjklzxcvbnmqwertyuiopasdfghjklzxcvbnmqwertyuiopasdfghjklzxcvbnm");


            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject = new System.Security.Claims.ClaimsIdentity(new[]
                {
                    new System.Security.Claims.Claim(JwtRegisteredClaimNames.NameId, user.Id.ToString()),
                    new System.Security.Claims.Claim(JwtRegisteredClaimNames.NameId, user.Email),
                    new System.Security.Claims.Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),

                }),

                Expires = DateTime.UtcNow.AddMinutes(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature)
                
            };

            var token = jwtTokenhandler.CreateToken(tokenDescription);
            return jwtTokenhandler.WriteToken(token);
        }
    }
}
