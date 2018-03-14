using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskrowSharp
{
    public class MobileApiCredential : Credential
    {
        public string MobileKey { get; private set; }

        public MobileApiCredential(string mobileKey)
        {
            this.MobileKey = mobileKey;
        }

        public override void Validate()
        {
            if (string.IsNullOrEmpty(MobileKey))
                throw new System.ArgumentNullException("MobileKey is required");
        }
    }
}
