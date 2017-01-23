using JwtApi.Security;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Filters;

namespace JwtApi.Filters
{
    public class JwtAuthenticationFilterAttribute : Attribute, IAuthenticationFilter
    {
        public bool AllowMultiple
        {
            get
            {
                return true;
            }
        }

        //TODO:Convert async
        public  Task AuthenticateAsync(HttpAuthenticationContext context, CancellationToken cancellationToken)
        {
            HttpRequestMessage request = context.Request;
            AuthenticationHeaderValue authorization = request.Headers.Authorization;

            if (authorization == null)
                return Task.FromResult(-1);

            if (!ValidateToken(authorization.Parameter))
            {
                context.ErrorResult = new CustomHttpActionResult(request);
                return Task.FromResult(-1);
            }

            IPrincipal principal = AuthenticationJwt();
            context.Principal = principal;

            return Task.FromResult(0);
        }

        public Task ChallengeAsync(HttpAuthenticationChallengeContext context, CancellationToken cancellationToken)
        {
            return Task.FromResult(0);
        }

        //Todo:Convert Task
        private IPrincipal AuthenticationJwt()
        {

            List<Claim> claims = new List<Claim> { new Claim(ClaimTypes.Name, "user") };
            ClaimsIdentity identity = new ClaimsIdentity(claims, "jwt") { };
            IPrincipal principal = new ClaimsPrincipal(identity);

            return principal;
            //return Task.FromResult<IPrincipal>(null);
        }

        private bool ValidateToken(string token)
        {
            SimpleJwtProvider jwtProvider = new SimpleJwtProvider();
            return jwtProvider.ValidateJwt(token);
            
        }
    }

}