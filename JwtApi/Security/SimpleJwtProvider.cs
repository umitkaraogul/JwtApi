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
        private readonly string securityKey = "a514385c32a74d93ac2bc2c075e665e3";

        public SimpleJwtProvider()
        {

            jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
        }

        public string CreateJwt()
        {
            List<Claim> claimList = new List<Claim>() { new Claim(ClaimTypes.Name, "jwt") };
            var symetricKey = Convert.FromBase64String(securityKey);

            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(claimList),
                Audience = "audience",
                Issuer = "issuer",
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(symetricKey), SecurityAlgorithms.HmacSha256)
            };

            SecurityToken securityToken = jwtSecurityTokenHandler.CreateToken(tokenDescriptor);
            string token = jwtSecurityTokenHandler.WriteToken(securityToken);

            return token;
        }

        public bool ValidateJwt(string token)
        {
            try
            {

                var symetricKey = Convert.FromBase64String(securityKey);

                TokenValidationParameters validationParams = new TokenValidationParameters()
                {
                    ValidAudiences = new string[] { "audience" },
                    ValidIssuers = new string[] { "issuer" },
                    IssuerSigningKey = new SymmetricSecurityKey(symetricKey)
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

        private byte[] GetBytes(string input)
        {
            var bytes = new byte[input.Length * sizeof(char)];
            Buffer.BlockCopy(input.ToCharArray(), 0, bytes, 0, bytes.Length);

            return bytes;

        }
    }
}