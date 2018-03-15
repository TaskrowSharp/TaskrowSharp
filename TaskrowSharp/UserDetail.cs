using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskrowSharp
{
    public class UserDetail
    {
        public int UserID { get; set; }

        public string FullName { get; set; }

        public string MainEmail { get; set; }

        public string UserLogin { get; set; }

        public bool Active { get; set; }

        public int AppMainCompanyID { get; set; }

        public string UserHashCode { get; set; }

        public string PhotoUrl { get { return string.Format("https://taskrowprod1pics.blob.core.windows.net/company{0}-150x150/{1}.jpg", this.AppMainCompanyID, this.UserHashCode); } }

        public string ApprovalGroup { get; set; }

        public string ProfileTitle { get; set; }

        public UserDetail(int userID, string fullName, string mainEmail, string userLogin, bool active, int appMainCompanyID, string userHashCode,
            string photoUrl, string approvalGroup, string profileTitle)
        {
            this.UserID = userID;
            this.FullName = fullName;
            this.MainEmail = mainEmail;
            this.UserLogin = userLogin;
            this.Active = active;
            this.AppMainCompanyID = appMainCompanyID;
            this.UserHashCode = userHashCode;
            this.ApprovalGroup = approvalGroup;
            this.ProfileTitle = profileTitle;
        }

        internal UserDetail(ApiModels.UserDetailResponse userDetailApi)
        {
            this.UserID = userDetailApi.User.UserID;
            this.FullName = userDetailApi.User.FullName;
            this.MainEmail = userDetailApi.User.MainEmail;
            this.UserLogin = userDetailApi.User.UserLogin;
            this.Active = !userDetailApi.User.Inactive;
            this.AppMainCompanyID = userDetailApi.User.AppMainCompanyID;
            this.UserHashCode = userDetailApi.User.UserHashCode;
            this.ApprovalGroup = userDetailApi.User.ApprovalGroup;
            this.ProfileTitle = userDetailApi.User.ProfileTitle;
        }
    }
}
