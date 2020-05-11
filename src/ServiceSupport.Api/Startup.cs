using System;
using System.Text;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using ServiceSupport.Infrastructure.IoC;
using ServiceSupport.Infrastructure.Services;
using ServiceSupport.Infrastructure.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using ServiceSupport.Infrastructure.CQRS;
using Microsoft.AspNetCore.SignalR;
using ServiceSupport.Infrastructure.CQRS.Shops;

namespace ServiceSupport.Api
{
    public class Startup
    {
        public IConfigurationRoot Configuration { get; }
        public IContainer ApplicationContainer { get; private set; }

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddAuthorization(x => x.AddPolicy("admin", p => p.RequireRole("admin")));
            services.AddMemoryCache();
            services.AddMvc()
                    .AddJsonOptions(x => x.SerializerSettings.Formatting = Formatting.Indented);

            services.AddMediatR(typeof(Startup));
            services.AddMediatR(typeof(GetShopsQueryHandler).Assembly);

            var builder = new ContainerBuilder();
            builder.Populate(services);
            builder.RegisterModule(new ContainerModule(Configuration));
            ApplicationContainer = builder.Build();

            services.Configure<JwtSettings>(Configuration.GetSection("jwt"));

            var provider = services.BuildServiceProvider();

            var jwtSettings = provider.GetService<JwtSettings>();

            services.AddAuthentication(options =>
            {
                options.DefaultScheme = IdentityConstants.ApplicationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = jwtSettings.Issuer,
                    ValidateAudience = false,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key)),
                };
            });

            return new AutofacServiceProvider(ApplicationContainer);
        }

        public void Configure(IApplicationBuilder app, IApplicationLifetime appLifetime)
        {
            var jwtSettings = app.ApplicationServices.GetService<JwtSettings>();

            app.UseAuthentication();

            if (jwtSettings.SeedData)
            {
                SeedData(app);
            }

            app.UseExceptionHandler("/Error");
            app.UseMvc();
            appLifetime.ApplicationStopped.Register(() => ApplicationContainer.Dispose());
        }

        private void SeedData(IApplicationBuilder app)
        {
            var dataInitializer = app.ApplicationServices.GetService<IDataInitializer>();
            dataInitializer.SeedAsync();
        }
    }
}
