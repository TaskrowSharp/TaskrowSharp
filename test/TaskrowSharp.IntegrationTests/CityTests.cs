using System.Threading.Tasks;
using Xunit;

namespace TaskrowSharp.IntegrationTests
{
    public class CityTests : BaseTest
    {
        private readonly TaskrowClient _taskrowClient;

        public CityTests()
        {
            _taskrowClient = GetTaskrowClient();
        }

        [Fact]
        public async Task CityListAsync_Success()
        {
            var list = await _taskrowClient.CityListAsync("SP");
            Assert.NotEmpty(list);
        }

        [Fact]
        public async Task CityGetByNameAsync_Success()
        {
            var stateAbbreviation = "SP";
            var cityName = Utils.RemoveDiacritics("São Paulo").ToUpper();

            var city = await _taskrowClient.CityGetByNameAsync(stateAbbreviation, cityName);
            
            Assert.NotNull(city);
            Assert.Equal(stateAbbreviation, city.UF);
            Assert.Equal(cityName, city.Name);
        }
    }
}