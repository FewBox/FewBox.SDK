using FewBox.SDK.Config;
using FewBox.SDK.Core;
using FewBox.SDK.Mail;
using FewBox.SDK.Realtime;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FewBox.SDK.Extension
{
    public static class FewBoxSDKExtension
    {
        public static void AddFewBoxSDK(this IServiceCollection services, FewBoxIntegrationType integrationType)
        {
            IConfigurationBuilder builder = new ConfigurationBuilder()
            .AddJsonFile("./appsettings.json")
            .AddEnvironmentVariables();
            IConfigurationRoot configuration = builder.Build();
            var fewBoxSDKConfig = configuration.GetSection("FewBoxSDK").Get<FewBoxSDKConfig>();
            services.AddSingleton(fewBoxSDKConfig);
            services.AddScoped<ITryCatchService, TryCatchService>();
            services.AddScoped<ICredentialService, CredentialService>();
            if (integrationType == FewBoxIntegrationType.RestfulAPI)
            {
                services.AddScoped<IMailService, RestfulMailService>();
            }
            else if (integrationType == FewBoxIntegrationType.MessageQueue)
            {
                services.AddScoped<IMailService, MQMailService>();
            }
            services.AddScoped<IRealtimeService, RestfulRealtimeService>();
        }
    }
}