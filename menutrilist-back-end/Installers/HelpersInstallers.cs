using Menutrilist.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Menutrilist.Installers
{
    public class HelpersInstallers : IServiceInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpClient();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IFatSecretService, FatSecretService>();
        }
    }
}