using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;
using Application;
using Database;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Serialization;

namespace ChamadoAPI
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
            
            services.AddCors(policy =>
            {
                policy.AddDefaultPolicy( opt => opt
                    .WithOrigins("http://170.246.105.250:9000","http://170.246.105.250:80")
                    .AllowAnyHeader()
                    .AllowAnyMethod());
            });
            services.AddControllers();
            services.AddRazorPages();
            services.AddDatabaseContext().AddServices();
            using var provider = services.BuildServiceProvider();
            var context = provider.GetRequiredService<Context>();
            context.Database.Migrate();
            services.AddDbContext<Context>(options => options.UseSqlServer(
                Configuration.GetConnectionString("DefaultConnection")));
            services.AddLogging();


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
            app.UseCors();
            app.UseAuthorization();
            app.UseResponseCaching();
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

        }
    }
}