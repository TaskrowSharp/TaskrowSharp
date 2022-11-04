using System.Threading.Tasks;
using Xunit;

namespace TaskrowSharp.IntegrationTests
{
    public class CityListTests : BaseTest
    {
        private readonly TaskrowClient _taskrowClient;

        public CityListTests()
        {
            _taskrowClient = GetTaskrowClient();
        }

        [Fact]
        public async Task CityList_Success()
        {
            var list = await _taskrowClient.ListCitiesAsync("SP");
            Assert.NotEmpty(list);
        }
    }
}