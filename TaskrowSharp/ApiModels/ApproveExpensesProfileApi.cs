using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskrowSharp.ApiModels
{
    internal class ApproveExpensesProfileApi
    {
        public int AppMainCompanyID { get; set; }
        public int ProfileID { get; set; }
        public int ProfileRate { get; set; }
        public string ProfileTitle { get; set; }
        public bool BuiltInUser { get; set; }
        public string PermissionListString { get; set; }
        public bool Inactive { get; set; }
        public object ExternalUser { get; set; }
        public IList<object> Permission { get; set; }
        public IList<object> Users { get; set; }
    }
}
