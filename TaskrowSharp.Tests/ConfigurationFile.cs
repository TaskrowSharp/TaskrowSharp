using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskrowSharp.Tests
{
    public class ConfigurationFile
    {
        [Newtonsoft.Json.JsonProperty("serviceUrl")]
        public string ServiceUrl { get; set; }

        [Newtonsoft.Json.JsonProperty("email")]
        public string Email { get; set; }

        [Newtonsoft.Json.JsonProperty("password")]
        public string Password { get; set; }
    }
}
