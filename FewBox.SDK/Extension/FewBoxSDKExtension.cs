using FewBox.SDK.Auth;
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
        public static void AddFewBoxSDK(this IServiceCollection services, FewBoxIntegrationType fewboxIntegrationType,
        FewBoxListenerHostType fewBoxListenerHostType = FewBoxListenerHostType.None,
        FewBoxListenerType fewBoxListenerType = FewBoxListenerType.None)
        {
            IConfigurationBuilder builder = new ConfigurationBuilder()
            .AddJsonFile("./appsettings.json")
            .AddEnvironmentVariables();
            IConfigurationRoot configuration = builder.Build();
            var fewBoxSDKConfig = configuration.GetSection("FewBoxSDK").Get<FewBoxSDKConfig>();
            services.AddSingleton(fewBoxSDKConfig);
            services.AddScoped<ITryCatchService, TryCatchService>();
            services.AddScoped<ICredentialService, CredentialService>();
            if (fewboxIntegrationType == FewBoxIntegrationType.RestfulAPI)
            {
                services.AddScoped<IMailService, RestfulMailService>();
                services.AddScoped<IRealtimeService, RestfulRealtimeService>();
            }
            else if (fewboxIntegrationType == FewBoxIntegrationType.MessageQueue)
            {
                services.AddScoped<IMailService, MQMailService>();
                services.AddScoped<IPlanService, MQPlanService>();
                if (fewBoxListenerHostType == FewBoxListenerHostType.Web)
                {
                    if (fewBoxListenerType.HasFlag(FewBoxListenerType.Email))
                    {
                        services.AddSingleton<IMQListenerService<EmailMessage>, MQListenerService<EmailMessage>>();
                        services.AddHostedService<MQMailHostService>();
                    }
                    if (fewBoxListenerType.HasFlag(FewBoxListenerType.Plan))
                    {
                        services.AddSingleton<IMQListenerService<PlanMessage>, MQListenerService<PlanMessage>>();
                        services.AddHostedService<MQPlanHostService>();
                    }
                }
                else if (fewBoxListenerHostType == FewBoxListenerHostType.Console)
                {
                    // Do Nothing
                }
                else
                {
                    // Do Nothing
                }
            }
        }
    }
}