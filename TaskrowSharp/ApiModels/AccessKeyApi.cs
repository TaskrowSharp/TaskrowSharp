using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskrowSharp.ApiModels
{
    internal class AccessKeyApi
    {
        public int UserCredentialID { get; set; }
        public string Description { get; set; }
        public string MembershipDate { get; set; }
        public string IP { get; set; }
        public string Identifier { get; set; }
    }
}
