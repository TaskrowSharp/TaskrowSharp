using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace TaskrowSharp.Tests
{
    [DataContract]
    public class ConfigurationFile
    {
        [DataMember(Name = "serviceUrl")]
        public string ServiceUrl { get; set; }

        [DataMember(Name = "email")]
        public string Email { get; set; }

        [DataMember(Name = "password")]
        public string Password { get; set; }

        [DataMember(Name = "accessKey")]
        public string AccessKey { get; set; }
    }
}
