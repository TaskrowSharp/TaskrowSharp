using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace TaskrowSharp.IntegrationTests
{
    public class UserDetailTests : BaseTest
    {
        private readonly TaskrowClient _taskrowClient;

        public UserDetailTests()
        {
            _taskrowClient = GetTaskrowClient();
        }

        [Fact]
        public async Task UserDetail_Get_FirstActive()
        {
            var listUsers = await _taskrowClient.ListUsersAsync();
            var user = listUsers.Where(a => !a.Inactive).First();

            var userDetail = await _taskrowClient.GetUserDetailAsync(user.UserID);

            Assert.NotNull(userDetail);
            Assert.Equal(user.UserID, userDetail.User.UserID);
            Assert.Equal(user.MainEmail, userDetail.User.MainEmail);
        }

        [Fact]
        public async Task UserDetail_Get_FirstInactive()
        {
            var listUsers = await _taskrowClient.ListUsersAsync();
            var user = listUsers.Where(a => !a.Inactive).FirstOrDefault();
            if (user == null)
                throw new System.InvalidOperationException($"No inactive users in {_taskrowClient.ServiceUrl}");

            var userDetail = await _taskrowClient.GetUserDetailAsync(user.UserID);

            Assert.NotNull(userDetail);
            Assert.Equal(user.UserID, userDetail.User.UserID);
            Assert.Equal(user.MainEmail, userDetail.User.MainEmail);
        }
    }
}