using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace Core.Logging;

public static class WebApplicationBuilderExtension
{
    public static void UseSerilogLogging(this WebApplication builder)
    {
        builder.UseSerilogRequestLogging();
    }
    
    public static void AddSerilogLogging(this WebApplicationBuilder builder)
    {
        builder.Services.AddSingleton(Log.Logger);
        builder.Host.UseSerilog();
    }
}