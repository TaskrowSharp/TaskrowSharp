using System.Linq;
using System;
using System.Threading.Tasks;
using TaskrowSharp.IntegrationTests.TestModels;
using TaskrowSharp.Models;
using Xunit;
using TaskrowSharp.Models.ClientModels;
using System.Collections.Generic;

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
            List<int> clientIDs = _configurationFile.Clients?.Select(a => a.ClientID).ToList() ?? new List<int>();

            foreach (var clientID in clientIDs)
            {
                var clientContacts = await _taskrowClient.ListClientContactsAsync(clientID);

                Assert.NotNull(clientContacts);
            }
        }

        [Fact]
        public async Task InsertClientContactAsync()
        {
            List<int> clientIDs = _configurationFile.Clients?.Select(a => a.ClientID).ToList() ?? new List<int>();
            var clientID = clientIDs.FirstOrDefault();

            if (clientID == 0)
                throw new System.InvalidOperationException("Error in configuration file, \"clients\" list is empty");
            var client = await _taskrowClient.GetClientDetailAsync(clientID);
            
            var contactName = $"Teste TaskrowSharp {DateTime.Now:yyyy-MM-dd HH:mm:ss.fff}";

            var request = new InsertClientContactRequest(clientID, client.Client.ClientName, contactName)
            {
                ContactEmail = $"{Guid.NewGuid()}@email.com",
                ContactMainPhone = "+55 (11) 6666-6666",
                ContactCellPhone = "+55 (11) 9999-9999",
                OfficeArea = "12",
                FunctionName = "Gerente",
                BirthDay = 12,
                BirthMonth = 7,
                ContactInfo = "Contato criado pelo teste do TaskrowSharp",
                IsMainContact = false,
                IsFinancialDocument = false,
                Inactive = true
            };

            var response = await _taskrowClient.InsertClientContactAsync(request);
            
            Assert.Equal(request.ContactName, response.Entity.ContactName);
        }
    }
}