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

namespace CatchMe.Api
{
    public class Startup
    {
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

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddApplicationInsightsTelemetry(Configuration);
			services.AddAuthorization(x=>x.AddPolicy("HasAdminRole", p => p.RequireRole("admin")));
			services.AddMvc()
				.AddJsonOptions(x => x.SerializerSettings.Formatting = Formatting.Indented);
			services.AddScoped<IEventRepository, EventRepository>();
			services.AddScoped<IUserRepository, UserRepository>();
			services.AddScoped<IEventService, EventService>();
			//services.AddScoped<ISeatService, SeatService>();
			services.AddScoped<IUserService, UserService>();
			services.AddSingleton(AutoMapperConfig.Initialize());
			services.AddSingleton<IJwtHandler, JwtHandler>();
			services.Configure<JwtSettings>(Configuration.GetSection("jwt"));
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline
		public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
		{
			loggerFactory.AddConsole(Configuration.GetSection("Logging"));
			loggerFactory.AddDebug();

			
			var jwtSettings = app.ApplicationServices.GetService<IOptions<JwtSettings>>();
			
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
			app.UseMvc();


        }
    }
}
