using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using TaskrowSharp.IntegrationTests.TestModels;
using TaskrowSharp.Models;
using TaskrowSharp.Models.InvoiceModels;
using Xunit;
using static TaskrowSharp.IntegrationTests.TestModels.ConfigurationFile;

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
        public async Task InsertInvoiceFeeThenCancelAndDelete_Success()
        {
            try
            {
                var insertInvoiceData = _configurationFile.InsertInvoiceData;
                if (insertInvoiceData == null)
                    throw new InvalidOperationException("Error in configuration file, \"insertInvoiceData\" null");

                var now = DateTime.Now.Date;
                var feePeriodDate = now.AddMonths(-1);
                var forecastDate = now;
                var dueDate = new DateTime(now.AddMonths(1).Date.Year, now.AddMonths(1).Month, 5);

                //--- Insert

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

                var insertResponse = await _taskrowClient.InsertInvoiceFeeAsync(request);
                if (!insertResponse.Success)
                    throw new InvalidOperationException($"Error inserting invoice -- {insertResponse.Message}");


                var invoiceFee = insertResponse.Entities!.OrderByDescending(a => a.InvoiceFeeID).First();

                Debug.WriteLine($"InvoiceFee inserted successfully (invoiceFeeID={invoiceFee.InvoiceFeeID}, invoiceID={invoiceFee.InvoiceID})");


                //--- Get

                invoiceFee = await _taskrowClient.GetInvoiceFeeDetailAsync(invoiceFee.Job.JobNumber, invoiceFee.InvoiceFeeID);

                var invoice = (await _taskrowClient.GetInvoiceDetailAsync(invoiceFee.InvoiceID)).InvoiceDetail;


                //--- Cancel Invoice

                var cancelInvoiceRequest = new CancelInvoiceRequest(invoiceFee.InvoiceID, "Inserted and canceled by TaskrowSharp", invoice.GuidModification);
                await _taskrowClient.CancelInvoiceAsync(cancelInvoiceRequest);

                Debug.WriteLine($"Invoice canceled (invoiceID={invoiceFee.InvoiceID})");


                Assert.True(insertResponse.Success);
                Assert.NotEmpty(insertResponse.Entities);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($">> ERROR: {ex.Message}");
                throw;
            }
        }
    }
}