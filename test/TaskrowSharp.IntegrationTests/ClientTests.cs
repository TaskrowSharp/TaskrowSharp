using System;
using System.Linq;
using System.Threading.Tasks;
using TaskrowSharp.Exceptions;
using TaskrowSharp.IntegrationTests.TestModels;
using TaskrowSharp.Models;
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

        [Fact]
        public async Task InsertClientAsync()
        {
            if (_configurationFile.Users?.Count == 0)
                throw new System.InvalidOperationException("Error in configuration file, \"users\" list is empty");

            var userID = _configurationFile.Users.First();
            var clientName = $"TaskrowSharp_{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff}";

            var request = new InsertClientRequest(clientName, clientName, userID);
            var response = await _taskrowClient.InsertClientAsync(request);
            Assert.Equal(request.ClientName, response.Entity.ClientName);
        }
    }
}