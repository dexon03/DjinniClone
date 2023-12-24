using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors(opt =>
{
    opt.AddPolicy("front", policy => policy.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:5173"));
});
builder.Configuration.SetBasePath(builder.Environment.ContentRootPath)
    // .AddOcelot(builder.Environment)
    .AddJsonFile("ocelot.json")
    .AddEnvironmentVariables();
builder.Services.AddOcelot(builder.Configuration);

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

app.UseCors("front");

await app.UseOcelot();

app.Run();