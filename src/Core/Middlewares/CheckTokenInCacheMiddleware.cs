using System.IdentityModel.Tokens.Jwt;
using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.DependencyInjection;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

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
            var jwtToken = new JwtSecurityTokenHandler().ReadJwtToken(token);
            var id = jwtToken.Claims.First(x => x.Type == JwtRegisteredClaimNames.Sub).Value;
            var cache = httpContext.RequestServices.GetRequiredService<IDistributedCache>();
            var cachedToken = await cache.GetStringAsync(id);
            if (cachedToken == null || cachedToken != token)
            {
                httpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                return;
            }
        }
        await _next.Invoke(httpContext);
    }
}