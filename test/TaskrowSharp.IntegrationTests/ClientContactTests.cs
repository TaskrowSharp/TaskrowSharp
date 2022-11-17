using System.Threading.Tasks;
using TaskrowSharp.IntegrationTests.TestModels;
using Xunit;

namespace TaskrowSharp.IntegrationTests
{
    public class ClientContactTests : BaseTest
    {
        private readonly TaskrowClient _taskrowClient;
        private readonly ConfigurationFile _configurationFile;

        public ClientContactTests()
        {
            _taskrowClient = GetTaskrowClient();
            _configurationFile = GetConfigurationFile();
        }

        [Fact]
        public async Task ListClientContactsAsync_Success()
        {
            var clients = _configurationFile.Clients;

            foreach (var clientID in clients)
            {
                var clientContacts = await _taskrowClient.ListClientContactsAsync(clientID);

                Assert.NotNull(clientContacts);
            }
        }
    }
}