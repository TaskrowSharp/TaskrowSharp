using System.Linq;
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
        public async Task InsertInvoiceAsync()
        {
            var insertInvoiceData = _configurationFile.InsertInvoiceData;
            if (insertInvoiceData == null)
                throw new InvalidOperationException("Error in configuration file, \"insertInvoiceData\" null");

            var now = DateTime.Now.Date;
            var feePeriodDate = now.AddMonths(-1);
            var forecastDate = now;
            var dueDate = new DateTime(now.AddMonths(1).Date.Year, now.AddMonths(1).Month, 5);
            
            var request = new InsertInvoiceRequest(insertInvoiceData.JobNumber, 
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

            var response = await _taskrowClient.InsertInvoiceAsync(request);
            
            Assert.True(response.Success);
            Assert.NotEmpty(response.Entities);
        }
    }
}