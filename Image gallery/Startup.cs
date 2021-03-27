using Image_gallery.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.IO;

namespace Image_gallery
{
    public class Startup
    {

        public IConfiguration Configuration { get; private set; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                 options.UseNpgsql(Configuration.GetConnectionString("ImageGalleryDB")));
            services.AddCors();
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseRouting();

            //
            app.UseCors(
            options => options.WithOrigins("http://localhost:4200").AllowAnyHeader().AllowAnyMethod()
     );


            app.UseEndpoints(endpoints =>
             {
                 endpoints.MapControllers();
             });

            app.Run(async (contex) =>
            {
                await contex.Response.WriteAsync("Could not find anything");
            });
        }
    }
}
