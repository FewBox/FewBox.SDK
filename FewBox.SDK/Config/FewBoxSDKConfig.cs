namespace FewBox.SDK.Config
{
    public class FewBoxSDKConfig
    {
        public string OpsEmail { get; set; }
        public CredentialConfig CredentialConfig { get; set; }
        public EndPointConfig MailEndPoint { get; set; }
        public EndPointConfig RealtimeEndPoint { get; set; }
        public MQConfig MQ { get; set; }
    }
}