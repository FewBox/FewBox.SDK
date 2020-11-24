using System.Collections.Generic;

namespace FewBox.SDK.Core
{
    public class EmailMessage
    {
        public string Name { get; set; }
        public string Content { get; set; }
        public IList<string> ToAddresses { get; set; }
    }
}