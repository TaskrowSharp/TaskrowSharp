using System.Threading.Tasks;
using Xunit;

namespace TaskrowSharp.IntegrationTests
{
    public class GroupListTests : BaseTest
    {
        private readonly TaskrowClient _taskrowClient;

        public GroupListTests()
        {
            _taskrowClient = GetTaskrowClient();
        }

        [Fact]
        public async Task GroupList_Success()
        {
            var listGroups = await _taskrowClient.ListGroupsAsync();
            Assert.NotEmpty(listGroups);
        }
    }
}