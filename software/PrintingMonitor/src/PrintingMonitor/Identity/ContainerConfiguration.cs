using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace PrintingMonitor.Identity
{
    public static class ContainerConfiguration
    {
        public static IServiceCollection AddIdentity(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddDefaultIdentity<ApplicationUser>()
                .AddUserStore<OptionsBasedPasswordOptionsUserStore>();

            services
                .AddOptions()
                .Configure<SecurityOptions>(configuration.GetSection("SecurityOptions"));

            services.AddTransient<IUserPasswordStore<ApplicationUser>, OptionsBasedPasswordOptionsUserStore>();


            return services;
        }
    }
}
