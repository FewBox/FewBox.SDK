using RabbitMQ.Client;
using FewBox.SDK.Config;
using System.Text;
using Microsoft.Extensions.Logging;
using FewBox.Core.Utility.Formatter;
using RabbitMQ.Client.Events;
using System;

namespace FewBox.SDK.Core
{
    abstract class MQService<T> where T : class
    {
        protected FewBoxSDKConfig FewBoxSDKConfig { get; set; }
        protected ILogger<MQService<T>> Logger { get; set; }
        protected MQService(FewBoxSDKConfig fewBoxSDKConfig, ILogger<MQService<T>> logger)
        {
            this.FewBoxSDKConfig = fewBoxSDKConfig;
            this.Logger = logger;
        }
        protected void Publish(string queue, T message)
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

                    var body = Encoding.UTF8.GetBytes(JsonUtility.Serialize<T>(message));

                    channel.BasicPublish(exchange: this.FewBoxSDKConfig.MQ.Exchange,
                             routingKey: RoutingKeys.Mail,
                             basicProperties: null,
                             body: body);
                    this.Logger.LogDebug("Sent {0}", message);
                }
            }
        }

        protected void Consume(string queue, Action<T> action)
        {
            var factory = new ConnectionFactory()
            {
                HostName = this.FewBoxSDKConfig.MQ.HostName,
                Port = this.FewBoxSDKConfig.MQ.Port,
                UserName = this.FewBoxSDKConfig.MQ.UserName,
                Password = this.FewBoxSDKConfig.MQ.Password
            };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: queue,
                             durable: false,
                             exclusive: false,
                             autoDelete: false,
                             arguments: null);

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);
                    T messageObject = JsonUtility.Deserialize<T>(message);
                    action(messageObject);
                    this.Logger.LogDebug("Received {0}", message);
                };
                channel.BasicConsume(queue: queue,
                             autoAck: true,
                             consumer: consumer);
                Console.ReadLine();
            }
        }
    }
}