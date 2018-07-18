using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using CatchMe.Core.Repository;
using CatchMe.Infrastructure.Repository;
using CatchMe.Infrastructure.Services;
using CatchMe.Infrastructure.Mapper;
using Newtonsoft.Json;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using CatchMe.Infrastructure.Settings;
using Microsoft.Extensions.Options;
using NLog.Extensions.Logging;
using NLog.Web;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using CatchMe.Api.Framework;


namespace CatchMe.Api
{
    public class Startup
    {

		public IConfigurationRoot Configuration { get; }
		public IContainer Container { get; private set; }

		public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            if (env.IsEnvironment("Development"))
            {
                // This will push telemetry data through Application Insights pipeline faster, allowing you to view results immediately.
                builder.AddApplicationInsightsSettings(developerMode: true);
            }

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        

        // This method gets called by the runtime. Use this method to add services to the container
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {

			//services.AddCors(options =>
			//{
			//	options.AddPolicy("CorsPolicy", x => x.AllowAnyOrigin());
			//	options.AddPolicy("CorsPolicy", xx => xx.AllowAnyMethod());
			//	options.AddPolicy("CorsPolicy", xxx => xxx.AllowAnyHeader());
			//	options.AddPolicy("CorsPolicy", xccx => xccx.AllowCredentials());

			//});

			// Add framework services.
			services.AddCors();
			services.AddApplicationInsightsTelemetry(Configuration);
			services.AddAuthorization(x=>x.AddPolicy("HasAdminRole", p => p.RequireRole("admin")));
			services.AddMvc()
				.AddJsonOptions(x => x.SerializerSettings.Formatting = Formatting.Indented);
			services.AddMemoryCache();
			//services.AddScoped<IEventRepository, EventRepository>();
			services.AddScoped<IUserRepository, UserRepository>();
			services.AddScoped<IEventService, EventService>();
			services.AddScoped<ISeatService, SeatService>();
			services.AddScoped<IUserService, UserService>();
			services.AddScoped<IDataInitializer, DataInitializer>();

			services.AddSingleton(AutoMapperConfig.Initialize());
			services.AddSingleton<IJwtHandler, JwtHandler>();
			services.Configure<JwtSettings>(Configuration.GetSection("jwt"));
			services.Configure<AppSettings>(Configuration.GetSection("app"));

			var builder = new ContainerBuilder();
			builder.Populate(services);
			builder.RegisterType<EventRepository>().As<IEventRepository>().InstancePerLifetimeScope();
			
			Container = builder.Build();
			return new AutofacServiceProvider(Container);
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline
		public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory,
			IApplicationLifetime appLifeTime)
		{
			//loggerFactory.AddConsole(Configuration.GetSection("Logging"));
			//loggerFactory.AddDebug();
			//loggerFactory.AddNLog();
			//app.AddNLogWeb();
			//env.ConfigureNLog("nlog.config");


			
			var jwtSettings = app.ApplicationServices.GetService<IOptions<JwtSettings>>();
			app.UseCors(options => options.AllowAnyHeader()
				.AllowAnyMethod()
				.AllowAnyOrigin()
				.AllowCredentials()
			);
			app.UseJwtBearerAuthentication(new JwtBearerOptions
				{ 
					AutomaticAuthenticate =true,
					TokenValidationParameters = new TokenValidationParameters
					{
						ValidIssuer = jwtSettings.Value.Issuer,
						ValidateAudience = false,
						IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Value.Key))
					}
				});
			app.UseApplicationInsightsRequestTelemetry();
			app.UseApplicationInsightsExceptionTelemetry();
			SeedData(app);
			app.UseErrorHandler();
			app.UseMvc();
			appLifeTime.ApplicationStopped.Register(() => Container.Dispose());



		}

		private void SeedData(IApplicationBuilder app)
		{
			var settings = app.ApplicationServices.GetService<IOptions<AppSettings>>();
			if(settings.Value.SeedData)
			{
				var dataInitialzer = app.ApplicationServices.GetService<IDataInitializer>();
				dataInitialzer.SeedAsync();
			}
		}
    }
}
