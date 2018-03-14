using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskrowSharp.ApiModels
{
    public class UserListApiResponse
    {
        public IList<UserApi> Users { get; set; }
        public PermissionsApi Permissions { get; set; }
        public object UsagePlanLimitUsers { get; set; }
    }
}
