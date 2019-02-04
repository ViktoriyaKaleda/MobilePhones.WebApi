using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MobilePhones.DataAccess;
using MobilePhones.Services;
using MobilePhones.Services.Models;
using MobilePhones.WebApi.Extensions;
using Swashbuckle.AspNetCore.Swagger;

namespace MobilePhones.WebApi
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Mobile phones API", Version = "v1" });
                c.EnableAnnotations();
            });

            MapperInitializer.MapperConfiguration();

            services.AddScoped<IImageService, ImageService>(service =>
                new ImageService(Configuration.GetValue<string>("ImagesFolderName")));

            services.AddScoped<IMobilePhoneService, MobilePhoneService>();

            services.AddScoped<IMobilePhonesJsonRepository, MobilePhonesRepository>(service =>
                new MobilePhonesRepository(Configuration.GetValue<string>("DataFilePath")));
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
                app.UseHsts();
            }

            app.ConfigureCustomExceptionMiddleware();

            app.UseStaticFiles();

            app.UseHttpsRedirection();
            app.UseMvc();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Web API V1");
                c.RoutePrefix = string.Empty;
            });
        }
    }
}
