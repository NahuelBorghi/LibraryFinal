namespace LibraryFinal.Middlewares
{
    public class AddCookieHeaderMiddleware
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AddCookieHeaderMiddleware(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task Invoke(HttpContext context, RequestDelegate next)
        {
            var cookie = _httpContextAccessor.HttpContext.Request.Cookies["AuthToken"];
            if (!string.IsNullOrEmpty(cookie))
            {
                context.Request.Headers.Append("Authorization", $"AuthToken={cookie}");
            }

            await next(context);
        }
    }
}
