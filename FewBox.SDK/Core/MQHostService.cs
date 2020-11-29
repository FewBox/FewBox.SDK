using Microsoft.Extensions.Logging;
using System;
using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;
using System.Threading;

namespace FewBox.SDK.Core
{
    public abstract class MQHostService<T> : IHostedService where T : class
    {
        private IMQListenerService<T> MQListenerService { get; set; }
        private ILogger<MQHostService<T>> Logger { get; set; }

        public MQHostService(IMQListenerService<T> mqListenerService, ILogger<MQHostService<T>> logger)
        {
            this.MQListenerService = mqListenerService;
            this.Logger = logger;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            Task.Run(()=>{
                this.MQListenerService.Start(this.GetQueue(), this.GetFunc());
            });
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            this.MQListenerService.Stop();
            return Task.CompletedTask;
        }

        protected abstract string GetQueue();
        protected abstract Func<T, bool> GetFunc();
    }
}