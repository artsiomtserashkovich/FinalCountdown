using Akka.Actor;
using Blazor.FileReader;
using ControlTower.Printer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ControlTower
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            var printerStatus = new PrinterStatus();
            var actorSystem = ActorSystem.Create("Printer");

            var monitor = actorSystem.ActorOf(PrinterMonitor.Props(printerStatus),"printer-monitor");
            var printer = actorSystem.ActorOf(PrinterDevice.Props(monitor), "printer");

            services.AddSingleton(printerStatus);
            services.AddSingleton<IPrinterService>(new PrinterService(printer));

            services.AddServerSideBlazor().AddHubOptions(options =>
            {
                options.MaximumReceiveMessageSize = 1024 * 1024 * 30;
            });

            services.AddRazorPages();

            services.AddFileReaderService(options => { options.InitializeOnFirstCall = true; });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}
