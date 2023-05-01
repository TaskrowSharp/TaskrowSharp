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
        public async Task CityList_Success()
        {
            var list = await _taskrowClient.ListCitiesAsync("SP");
            Assert.NotEmpty(list);
        }

        [Fact]
        public async Task GetCityByName_Success()
        {
            var stateAbbreviation = "SP";
            var cityName = Utils.Text.RemoveDiacritics("São Paulo").ToUpper();

            var city = await _taskrowClient.GetCityByName(stateAbbreviation, cityName);
            
            Assert.NotNull(city);
            Assert.Equal(stateAbbreviation, city.UF);
            Assert.Equal(cityName, city.Name);
        }
    }
}