using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace JwtApi.Filters
{
    public class CustomHttpActionResult : IHttpActionResult
    {
        private HttpRequestMessage _request;
        public CustomHttpActionResult(HttpRequestMessage request)
        {
            this._request = request;
        }

        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.Unauthorized)
            {
                RequestMessage = this._request,
                ReasonPhrase = "Authentication Error"
            };

            return Task.FromResult(response);
        }
    }
}