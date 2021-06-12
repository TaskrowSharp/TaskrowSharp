using System.Linq;
using Xunit;

namespace TaskrowSharp.IntegrationTests
{
    public class UserDetailTests : BaseTest
    {
        private readonly TaskrowClient taskrowClient;

        public UserDetailTests()
        {
            taskrowClient = GetTaskrowClient();
        }

        [Fact]
        public void UserDetail_Get_FirstActive()
        {
            var listUsers = taskrowClient.ListUsers();
            var userTest = listUsers.Where(a => a.Active).First();

            var userDetail = taskrowClient.GetUser(userTest.UserID);
            Assert.True(userDetail != null);
            Assert.True(string.Equals(userTest.UserID, userDetail.UserID));
            Assert.True(string.Equals(userTest.MainEmail, userDetail.MainEmail));
        }

        [Fact]
        public void UserDetail_Get_FirstInactive()
        {
            var listUsers = taskrowClient.ListUsers();
            var userTest = listUsers.Where(a => !a.Active).FirstOrDefault();
            if (userTest == null)
                throw new System.InvalidOperationException($"No inactive users in {taskrowClient.ServiceUrl}");

            var userDetail = taskrowClient.GetUser(userTest.UserID);
            Assert.True(userDetail != null);
            Assert.True(string.Equals(userTest.UserID, userDetail.UserID));
            Assert.True(string.Equals(userTest.MainEmail, userDetail.MainEmail));
        }
    }
}
