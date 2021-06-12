using Xunit;

namespace TaskrowSharp.IntegrationTests
{
    public class IndexDataTests : BaseTest
    {
        private readonly TaskrowClient taskrowClient;

        public IndexDataTests()
        {
            taskrowClient = GetTaskrowClient();
        }

        [Fact]
        public void IndexData_Get()
        {
            var indexData = taskrowClient.GetIndexData();
            Assert.True(indexData.Photo != null);
        }
    }
}
