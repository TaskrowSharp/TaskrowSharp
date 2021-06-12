using Xunit;

namespace TaskrowSharp.IntegrationTests
{
    public class UserListTests : BaseTest
    {
        private readonly TaskrowClient taskrowClient;

        public UserListTests()
        {
            taskrowClient = GetTaskrowClient();
        }

        [Fact]
        public void UserList_OK()
        {
            var listUsers = taskrowClient.ListUsers();
            Assert.True(listUsers.Count > 0);
        }
    }
}
