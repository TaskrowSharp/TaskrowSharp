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
            var clientIDs = _configurationFile.ClientIDs;

            foreach (var clientID in clientIDs)
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
            if (_configurationFile.UserIDs?.Count == 0)
                throw new System.InvalidOperationException("Error in configuration file, \"users\" list is empty");

            var userID = _configurationFile.UserIDs.First();
            var clientName = $"TaskrowSharp_{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff}";

            var request = new InsertClientRequest(clientName, clientName, userID);
            var response = await _taskrowClient.InsertClientAsync(request);
            Assert.True(response.Success);
            Assert.Equal(request.ClientName, response.Entity.ClientName);
        }

        [Fact]
        public async Task SaveClientAddresAsync()
        {
            var clientID = _configurationFile.ClientIDs.First();
            var client = await _taskrowClient.GetClientDetailAsync(clientID);

            string cnpj = "62.520.218/0001-24";
            string name = $"TaskrowSharp_{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff}";

            var clientAddress = client.Client.ClientAddress.Where(a => a.CNPJ.Equals(cnpj)).FirstOrDefault();

            if (clientAddress == null)
            {
                var request = new InsertClientAddressRequest(
                    clientID, 
                    client.Client.ClientNickName, 
                    name, 
                    cnpj, 
                    null, 
                    31, 
                    7352, 
                    "SP", 
                    "SAO PAULO", 
                    "Av Paulista", 
                    "1578");

                var response = await _taskrowClient.InsertClientAddressAsync(request);
                Assert.True(response.Success);
                Assert.Equal(request.SocialContractName, response.Entity.SocialContractName);
            }
            else
            {
                var request = new UpdateClientAddressRequest(
                    clientID, 
                    client.Client.ClientNickName, 
                    clientAddress.ClientAddressID, 
                    name, 
                    clientAddress.CNPJ,
                    clientAddress.CPF,
                    clientAddress.Country.CountryID,
                    clientAddress.CityID ?? 0,
                    clientAddress.StateName,
                    clientAddress.CityName,
                    clientAddress.Street,
                    clientAddress.Number ?? "0");

                var response = await _taskrowClient.UpdateClientAddressAsync(request);
                Assert.True(response.Success);
                Assert.Equal(request.SocialContractName, response.Entity.SocialContractName);
            }
        }
    }
}