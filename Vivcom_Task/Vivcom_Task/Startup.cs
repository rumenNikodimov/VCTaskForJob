using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Vivcom_Task.Hubs;

namespace Vivcom_Task
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddSignalR();

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
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseSignalR(routes =>
            {
                routes.MapHub<SearchingHub>("/searchingHub");
            });

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });


        //    var directories = this.GetDirectory();

        //    foreach (var dir in directories)
        //    {
        //        var filesPath = Directory.GetFiles(dir);

        //        foreach (var file in filesPath)
        //        {
        //            string fileContent = default(string);

        //            try
        //            {
        //                fileContent = File.ReadAllText(file);

        //            }
        //            catch (Exception ex)
        //            {
        //                throw ex;
        //            }
        //        }

        //        // Do something with file content

        //        // Uncomment to save file if necessary
        //        // File.WriteAllText(fileWithModifiedContent)
        //    }
        //}

        //private IEnumerable<string> GetDirectory()
        //{
        //    var projectDirectory = Directory.GetCurrentDirectory();
        //    var logFilesDirectoryRelativePath = @"\Logs";

        //    var fullPath = projectDirectory + logFilesDirectoryRelativePath;

        //    IEnumerable<string>
        //        dirs = new List<string>
        //            (Directory.EnumerateDirectories(fullPath));

        //    return dirs;
        }
    }
}
