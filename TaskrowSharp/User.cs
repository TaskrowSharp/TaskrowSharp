using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TaskrowSharp
{
    public class User
    {
        public int UserID { get; set; }

        public string FullName { get; set; }

        public string MainEmail { get; set; }

        public string UserLogin { get; set; }

        public bool Inactive { get; set; }

        public int AppMainCompanyID { get; set; }

        public string UserHashCode { get; set; }

        public string PhotoUrl { get { return string.Format("https://taskrowprod1pics.blob.core.windows.net/company{0}-150x150/{1}.jpg", this.AppMainCompanyID, this.UserHashCode); } }

        public string ApprovalGroup { get; set; }

        public string ProfileTitle { get; set; }
      
    }
}
