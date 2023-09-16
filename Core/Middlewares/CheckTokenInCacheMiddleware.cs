using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.JsonWebTokens;

namespace Core.Middlewares;

public class CheckTokenInCacheMiddleware
{
    private readonly RequestDelegate _next;
    
    public CheckTokenInCacheMiddleware(RequestDelegate next)
    {
        _next = next;
    }
    
    public async Task Invoke(HttpContext httpContext)
    {
        var token = httpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
        if (token != null)
        {
            var id = httpContext.User.Claims.First(x => x.Type == JwtRegisteredClaimNames.Sub).Value;
            var cache = httpContext.RequestServices.GetRequiredService<IDistributedCache>();
            var cachedToken = await cache.GetStringAsync(id);
            if (cachedToken != null)
            {
                await _next.Invoke(httpContext);
            }
            else
            {
                httpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            }
        }
        await _next.Invoke(httpContext);
    }
}