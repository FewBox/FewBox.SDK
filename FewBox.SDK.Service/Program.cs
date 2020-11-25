using System;
using FewBox.SDK.Config;
using FewBox.SDK.Mail;

namespace FewBox.SDK.Service
{
    class Program
    {
        static void Main(string[] args)
        {
            var fewBoxSDKConfig = new FewBoxSDKConfig
            {
                MQ = new MQConfig
                {
                    HostName = "localhost",
                    Port = 31849,
                    UserName = "fewbox",
                    Password = "landpy",
                    Exchange = ""
                }
            };
            IMailService mailService = new MQMailService(fewBoxSDKConfig, new ColorConsoleLogger());
            mailService.ReceiveOpsNotification((mailMessage) =>
            {
                Console.WriteLine(mailMessage.Name);
            });
            Console.ReadLine();
        }
    }
}
