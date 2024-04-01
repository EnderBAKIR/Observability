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


        public async Task InvokeAsync(HttpContext context)
        {
            context.Request.EnableBuffering();

            var requestBodyStreamReader = new StreamReader(context.Request.Body);

            var requestBody = await requestBodyStreamReader.ReadToEndAsync();
        }



        private async Task ActivityResponse(HttpContext context)
        {

        }

        private async Task ActivityRequest(HttpContext context)
        {

        }

    }
}
