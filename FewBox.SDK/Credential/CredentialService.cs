using FewBox.SDK.Config;

namespace FewBox.SDK.Core
{
    class CredentialService : ICredentialService
    {
        private FewBoxSDKConfig FewBoxSDKConfig { get; set; }
        public CredentialService(FewBoxSDKConfig fewBoxSDKConfig)
        {
            this.FewBoxSDKConfig = fewBoxSDKConfig;
        }

        public Credential GetCredential()
        {
            return new Credential {  };
        }
    }
}