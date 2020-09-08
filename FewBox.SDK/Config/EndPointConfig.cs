using System;

namespace FewBox.SDK.Config
{
    public class EndPointConfig
    {
        public string Protocol { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
        public string Version { get; set; }
        internal string UrlPrefix
        {
            get
            {
                string port = (this.Port == 80 || this.Port == 443) ? String.Empty : $":{this.Port}";
                string version = String.IsNullOrEmpty(this.Version) ? "v1alpha1" : this.Version;
                return $"{this.Protocol}://{this.Host}{port}/api/{version}";
            }
        }
    }
}