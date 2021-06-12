using Xunit;

namespace TaskrowSharp.IntegrationTests
{
    public class GroupListTests : BaseTest
    {
        private readonly TaskrowClient taskrowClient;

        public GroupListTests()
        {
            taskrowClient = GetTaskrowClient();
        }

        [Fact]
        public void GroupList_OK()
        {
            var listGroups = taskrowClient.ListGroups();
            Assert.True(listGroups.Count > 0);
        }
    }
}
