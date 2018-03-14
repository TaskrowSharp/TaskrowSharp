using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskrowSharp.ApiModels
{
    internal class UserFunctionApi
    {
        public int UserFunctionID { get; set; }
        public string UserFunctionTitle { get; set; }
        public object DailyMinutes { get; set; }
        public bool NotBillable { get; set; }
        public bool NotRequiredTimesheet { get; set; }
        public int FunctionGroupID { get; set; }
        public string GroupName { get; set; }
        public string FullFunctionName { get; set; }
    }
}
