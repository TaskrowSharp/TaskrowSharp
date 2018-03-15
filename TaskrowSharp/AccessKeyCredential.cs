using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskrowSharp
{
    public class AccessKeyCredential : Credential
    {
        public string AccessKey { get; private set; }

        public AccessKeyCredential(string mobileKey)
        {
            this.AccessKey = mobileKey;
        }

        public override void Validate()
        {
            if (string.IsNullOrEmpty(AccessKey))
                throw new System.ArgumentNullException("AccessKey is required");

            if (AccessKey.Length < 20)
                throw new System.ArgumentException("Invalid AccessKey");
        }
    }
}
