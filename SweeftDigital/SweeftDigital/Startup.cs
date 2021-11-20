using Core.Utilities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SweeftDigital
{
    public class Startup
    {

        readonly AppSettingsCollection AppSettings;

        public Startup(IWebHostEnvironment Env, IConfiguration configuration)
        {
            Configuration = configuration;
            if (Env.IsDevelopment())
            {
                var Builder = new ConfigurationBuilder().SetBasePath(Env.ContentRootPath).AddJsonFile("appsettings.json");
                AppSettings = new AppSettingsCollection(Builder.Build());
            }
            else
            {
#if DEBUG
                var Builder = new ConfigurationBuilder().SetBasePath(Env.ContentRootPath).AddJsonFile("appsettings.debug.json");
                AppSettings = new AppSettingsCollection(Builder.Build());
#else
                var Builder = new ConfigurationBuilder().SetBasePath(Env.ContentRootPath).AddJsonFile("appsettings.release.json");
                AppSettings = new AppSettingsCollection(Builder.Build());
                AppSettings.IsDevelopment = false;
#endif
            }
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(AppSettings);
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

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
