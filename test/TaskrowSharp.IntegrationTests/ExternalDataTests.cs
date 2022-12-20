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
        private const string PROVIDER = "taskrowsharp-tests";
        private const string ENTITY_NAME = "client";
        private const string ENTITY_ID = "clientID";

        public ExternalDataTests()
        {
            _taskrowClient = GetTaskrowClient();
            _configurationFile = GetConfigurationFile();
        }

        [Fact]
        public async Task SaveExternalData_Success()
        {
            try
            {
                var clients = _configurationFile.ClientIDs;
                var clientID = clients.First();

                var value1 = Guid.NewGuid().ToString();
                var value2 = Guid.NewGuid().ToString();

                var dic = new Dictionary<string, object>
                {
                    { "value1", value1 },
                    { "value2", value2 }
                };

                await _taskrowClient.SaveExternalDataAsync(PROVIDER, ENTITY_NAME, clientID, dic);

                var dicGet = await _taskrowClient.GetExternalDataAsync(PROVIDER, ENTITY_NAME, clientID);

                Assert.Equal(value1, dicGet["value1"].ToString());
                Assert.Equal(value2, dicGet["value2"].ToString());

                var listFind = await _taskrowClient.SearchExternalDataByFieldValueAsync(PROVIDER, ENTITY_NAME, ENTITY_ID, "value1", value1);
                var dicFind = listFind.First();

                Assert.Equal(value1, dicFind["value1"].ToString());
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error in ExternalDataTest -- {ex.Message}", ex);
            }
        }
    }
}
