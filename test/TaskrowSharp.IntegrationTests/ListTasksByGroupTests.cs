using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace TaskrowSharp.IntegrationTests
{
    public class ListTasksByGroupTests : BaseTest
    {
        private readonly TaskrowClient _taskrowClient;

        public ListTasksByGroupTests()
        {
            _taskrowClient = GetTaskrowClient();
        }

        [Fact]
        public async Task Task_ListTasksByGroup_Success()
        {
            var groups = await _taskrowClient.ListGroupsAsync();
            var group = groups.First();

            var entity = await _taskrowClient.ListTasksByGroupAsync(group.GroupID);
            Assert.NotNull(entity);
        }
    }
}