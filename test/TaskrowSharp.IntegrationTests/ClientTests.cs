using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskrowSharp.Exceptions;
using TaskrowSharp.IntegrationTests.TestModels;
using TaskrowSharp.Models.ClientModels;
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
        public async Task ListClientsAsync_Success()
        {
            var clients = await _taskrowClient.ListClientsAsync();
            Assert.NotNull(clients);
        }

        [Fact]
        public async Task ListClientsAsync_IncludeInactives_Success()
        {
            var clients = new List<ListClientItem>();

            string? nextToken = null;
            do
            {
                var response = await _taskrowClient.ListClientsAsync(nextToken, true);
                clients.AddRange(response.Items);
                nextToken = response.NextToken;
            } while (nextToken != null);

            Assert.NotNull(clients);
            var inactives = clients.Where(a => a.Inactive).ToList();
            Assert.NotEmpty(inactives);
        }

        [Fact]
        public async Task GetClientDetailAsync_Success()
        {
            List<int> clientIDs = _configurationFile.Clients?.Select(a => a.ClientID).ToList() ?? new List<int>();

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
        public async Task InsertClientAsync_Success()
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
        public async Task UpdateClientAsync_Success()
        {
            List<int> clientIDs = _configurationFile.Clients?.Select(a => a.ClientID).ToList() ?? new List<int>();
            var clientID = clientIDs.First();

            var clientDetail = await _taskrowClient.GetClientDetailAsync(clientID);
            var client = clientDetail.Client;

            var request = new UpdateClientRequest(client)
            {
                Memo = $"Updated {DateTime.Now:yyyy-MM-dd HH:mm:ss}"
            };

            var response = await _taskrowClient.UpdateClientAsync(request);
            Assert.True(response.Success);
            Assert.Equal(request.Memo, response.Entity.ClientAdministrativeDetail.Memo);
        }

        [Fact]
        public async Task SaveClientAddresAsync()
        {
            List<int> clientIDs = _configurationFile.Clients?.Select(a => a.ClientID).ToList() ?? new List<int>();
            var clientID = clientIDs.First();
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
                    clientAddress.ClientAddressID, 
                    client.Client.ClientNickName, 
                    name, 
                    clientAddress.CNPJ,
                    clientAddress.CPF,
                    clientAddress.Country.CountryID,
                    clientAddress.CityID ?? 0,
                    clientAddress.StateName,
                    clientAddress.CityName,
                    clientAddress.Street,
                    clientAddress.Number);

                var response = await _taskrowClient.UpdateClientAddressAsync(request);
                Assert.True(response.Success);
                Assert.Equal(request.SocialContractName, response.Entity.SocialContractName);
            }
        }
    }
}