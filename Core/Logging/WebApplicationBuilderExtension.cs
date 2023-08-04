using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace Core.Logging;

public static class WebApplicationBuilderExtension
{
    public static WebApplication UseSerilogLogging(this WebApplication builder)
    {
        builder.UseSerilogRequestLogging();
        return builder;
    }
    
    public static WebApplicationBuilder AddSerilogLogging(this WebApplicationBuilder builder)
    {
        builder.Services.AddSingleton(Log.Logger);
        builder.Host.UseSerilog();
        return builder;
    }
}