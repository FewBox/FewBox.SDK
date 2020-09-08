using System;
using System.Collections.Generic;
using System.Text;
using FewBox.SDK.Config;
using FewBox.SDK.Mail;
using Microsoft.Extensions.Logging;

namespace FewBox.SDK.Core
{
    class TryCatchService : ITryCatchService
    {
        private ILogger Logger { get; set; }
        private IMailService MailService { get; set; }
        private FewBoxSDKConfig FewBoxSDKConfig { get; set; }

        public TryCatchService(ILogger<TryCatchService> logger, IMailService mailService, FewBoxSDKConfig fewBoxSDKConfig)
        {
            this.Logger = logger;
            this.MailService = mailService;
            this.FewBoxSDKConfig = fewBoxSDKConfig;
        }
        public void Execute(Action action, bool isNeedNotification)
        {
            try
            {
                action();
            }
            catch (Exception exception)
            {
                StringBuilder exceptionDetail = new StringBuilder();
                this.BuildException(exceptionDetail, exception);
                this.Logger.LogError(exceptionDetail.ToString());
                if (isNeedNotification)
                {
                    this.MailService.OpsNotification(exception.Message, exceptionDetail.ToString(), new List<string> { this.FewBoxSDKConfig.OpsEmail });
                }
            }
        }

        private void BuildException(StringBuilder exceptionDetail, Exception exception)
        {
            exceptionDetail.AppendLine(exception.Message);
            exceptionDetail.AppendLine(exception.StackTrace);
            while (exception.InnerException != null)
            {
                exception = exception.InnerException;
                BuildException(exceptionDetail, exception);
            }
        }
    }
}