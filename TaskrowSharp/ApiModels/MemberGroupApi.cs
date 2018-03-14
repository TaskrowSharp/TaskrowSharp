using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskrowSharp.ApiModels
{
    internal class MemberGroupApi
    {
        public int UserID { get; set; }
        public string UserLogin { get; set; }
        public string UserHashCode { get; set; }
        public string FullName { get; set; }
        public int ProfileID { get; set; }
        public string ApprovalGroup { get; set; }
    }
}
