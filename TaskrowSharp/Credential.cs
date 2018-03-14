using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskrowSharp
{
    /// <summary>
    /// Credential abastract class. To construct a new credential use: UserAndPasswordCredential or MobileApiCredential
    /// </summary>
    public abstract class Credential
    {
        internal Credential()
        {

        }

        public abstract void Validate();
    }
}