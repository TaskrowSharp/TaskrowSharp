using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskrowSharp.IntegrationTests.TestModels;
using Xunit;

namespace TaskrowSharp.IntegrationTests
{
    public class ExternalDataTests : BaseTest
    {
        private readonly TaskrowClient _taskrowClient;
        private readonly ConfigurationFile _configurationFile;

        public ExternalDataTests()
        {
            _taskrowClient = GetTaskrowClient();
            _configurationFile = GetConfigurationFile();
        }

        [Fact]
        public async Task SaveExternalData_Success()
        {
            var clients = _configurationFile.Clients;
            var clientID = clients.First();

            var value1 = Guid.NewGuid().ToString();
            var value2 = Guid.NewGuid().ToString();

            var dic = new Dictionary<string, string>
            {
                { "value1", value1 },
                { "value2", value2 }
            };

            await _taskrowClient.SaveExternalDataAsync("taskrowsharp-tests", "client", clientID, dic);

            var dicRet = await _taskrowClient.GetExternalDataAsync("taskrowsharp-tests", "client", clientID);

            Assert.Equal(value1, dicRet["value1"]);
            Assert.Equal(value2, dicRet["value2"]);
        }
    }
}
