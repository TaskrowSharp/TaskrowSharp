using System;
using System.Threading.Tasks;
using TaskrowSharp.IntegrationTests.TestModels;
using TaskrowSharp.Models;
using Xunit;

namespace TaskrowSharp.IntegrationTests
{
    public class InvoiceTests : BaseTest
    {
        private readonly TaskrowClient _taskrowClient;
        private readonly ConfigurationFile _configurationFile;

        public InvoiceTests()
        {
            _taskrowClient = GetTaskrowClient();
            _configurationFile = GetConfigurationFile();
        }

        [Fact]
        public async Task GetInvoiceAsync()
        {
            var invoiceIDs = _configurationFile.InvoiceIDs;
            if (invoiceIDs?.Count == null)
                throw new InvalidOperationException("Error in configuration file, \"invoices\" null");

            foreach (var invoiceID in invoiceIDs)
            {
                var invoiceDetail = await _taskrowClient.GetInvoiceDetailAsync(invoiceID);

                Assert.NotNull(invoiceDetail);
                Assert.Equal(invoiceID, invoiceDetail.InvoiceID);
            }
        }
    }
}