using JwtApi.Filters;
using System.Web.Http;

namespace JwtApi.Controllers
{
    public class ValueController : ApiController
    {
        [JwtAuthenticationFilter]
        public string Get() {
            return "Success.";
        }
    }
}
