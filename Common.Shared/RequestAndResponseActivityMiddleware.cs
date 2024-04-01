using Microsoft.AspNetCore.Http;

namespace Common.Shared
{
    public class RequestAndResponseActivityMiddleware
    {
        private readonly RequestDelegate _next;
        public RequestAndResponseActivityMiddleware(RequestDelegate next)
        {
            _next = next;
        }






        private async Task ActivityResponse(HttpContext context)
        {

        }

        private async Task ActivityRequest(HttpContext context)
        {

        }

    }
}
