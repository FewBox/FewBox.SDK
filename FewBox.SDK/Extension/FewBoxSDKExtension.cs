using FewBox.SDK.Config;
using FewBox.SDK.Core;
using FewBox.SDK.Mail;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FewBox.SDK.Extension
{
    public static class FewBoxSDKExtension
    {
        public static void AddFewBoxSDK(this IServiceCollection services)
        {
            IConfigurationBuilder builder = new ConfigurationBuilder()
            .AddJsonFile("./appsettings.json")
            .AddEnvironmentVariables();
            IConfigurationRoot configuration = builder.Build();
            var fewBoxSDKConfig = configuration.GetSection("FewBoxSDK").Get<FewBoxSDKConfig>();
            services.AddSingleton(fewBoxSDKConfig);
            services.AddScoped<ITryCatchService, TryCatchService>();
            services.AddScoped<ICredentialService, CredentialService>();
            services.AddScoped<IMailService, MailService>();
        }
    }
}