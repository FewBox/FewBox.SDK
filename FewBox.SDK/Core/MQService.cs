using RabbitMQ.Client;
using FewBox.SDK.Config;
using System.Text;
using Microsoft.Extensions.Logging;
using FewBox.Core.Utility.Formatter;

namespace FewBox.SDK.Core
{
    abstract class MQService<T> where T : class
    {
        protected FewBoxSDKConfig FewBoxSDKConfig { get; set; }
        protected ILogger<MQService<T>> Logger { get; set; }
        protected IConnection Connection { get; set; }
        protected IModel Channel { get; set; }
        protected MQService(FewBoxSDKConfig fewBoxSDKConfig, ILogger<MQService<T>> logger)
        {
            this.FewBoxSDKConfig = fewBoxSDKConfig;
            this.Logger = logger;
            var factory = new ConnectionFactory()
            {
                HostName = this.FewBoxSDKConfig.MQ.HostName,
                Port = this.FewBoxSDKConfig.MQ.Port,
                UserName = this.FewBoxSDKConfig.MQ.UserName,
                Password = this.FewBoxSDKConfig.MQ.Password
            };
            this.Connection = factory.CreateConnection();
            this.Channel = this.Connection.CreateModel();
            logger.LogDebug("[MQ Init] {0}:{1}", fewBoxSDKConfig.MQ.HostName, fewBoxSDKConfig.MQ.Port);
        }

        protected void Publish(string queue, T message)
        {
            this.Channel.QueueDeclare(queue: queue,
                             durable: false,
                             exclusive: false,
                             autoDelete: false,
                             arguments: null);
            string messageJson = JsonUtility.Serialize<T>(message);
            var body = Encoding.UTF8.GetBytes(messageJson);
            this.Channel.BasicPublish(exchange: this.FewBoxSDKConfig.MQ.Exchange,
                     routingKey: queue,
                     basicProperties: null,
                     body: body);
            this.Logger.LogDebug("[MQ Send] {0}:{1}:{2}", this.FewBoxSDKConfig.MQ.Exchange, queue, messageJson);
        }
    }
}