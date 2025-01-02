using FluentValidation;
using Ganz.Application.Interfaces;
using Ganz.Application.Interfaces.Catalogs.Features;
using Ganz.Application.Services;
using Ganz.Application.Services.Catalogs.Features;
using Ganz.Application.Utilities;
using Ganz.Application.Validators;
using Ganz.Domain;
using Ganz.Domain.Catalogs.Features;
using Ganz.Domain.Contracts;
using Ganz.Infrastructure.Persistence;
using Ganz.Infrastructure.Persistence.Catalogs.Features;
using Ganz.Infrastructure.Utilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Ganz.Infrastructure
{
    public static class DependencyRegistrar
    {
        public static void RegisterServices(this IServiceCollection services, IConfiguration configuration)
        {
            string connectionstring = configuration.GetConnectionString("SqlConnection")!;

            services.AddDbContext<ApplicationDBContext>(options =>
            {
                options.UseSqlServer(connectionstring);
            });

            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<IPermissionRepository, PermissionRepository>();
            services.AddScoped<IPermissionService, PermissionService>();
            services.AddScoped<IFeatureService, FeatureService>();
            services.AddScoped<IFeatureRepository, FeatureRepository>();
            
            services.AddHttpContextAccessor();
            services.AddScoped<IMyFileUtility, MyFileUtility>();


            services.AddSingleton<EncryptionUtility>();
            services.Configure<Configs>(configuration.GetSection("Configs"));

            services.AddGrpc();


            var config = new AutoMapper.MapperConfiguration(options =>
            {
                options.AddProfile(new Ganz.Application.AutoMapper.AutoMapperConfig());
            });
            var mapper = config.CreateMapper();
            services.AddSingleton(mapper);

            services.AddValidatorsFromAssemblyContaining<ProductDtoValidator>();


            


            //services.AddSwaggerGen(options =>
            //{
            //    options.SwaggerDoc("v1", new OpenApiInfo { Title = "OnlineShop", Version = "v1" });
            //    options.EnableAnnotations();
            //});


            //Interceptor
            //var proxyGenerator = new ProxyGenerator();
            //services.AddScoped<IProductService>(provider =>
            //{
            //    var productDependency = provider.GetRequiredService<IProductRepository>();

            //    var productService = new ProductService(productDependency);
            //    var interceptor = new LoggingInterceptor();
            //    return proxyGenerator.CreateInterfaceProxyWithTarget<IProductService>(productService, interceptor);
            //});

            //services.Decorate<IProductService, ProductServiceLoggingDecorator>();

        }
    }
}
