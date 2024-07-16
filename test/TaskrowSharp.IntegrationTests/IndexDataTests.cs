using System.Threading.Tasks;
using Xunit;

namespace TaskrowSharp.IntegrationTests
{
    public class IndexDataTests : BaseTest
    {
        private readonly TaskrowClient _taskrowClient;

        public IndexDataTests()
        {
            _taskrowClient = GetTaskrowClient();
        }

        [Fact]
        public async Task IndexDataGetAsync_Success()
        {
            var indexData = await _taskrowClient.IndexDataGetAsync();
            
            Assert.NotNull(indexData);
            Assert.NotEqual(0, indexData.InternalClient.AppMainCompanyID);
        }
    }
}