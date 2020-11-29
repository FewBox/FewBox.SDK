using FewBox.SDK.Extension;
using FewBox.SDK.Mail;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FewBox.SDK.Service
{
    public class Startup
    {
        public IConfigurationRoot Configuration { get; private set; }

        public Startup()
        {
            IConfigurationBuilder builder = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", false, true)
            .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddFewBoxSDK(FewBoxIntegrationType.MessageQueue, FewBoxListenerHostType.Console);
            services.AddLogging();
            services.AddScoped<IMQMailHandler, TestMQMailHandler>();
        }
    }
}