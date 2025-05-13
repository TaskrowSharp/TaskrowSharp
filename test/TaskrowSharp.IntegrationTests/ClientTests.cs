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
        public async Task ClientListAsync_Success()
        {
            var response = await _taskrowClient.ClientListAsync();
            Assert.NotNull(response);
            Assert.NotEmpty(response.Items);
        }

        [Fact]
        public async Task ClientListAsync_IncludeInactives_Success()
        {
            var clients = new List<ClientListItem>();

            string? nextToken = null;
            string? lastNextToken = null;
            do
            {
                var response = await _taskrowClient.ClientListAsync(nextToken, includeInactives: true, convertNextTokenToNumeric: true);
                clients.AddRange(response.Items);
                nextToken = response.NextToken;

                if (string.IsNullOrWhiteSpace(nextToken))
                    throw new InvalidOperationException("nextToken retornado é null");

                if (lastNextToken != null && nextToken.Equals(lastNextToken))
                    throw new InvalidOperationException("Retorno está em loop retornando sempre o mesmo nextToken");

                lastNextToken = nextToken;

            } while (nextToken != null);

            Assert.NotNull(clients);
            var inactives = clients.Where(a => a.Inactive).ToList();
            Assert.NotEmpty(inactives);
        }

        [Fact]
        public async Task ClientSearchAsync_Success()
        {
            var term = "a";
            var response = await _taskrowClient.ClientSearchAsync(term);
            Assert.NotEmpty(response);
        }


        [Fact]
        public async Task ClientDetailGetAsync_Success()
        {
            List<int> clientIDs = _configurationFile.Clients?.Select(a => a.ClientID).ToList() ?? new List<int>();

            foreach (var clientID in clientIDs)
            {
                var client = await _taskrowClient.ClientDetailGetAsync(clientID);

                Assert.NotNull(client);
                Assert.Equal(clientID, client.ClientID);
            }
        }

        [Fact]
        public async Task ClientDetailGetAsync_NotFound()
        {
            var clientID = 0;

            TaskrowSharpException exception = null;
            try
            {
                var client = await _taskrowClient.ClientDetailGetAsync(clientID);
                Assert.NotNull(client);
            }
            catch (TaskrowSharpException ex)
            {
                exception = ex;
            }
            Assert.NotNull(exception);
        }

        [Fact]
        public async Task ClientInsertAsync_Success()
        {
            if (_configurationFile.UserIDs?.Count == 0)
                throw new System.InvalidOperationException("Error in configuration file, \"users\" list is empty");

            var userID = _configurationFile.UserIDs.First();
            var clientName = $"TaskrowSharp_{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff}";

            var request = new ClientInsertRequest(clientName, clientName, userID);
            var response = await _taskrowClient.ClientInsertAsync(request);
            Assert.NotNull(response);
            Assert.Equal(request.ClientName, response.ClientName);
        }
        
        [Fact]
        public async Task ClientUpdateAsync_Success()
        {
            List<int> clientIDs = _configurationFile.Clients?.Select(a => a.ClientID).ToList() ?? new List<int>();
            var clientID = clientIDs.First();

            var client = await _taskrowClient.ClientDetailGetAsync(clientID);
            
            var request = new ClientUpdateRequest(client)
            {
                Memo = $"Updated {DateTime.Now:yyyy-MM-dd HH:mm:ss}"
            };

            var response = await _taskrowClient.ClientUpdateAsync(request);
            Assert.NotNull(response);
            Assert.Equal(request.Memo, response.ClientAdministrativeDetail.Memo);
        }

        [Fact]
        public async Task ClientAddressInsert_or_UpdateAsync_Success()
        {
            List<int> clientIDs = _configurationFile.Clients?.Select(a => a.ClientID).ToList() ?? new List<int>();
            var clientID = clientIDs.First();
            var client = await _taskrowClient.ClientDetailGetAsync(clientID);

            string cnpj = "62.520.218/0001-24";
            string name = $"TaskrowSharp_{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff}";

            var clientAddress = client.ClientAddress.Where(a => a.CNPJ.Equals(cnpj)).FirstOrDefault();

            if (clientAddress == null)
            {
                var request = new ClientAddressInsertRequest(
                    clientID, 
                    client.ClientNickName, 
                    name, 
                    cnpj, 
                    null, 
                    31, 
                    7352, 
                    "SP", 
                    "SAO PAULO", 
                    "Av Paulista", 
                    "1578",
                    "Conjunto 123",
                    "01310-200",
                    "Bela Vista");

                var response = await _taskrowClient.ClientAddressInsertAsync(request);
                Assert.NotNull(response);
                Assert.Equal(request.SocialContractName, response.SocialContractName);
            }
            else
            {
                var request = new ClientAddressUpdateRequest(
                    clientID, 
                    clientAddress.ClientAddressID, 
                    client.ClientNickName, 
                    name, 
                    clientAddress.CNPJ,
                    clientAddress.CPF,
                    clientAddress.Country.CountryID,
                    clientAddress.CityID,
                    clientAddress.StateName,
                    clientAddress.CityName,
                    clientAddress.Street,
                    clientAddress.Number,
                    clientAddress.Complement,
                    clientAddress.ZipCode,
                    clientAddress.District);

                var response = await _taskrowClient.ClientAddressUpdateAsync(request);
                Assert.NotNull(response);
                Assert.Equal(request.SocialContractName, response.SocialContractName);
            }
        }
    }
}