using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using TaskrowSharp.IntegrationTests.TestModels;
using TaskrowSharp.Models.InvoiceModels;
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

                var request = new InvoiceFeeInsertRequest(insertInvoiceData.JobNumber,
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

                var listInvoiceFees = await _taskrowClient.InvoiceFeeInsertAsync(request);
                
                var invoiceFee = listInvoiceFees.OrderByDescending(a => a.InvoiceFeeID).First();
                Debug.WriteLine($"InvoiceFee inserted successfully (invoiceFeeID={invoiceFee.InvoiceFeeID}, invoiceID={invoiceFee.InvoiceID})");

                //--- Get
                invoiceFee = await _taskrowClient.InvoiceFeeDetailGetAsync(invoiceFee.Job.JobNumber, invoiceFee.InvoiceFeeID);
                var invoice = (await _taskrowClient.InvoiceDetailGetAsync(invoiceFee.InvoiceID)).InvoiceDetail;
                Debug.WriteLine($"Invoice inserted successfully (invoiceID={invoice.InvoiceID})");

                //-- Cancel Invoice Bills
                foreach (var invoiceBill in invoice.InvoiceBill)
                {
                    var responseCancelInvoiceBill = await _taskrowClient.InvoiceBillCancelAsync(invoiceBill.InvoiceBillID);
                    Debug.WriteLine($"InvoiceBill Canceled (invoiceBillID={invoiceBill.InvoiceBillID}, invoiceID={invoice.InvoiceID})");
                }

                //--- Cancel Invoice
                var cancelInvoiceRequest = new InvoiceCancelRequest(invoiceFee.InvoiceID, "Inserted and canceled by TaskrowSharp", invoice.GuidModification);
                await _taskrowClient.InvoiceCancelAsync(cancelInvoiceRequest);
                Debug.WriteLine($"Invoice Canceled (invoiceID={invoice.InvoiceID})");

                Debug.WriteLine($"Invoice canceled (invoiceID={invoiceFee.InvoiceID})");

                Assert.NotEmpty(listInvoiceFees);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($">> ERROR: {ex.Message}");
                throw;
            }
        }
    }
}