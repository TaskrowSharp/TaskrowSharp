using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskrowSharp.ApiModels
{
    internal class UserApi
    {
        public int UserID { get; set; }
        public string FullName { get; set; }
        public string MainEmail { get; set; }
        public int PhotoVersion { get; set; }
        public string Extension { get; set; }
        public string MainPhoneNumber { get; set; }
        public object MainCellNumber { get; set; }
        public string UserLogin { get; set; }
        public string UserHashCode { get; set; }
        public int AppMainCompanyID { get; set; }
        public int AppMainLanguageID { get; set; }
        public bool Inactive { get; set; }
        public int ProfileID { get; set; }
        public string ExternalCode { get; set; }
        public string ProfileTitle { get; set; }
        public string UserFunctionTitle { get; set; }
        public bool NotBillable { get; set; }
        public bool NotRequiredTimesheet { get; set; }
        public string ApprovalGroup { get; set; }
        public bool ExternalUser { get; set; }
        public string RegistrationNumber { get; set; }
        public OfficeApi Office { get; set; }

        public List<UserFunctionPeriodApi> UserFunctionPeriod { get; set; }
    }
}
