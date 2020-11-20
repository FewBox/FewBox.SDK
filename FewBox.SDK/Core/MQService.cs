using RabbitMQ.Client;
using FewBox.SDK.Config;
using System.Text;
using Microsoft.Extensions.Logging;
using FewBox.Core.Utility.Formatter;

namespace FewBox.SDK.Core
{
    abstract class MQService
    {
        private ITryCatchService TryCatchService { get; set; }
        protected ICredentialService CredentialService { get; set; }
        protected FewBoxSDKConfig FewBoxSDKConfig { get; set; }
        protected ILogger<MQService> Logger { get; set; }
        protected MQService(ITryCatchService tryCatchService, ICredentialService credentialService, FewBoxSDKConfig fewBoxSDKConfig, ILogger<MQService> logger)
        {
            this.TryCatchService = tryCatchService;
            this.CredentialService = credentialService;
            this.FewBoxSDKConfig = fewBoxSDKConfig;
            this.Logger = logger;
        }
        protected void PostInvoke(string queue, object message)
        {
            var factory = new ConnectionFactory()
            {
                HostName = this.FewBoxSDKConfig.MQ.HostName,
                Port = this.FewBoxSDKConfig.MQ.Port,
                UserName = this.FewBoxSDKConfig.MQ.UserName,
                Password = this.FewBoxSDKConfig.MQ.Password
            };
            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue: queue,
                             durable: false,
                             exclusive: false,
                             autoDelete: false,
                             arguments: null);

                    var body = Encoding.UTF8.GetBytes(JsonUtility.Serialize(message));

                    channel.BasicPublish(exchange: this.FewBoxSDKConfig.MQ.Exchange,
                             routingKey: RoutingKeys.Mail,
                             basicProperties: null,
                             body: body);
                    this.Logger.LogDebug("Sent {0}", message);
                }
            }
        }
    }
}