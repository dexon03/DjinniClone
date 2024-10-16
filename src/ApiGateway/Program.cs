using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();
builder.Services.AddCors(opt =>
{
    opt.AddPolicy("front", policy => policy.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:5173").AllowCredentials());
});
builder.Configuration.SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("ocelot.json")
    .AddEnvironmentVariables();
builder.Services.AddOcelot(builder.Configuration);
builder.Services.AddSignalR();

var app = builder.Build();

app.MapDefaultEndpoints();

app.UseAuthentication();
app.UseAuthorization();

app.UseCors("front");

app.UseWebSockets();
await app.UseOcelot();

app.Run();