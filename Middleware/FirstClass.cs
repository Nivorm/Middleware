namespace Middleware
{
    public class FirstClass
    {
        private RequestDelegate _next;

        public FirstClass(RequestDelegate next)
        {
            _next = next;
        }

        public Task Invoke(HttpContext context)
        {
            var r = context.Request.Headers["User-Agent"].ToString();

            if (r.Contains("Edg"))
            {
                context.Response.Redirect("https://www.mozilla.org/pl/firefox/new/%22");
            }

            return _next(context);
        }

    }

    public static class UserAgentMiddleware
    {
        public static IApplicationBuilder UseUrlTransformMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<FirstClass>();
        }
    }
}