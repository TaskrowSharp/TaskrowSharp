using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskrowSharp.ApiModels
{
    internal class UserRelationTypeApi
    {
        public int UserRelationTypeID { get; set; }
        public int AppMainCompanyID { get; set; }
        public string Name { get; set; }
        public int DailyMinutes { get; set; }
        public bool Inactive { get; set; }
        public bool AllowSendIncompleteTimesheet { get; set; }
        public bool ExternalUser { get; set; }
    }
}
