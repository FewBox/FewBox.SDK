using System;
using System.Text;
using FewBox.Core.Utility.Formatter;
using FewBox.SDK.Config;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace FewBox.SDK.Core
{
    public class MQListenerService<T> : IMQListenerService<T> where T : class
    {
        private FewBoxSDKConfig FewBoxSDKConfig { get; set; }
        private IConnection Connection { get; set; }
        private IModel Channel { get; set; }
        private ILogger<MQListenerService<T>> Logger { get; set; }

        public MQListenerService(FewBoxSDKConfig fewBoxSDKConfig, ILogger<MQListenerService<T>> logger)
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
        }
        
        public void Start(string queue, Func<T, bool> func)
        {
            this.Channel.QueueDeclare(queue: queue,
                             durable: false,
                             exclusive: false,
                             autoDelete: false,
                             arguments: null);

            var consumer = new EventingBasicConsumer(this.Channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                this.Logger.LogDebug("Received {0}", message);
                T messageObject = JsonUtility.Deserialize<T>(message);
                if (func(messageObject))
                {
                }

            };
            this.Channel.BasicConsume(queue: queue,
                         autoAck: true,
                         consumer: consumer);
        }

        public void Stop()
        {
            this.Connection.Close();
        }

        public void Dispose()
        {
            this.Stop();
        }
    }
}