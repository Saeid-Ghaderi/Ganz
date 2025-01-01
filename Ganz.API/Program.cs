using Ganz.API;
using Ganz.API.CustomMiddlewares;
using Ganz.Application.CQRS.ProductCommandQuery.Query;
using Ganz.Infrastructure;
using MediatR;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();
//builder.Services.Configure<Configs>(builder.Configuration.GetSection("Configs"));



DependencyRegistrar.RegisterServices(builder.Services, builder.Configuration);
builder.Services.AddSwagger();
builder.Services.AddJWT();
builder.Services.AddMemoryCache();

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining(typeof(GetProductQuery)));

var app = builder.Build();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
           Path.Combine(builder.Environment.ContentRootPath, "Media")),
    RequestPath = "/Media"
});

//call CustomMiddleware
app.UseLoggingMiddleware();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program { }
