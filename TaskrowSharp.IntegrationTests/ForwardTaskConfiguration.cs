using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace TaskrowSharp.IntegrationTests
{
    [DataContract]
    public class ForwardTaskConfiguration
    {
        [DataMember(Name = "taskUrl")]
        public string TaskUrl { get; set; }

        [DataMember(Name = "user1Email")]
        public string User1Email { get; set; }

        [DataMember(Name = "user2Email")]
        public string User2Email { get; set; }
    }
}
