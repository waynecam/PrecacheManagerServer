﻿using System;
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

            Installer.ConfigureServices(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
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



            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
