using System;

namespace FewBox.SDK.Config
{
    public class MQConfig
    {
        public string HostName { get; set; }
        public int Port { get; set; }
        public string Version { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Exchange { get; set; }
    }
}