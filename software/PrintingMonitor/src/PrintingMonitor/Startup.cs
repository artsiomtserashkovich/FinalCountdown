using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PrintingMonitor.Camera;
using PrintingMonitor.Identity;
using PrintingMonitor.Printer;
using PrintingMonitor.PrinterConnection;
using PrintingMonitor.ScheduledMonitoring;

namespace PrintingMonitor
{
    public class Startup
    {
        public Startup(IHostEnvironment environment)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(environment.ContentRootPath)
                .AddJsonFile("appsettings.json", false, true)
                .AddJsonFile($"appsettings.{environment.EnvironmentName}.json", true);

            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddRazorPages()
                .AddRazorPagesOptions(
                    options =>
                    {
                        options.Conventions.AuthorizePage("/Account/Login/Login");
                    });
            services.AddServerSideBlazor();

            services
                .AddIdentity(Configuration)
                .AddPrinterConnection(Configuration)
                .AddPrinterCoreSupport()
                .AddCameraSupport()
                .AddScheduledMonitoringSupport(Configuration);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/Main/Index");
            });
        }
    }
}
