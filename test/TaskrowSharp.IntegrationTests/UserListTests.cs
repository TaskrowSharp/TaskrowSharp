using System.Threading.Tasks;
using Xunit;

namespace TaskrowSharp.IntegrationTests
{
    public class UserListTests : BaseTest
    {
        private readonly TaskrowClient _taskrowClient;

        public UserListTests()
        {
            _taskrowClient = GetTaskrowClient();
        }

        [Fact]
        public async Task UserList_Success()
        {
            var listUsers = await _taskrowClient.ListUsersAsync();
            Assert.NotEmpty(listUsers);
        }
    }
}