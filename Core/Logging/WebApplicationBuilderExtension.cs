using Microsoft.AspNetCore.Builder;
using Serilog;

namespace Core.Logging;

public static class WebApplicationBuilderExtension
{
    public static WebApplication UseSerilogLogging(this WebApplication builder)
    {
        builder.UseSerilogRequestLogging();
        return builder;
    }
    
    public static WebApplicationBuilder AddLogging(this WebApplicationBuilder builder)
    {
        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(builder.Configuration)
            .WriteTo.Console()
            .CreateLogger();
        builder.Host.UseSerilog();
        return builder;
    }
}