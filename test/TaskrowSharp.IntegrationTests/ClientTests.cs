using System.Threading.Tasks;
using TaskrowSharp.Exceptions;
using TaskrowSharp.IntegrationTests.TestModels;
using Xunit;

namespace TaskrowSharp.IntegrationTests
{
    public class ClientTests : BaseTest
    {
        private readonly TaskrowClient _taskrowClient;
        private readonly ConfigurationFile _configurationFile;

        public ClientTests()
        {
            _taskrowClient = GetTaskrowClient();
            _configurationFile = GetConfigurationFile();
        }

        [Fact]
        public async Task GetClientDetailAsync_Success()
        {
            var clients = _configurationFile.Clients;

            foreach (var clientID in clients)
            {
                var client = await _taskrowClient.GetClientDetailAsync(clientID);

                Assert.NotNull(client);
                Assert.Equal(clientID, client.Client.ClientID);
            }
        }

        [Fact]
        public async Task GetClientDetailAsync_NotFound()
        {
            var clientID = 0;

            TaskrowException exception = null;
            try
            {
                var client = await _taskrowClient.GetClientDetailAsync(clientID);
                Assert.NotNull(client);
            }
            catch (TaskrowException ex)
            {
                exception = ex;
            }
            Assert.NotNull(exception);
        }
    }
}