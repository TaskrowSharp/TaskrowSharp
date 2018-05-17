using System;
using System.Collections.Generic;
using System.Text;

namespace TaskrowSharp
{
    public class Owner
    {
        public int UserID { get; set; }

        public string UserName { get; set; }

        public string UserHashCode { get; set; }

        public Owner(int userID, string userName, string userHashCode)
        {
            this.UserID = userID;
            this.UserName = userName;
            this.UserHashCode = userHashCode;
        }
    }
}
