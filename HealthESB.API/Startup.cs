using HealthESB.API.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HealthESB.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Identity;
using HealthESB.Framework.Utility;
using HealthESB.Framework.DependecyConfig;
using Microsoft.AspNetCore.Http;
using static HealthESB.API.Configuration.RequestResponseLoggingMiddleware;
using System.Diagnostics;
using HealthESB.ElasticSearch.IContracts;
using HealthESB.ElasticSearch.Implmentation;
using HealthESB.RabbitMQ.IContract;
using HealthESB.RabbitMQ.Implementation;
using HealthESB.Domain.IRepository;
using HealthESB.Persistance.Repository;

namespace HealthESB.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            ConfigureServices configureServices = new ConfigureServices(services);
            services.AddControllers();
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .SetIsOriginAllowed((host) => true)
                        .AllowCredentials());
            });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "HealthESB.API", Version = "v1" });
            });
            services.Configure<JwtConfig>(Configuration.GetSection("JwtConfig"));
            services.AddDbContext<HealthESBDbContext>(options => options.UseSqlServer(
               Configuration["ConnectionStrings:DefaultConnection"],
               optionsBuilder => optionsBuilder.MigrationsAssembly("HealthESB.API")));
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(jwt =>
                {
                    var key = Encoding.ASCII.GetBytes(Configuration["JwtConfig:Secret"]);

                    jwt.SaveToken = true;
                    jwt.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true, // this will validate the 3rd part of the jwt token using the secret that we added in the appsettings and verify we have generated the jwt token
                        IssuerSigningKey = new SymmetricSecurityKey(key), // Add the secret key to our Jwt encryption
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        RequireExpirationTime = false,
                        ValidateLifetime = true
                    };
                });

            // For saving Token 
            //services.AddAuthentication("Bearer")
            //  .AddJwtBearer("Bearer", options => options.SaveToken = true);
            //HttpContext.GetTokenAsync("Bearer", "access_token"); 
            //services.AddIdentity<IdentityUser, IdentityRole>()
            //    .AddEntityFrameworkStores<HealthESBDbContext>()
            //    .AddDefaultTokenProviders();

            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true).AddRoles<IdentityRole>()
                            .AddEntityFrameworkStores<HealthESBDbContext>();
            services.AddScoped<IElasticService, ElasticService>();
            services.AddScoped<IRabbitMqService, RabbitMqService>();
            services.AddScoped<IDapperRepository, DapperRepository>();

            services.AddSingleton<IConfiguration>(Configuration);
            Constants.TTAC_BaseUrl = Configuration["TTAC:BaseUrl"];
            Constants.TTAC_UserName = Configuration["TTAC:UserName"];
            Constants.TTAC_Password = Configuration["TTAC:Password"];
            Constants.TTAC_RegisterApiName = Configuration["TTAC:RegisterApiName"];
            Constants.TTAC_RegisterApiKey = Configuration["TTAC:RegisterApiKey"];
            Constants.TTAC_CheckApiKey = Configuration["TTAC:CheckApiKey"];
            Constants.TTAC_CheckApiName = Configuration["TTAC:CheckApiName"];
            Constants.TTAC_ReactiveApiName = Configuration["TTAC:ReactiveApiName"];
            Constants.TTAC_ReactiveApiKey = Configuration["TTAC:ReactiveApiKey"];
            Constants.TokenExpirationHours = int.Parse(Configuration["JwtConfig:TokenExpirationHours"]);
            Constants.TokenKey = Configuration["JwtConfig:Secret"];
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "HealthESB.API v1"));

                app.UseCors("CorsPolicy");
            }
            void RequestResponseHandler(RequestProfilerModel requestProfilerModel)
            {
                Debug.Print(requestProfilerModel.Request);
                Debug.Print(Environment.NewLine);
                Debug.Print(requestProfilerModel.Response);
            }
            app.UseMiddleware<RequestResponseLoggingMiddleware>((Action<RequestProfilerModel>)RequestResponseHandler);
            app.UseMiddleware<ExceptionMiddleware>();
            app.UseAuthentication();
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
