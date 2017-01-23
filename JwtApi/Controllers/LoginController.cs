using JwtApi.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace JwtApi.Controllers
{
    public class LoginController : ApiController
    {
        [AllowAnonymous]
        public string Get(string username, string password)
        {
            if (ValidateUser(username,password))
            {
                SimpleJwtProvider jwtProvider = new SimpleJwtProvider();
                return jwtProvider.CreateJwt();
            }

            throw new HttpResponseException(HttpStatusCode.Unauthorized);
        }

        private bool ValidateUser(string username, string password)
        {
            if (username == "admin" && password == "admin")
                return true;

            return false;
        }
    }
}
