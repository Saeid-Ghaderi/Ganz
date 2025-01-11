using Ganz.API;
using Ganz.API.CustomMiddlewares;
using Ganz.API.General;
using Ganz.Application.CQRS.ProductCommandQuery.Query;
using Ganz.Application.Services.Elastic;
using Ganz.Application.Services.SMS_gRPC;
using Ganz.Domain.Elastic;
using Ganz.Infrastructure;
using Ganz.Infrastructure.Persistence.Elastic;
using MediatR;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Options;
using Nest;

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

builder.Services.Configure<ElasticSearchSettings>(
    builder.Configuration.GetSection("ElasticSearch"));

builder.Services.AddSingleton<IElasticClient>(sp =>
{
    var settings = sp.GetRequiredService<IOptions<ElasticSearchSettings>>().Value;

    var connectionSettings = new ConnectionSettings(new Uri(settings.Url))
        .DefaultIndex(settings.DefaultIndex);

    return new ElasticClient(connectionSettings);
});


//builder.WebHost.ConfigureKestrel(options =>
//{
//    options.ListenAnyIP(5000, listenOptions =>
//    {
//        listenOptions.Protocols = HttpProtocols.Http1AndHttp2;
//    });

//    options.ListenAnyIP(5001, listenOptions =>
//    {
//        listenOptions.UseHttps();
//        listenOptions.Protocols = HttpProtocols.Http1AndHttp2;
//    });
//});

var app = builder.Build();

//app.Urls.Add("http://0.0.0.0:5000");
//app.Urls.Add("https://0.0.0.0:5001");



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

app.UseRouting();
//call CustomMiddleware
app.UseLoggingMiddleware();

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapGrpcService<SMSService>();
});

app.MapControllers();



app.Run();

public partial class Program { }
