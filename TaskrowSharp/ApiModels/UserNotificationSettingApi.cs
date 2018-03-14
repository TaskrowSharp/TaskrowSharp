using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskrowSharp.ApiModels
{
    internal class UserNotificationSettingApi
    {
        public string MailNotifications { get; set; }
        public string InAppNotifications { get; set; }
        public string MobileNotifications { get; set; }
    }
}
