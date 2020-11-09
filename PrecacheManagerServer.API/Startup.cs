using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using PrecacheManagerServer.API.Infrastructure;
using AutoMapper;
using PrecacheManagerServer.API.Models;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using PrecacheManagerServer.API.Services;
//using PrecacheManagerServer.BLL.Models;
//using PrecacheManagerServer.DAL.Models;

namespace PrecacheManagerServer
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



            //// Add framework services.
            //Mapper.Initialize(cfg => cfg.AddProfile<MappingProfile>());

            //services.AddAutoMapper();

            //https://stackoverflow.com/questions/40275195/how-to-set-up-automapper-in-asp-net-core
            //https://stackoverflow.com/questions/50411188/trying-to-add-automapper-to-asp-net-core-2
            services.AddAutoMapper(typeof(Startup));


            //https://stackoverflow.com/questions/50411188/trying-to-add-automapper-to-asp-net-core-2

            // Auto Mapper Configurations
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
                //mc.CreateMap(typeof(PlatformSettingRequestsModelAddOrUpdate<>), typeof(PlatformSettingsModelAddOrUpdate<>));
                //mc.CreateMap(typeof(PlatformSettingsModelAddOrUpdate<>), typeof(PlatformSettingRequestsModelAddOrUpdate<>));
            });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddCors();

            services.Configure<TokenManagement>(Configuration.GetSection("tokenManagement"));
            var token = Configuration.GetSection("tokenManagement").Get<TokenManagement>();
            var secret = Encoding.ASCII.GetBytes(token.Secret);

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                //this parts handles the validation of incomign jwts from incomign requests
                //https://developer.okta.com/blog/2018/03/23/token-authentication-aspnetcore-complete-guide
                //https://www.google.com/search?safe=strict&rlz=1C1GCEA_enGB883GB883&q=asp.net+core+2.2+jwt+authentication&sa=X&ved=2ahUKEwjAk7Pr9vDrAhWmaRUIHRbGARAQ1QIoAnoECA4QAw&biw=1920&bih=953#kpvalbx=_ULtjX-79L_aChbIP-qSrgA426
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,//va;idates the token with the jwt secret
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(token.Secret)),
                    ValidIssuer = token.Issuer,
                    ValidAudience = token.Audience,
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });


            services.AddScoped<ITokenAuthenticateService, TokenAuthenticationService>();

            services.AddScoped<IUserManagementService, UserManagementService>();

            services.AddScoped<IPlatformConfigService, PlatformConfigService>();

            services.AddSwaggerGen();

            Installer.ConfigureServices(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {

            loggerFactory.AddFile("Logs/PrecacheManager-{Date}.txt", minimumLevel: LogLevel.Information);


            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseCors(builder =>
                builder
                .WithOrigins("*")
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials()
            );
            app.UseSwagger();
            app.UseSwaggerUI(a =>
            {

                a.SwaggerEndpoint("/swagger/v1/swagger.json", "PrecacheManagerServer API");

            });

            app.UseAuthentication();//adds the authenticatrion middleware
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
