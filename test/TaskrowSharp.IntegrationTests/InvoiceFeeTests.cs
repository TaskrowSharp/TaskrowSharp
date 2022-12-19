using System;
using System.Threading.Tasks;
using TaskrowSharp.IntegrationTests.TestModels;
using TaskrowSharp.Models;
using Xunit;

namespace TaskrowSharp.IntegrationTests
{
    public class InvoiceFeeTests : BaseTest
    {
        private readonly TaskrowClient _taskrowClient;
        private readonly ConfigurationFile _configurationFile;

        public InvoiceFeeTests()
        {
            _taskrowClient = GetTaskrowClient();
            _configurationFile = GetConfigurationFile();
        }

        [Fact]
        public async Task InsertInvoiceFeeAsync()
        {
            var insertInvoiceData = _configurationFile.InsertInvoiceData;
            if (insertInvoiceData == null)
                throw new InvalidOperationException("Error in configuration file, \"insertInvoiceData\" null");

            var now = DateTime.Now.Date;
            var feePeriodDate = now.AddMonths(-1);
            var forecastDate = now;
            var dueDate = new DateTime(now.AddMonths(1).Date.Year, now.AddMonths(1).Month, 5);
            
            var request = new InsertInvoiceFeeRequest(insertInvoiceData.JobNumber, 
                feePeriodDate.Month, feePeriodDate.Year,
                forecastDate.Month, forecastDate.Year,
                insertInvoiceData.FromClientAddressID,
                insertInvoiceData.ToClientAddressID,
                0, 
                0, 
                1.20M,
                dueDate,
                1,
                insertInvoiceData.UserSigningDocument,
                "Serviços TaskrowSharp Test",
                null);

            var response = await _taskrowClient.InsertInvoiceFeeAsync(request);
            
            Assert.True(response.Success);
            Assert.NotEmpty(response.Entities);
        }

        [Fact]
        public async Task GetInvoiceFeeAsync()
        {
            var invoiceReferences = _configurationFile.InvoiceFees;
            if (invoiceReferences?.Count == null)
                throw new InvalidOperationException("Error in configuration file, \"invoices\" null");

            foreach (var invoiceReference in invoiceReferences)
            {
                var invoice = await _taskrowClient.GetInvoiceFeeDetailAsync(invoiceReference.JobNumber, invoiceReference.InvoiceFeeID);

                Assert.NotNull(invoice);
                Assert.Equal(invoiceReference.InvoiceFeeID, invoice.InvoiceFeeID);
            }
        }
    }
}