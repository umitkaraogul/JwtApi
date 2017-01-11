using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace JwtApi.Security
{
    public class SimpleJwtProvider : IJwtProvider
    {
        private readonly JwtSecurityTokenHandler jwtSecurityTokenHandler;
        private readonly string securityKey = "Hello Jwt";

        public SimpleJwtProvider() {

            jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
        }

        public string CreateJwt()
        {
            List<Claim> claimList = new List<Claim>() { new Claim(ClaimTypes.Name,"jwt") };

            SymmetricSecurityKey security = new SymmetricSecurityKey(GetBytes(securityKey));

            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor() {
                Subject=new ClaimsIdentity(claimList),
                Audience = "audience",
                Issuer = "issuer",
                SigningCredentials = new SigningCredentials(security, SecurityAlgorithms.HmacSha256)
                

            };

            SecurityToken securityToken= jwtSecurityTokenHandler.CreateToken(tokenDescriptor);
            SecurityToken securityTokenjwt = jwtSecurityTokenHandler.CreateJwtSecurityToken(tokenDescriptor);

            string token = jwtSecurityTokenHandler.WriteToken(securityToken);
            string tokenjwt = jwtSecurityTokenHandler.WriteToken(securityToken);

            return token;
        }

        public bool ValidateJwt(string token)
        {
            try
            {
                SymmetricSecurityKey security = new SymmetricSecurityKey(GetBytes(securityKey));

                TokenValidationParameters validationParams = new TokenValidationParameters() {
                    ValidAudiences=new string[] { "audience" },
                    ValidIssuers = new string[] { "issuer" },
                    IssuerSigningKey=security
                };
                SecurityToken validatedToken = null;
                ClaimsPrincipal claimsPrincipal = jwtSecurityTokenHandler.ValidateToken(token, validationParams, out validatedToken);

                return validatedToken != null;
            }
            catch (Exception ex)
            {
                return false;
            }  
        }

        private  byte[] GetBytes(string input)
        {
            var bytes = new byte[input.Length * sizeof(char)];
            Buffer.BlockCopy(input.ToCharArray(), 0, bytes, 0, bytes.Length);

            return bytes;

        }
    }
}