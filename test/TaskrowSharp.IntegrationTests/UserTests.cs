using System.Threading.Tasks;
using Xunit;

namespace TaskrowSharp.IntegrationTests
{
    public class UserTests : BaseTest
    {
        private readonly TaskrowClient _taskrowClient;

        public UserTests()
        {
            _taskrowClient = GetTaskrowClient();
        }

        [Fact]
        public async Task UserList_Success()
        {
            var listUsers = await _taskrowClient.UserListAsync();
            Assert.NotEmpty(listUsers);
        }
    }
}