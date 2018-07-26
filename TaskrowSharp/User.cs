using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskrowSharp
{
    public class User
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

        public List<UserFunctionPeriod> UserFunctionPeriods { get; set; }

        public User(int userID, string fullName, string mainEmail, string userLogin, bool active, int appMainCompanyID, string userHashCode,
            string photoUrl, string approvalGroup, string profileTitle, List<UserFunctionPeriod> userFunctionPeriods)
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
            this.UserFunctionPeriods = userFunctionPeriods;
        }

        internal User(ApiModels.UserResponseApi userDetailResponseApi)
        {
            this.UserID = userDetailResponseApi.User.UserID;
            this.FullName = userDetailResponseApi.User.FullName;
            this.MainEmail = userDetailResponseApi.User.MainEmail;
            this.UserLogin = userDetailResponseApi.User.UserLogin;
            this.Active = !userDetailResponseApi.User.Inactive;
            this.AppMainCompanyID = userDetailResponseApi.User.AppMainCompanyID;
            this.UserHashCode = userDetailResponseApi.User.UserHashCode;
            this.ApprovalGroup = userDetailResponseApi.User.ApprovalGroup;
            this.ProfileTitle = userDetailResponseApi.User.ProfileTitle;

            this.UserFunctionPeriods = (userDetailResponseApi.User.UserFunctionPeriod != null ? userDetailResponseApi.User.UserFunctionPeriod.Select(a => new UserFunctionPeriod(a)).ToList() : new List<UserFunctionPeriod>());
        }
    }
}
