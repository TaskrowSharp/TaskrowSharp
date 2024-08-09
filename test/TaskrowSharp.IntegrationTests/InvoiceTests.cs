using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskrowSharp.IntegrationTests.TestModels;
using Xunit;
using System.Diagnostics;
using TaskrowSharp.Models.InvoiceModels;

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
        public async Task InsertInvoiceThenCancelAndDelete_Success()
        {
            try
            {
                var now = DateTime.Now;
                var today = now.Date;

                var jobNumber = _configurationFile.InsertInvoiceData.JobNumber;
                var fromClientAddressID = _configurationFile.InsertInvoiceData.FromClientAddressID;
                var toClientAddressID = _configurationFile.InsertInvoiceData.ToClientAddressID;
                var userSigningDocument = _configurationFile.InsertInvoiceData.UserSigningDocument;

                var feePeriodDate = today.AddMonths(-1);
                var forecastDate = today;
                var dueDate = new DateTime(today.AddMonths(1).Date.Year, today.AddMonths(1).Month, 5);
                var issueDate = today;

                int invoiceServiceCodeID = 114;

                var invoice1Value = 1.25M;
                var invoice2Value = 2.25M;
                var invoiceTotal = invoice1Value + invoice2Value;

                var bill1Value = invoiceTotal / 2;
                var bill2Value = invoiceTotal - bill1Value;
                var bill1DueDate = dueDate;
                var bill2DueDate = bill1DueDate.AddMonths(1);

                var financialAccountID = 82;


                //-- req 100 - Cria faturamento 1
                var invoiceFeeRequest1 = new InvoiceFeeInsertRequest(
                    jobNumber: jobNumber,
                    feeMonth: feePeriodDate.Month,
                    feeYear: feePeriodDate.Year,
                    invoiceForecastMonth: forecastDate.Month,
                    invoiceForecastYear: forecastDate.Year,
                    fromClientAddressID: fromClientAddressID,
                    toClientAddressID: toClientAddressID,
                    monthRevenueValue: 0,
                    monthExpectedValue: 0,
                    invoiceValue: invoice1Value,
                    dueDate: dueDate,
                    invoiceStatusID: 1,
                    userSigningDocument: userSigningDocument,
                    description: "Serviços IntegracaoOmie Test",
                    administrativeNotes: null);

                var listInvoiceFeees1 = await _taskrowClient.InvoiceFeeInsertAsync(invoiceFeeRequest1);
                
                var invoiceFee1 = listInvoiceFeees1.OrderByDescending(a => a.InvoiceFeeID).First();

                Debug.WriteLine($"InvoiceFee inserted (invoiceFeeID={invoiceFee1.InvoiceFeeID}, invoiceID={invoiceFee1.InvoiceID})");


                //-- Obtém invoice1
                var invoice1 = (await _taskrowClient.InvoiceDetailGetAsync(invoiceFee1.InvoiceID)).InvoiceDetail;


                //-- req 200 - Cria faturamento 2
                var invoiceFeeRequest2 = new InvoiceFeeInsertRequest(
                    jobNumber: jobNumber,
                    feeMonth: feePeriodDate.Month,
                    feeYear: feePeriodDate.Year,
                    invoiceForecastMonth: forecastDate.Month,
                    invoiceForecastYear: forecastDate.Year,
                    fromClientAddressID: fromClientAddressID,
                    toClientAddressID: toClientAddressID,
                    monthRevenueValue: 0,
                    monthExpectedValue: 0,
                    invoiceValue: invoice2Value,
                    dueDate: dueDate,
                    invoiceStatusID: 1,
                    userSigningDocument: userSigningDocument,
                    description: "Serviços IntegracaoOmie Test",
                    administrativeNotes: null);

                var listInvoiceFees2 = await _taskrowClient.InvoiceFeeInsertAsync(invoiceFeeRequest2);
                
                var invoiceFee2 = listInvoiceFees2.OrderByDescending(a => a.InvoiceFeeID).First();


                //-- Obtém invoice2
                var invoice2 = (await _taskrowClient.InvoiceDetailGetAsync(invoiceFee2.InvoiceID)).InvoiceDetail;

                Debug.WriteLine($"InvoiceFee inserted (invoiceFeeID={invoiceFee2.InvoiceFeeID}, invoiceID={invoiceFee2.InvoiceID})");


                //-- req 300 - Adicionar autorização do faturamento do faturamento 1 no faturamento 2
                var requestSaveInvoiceAuthorization = new InvoiceAuthorizationSaveRequest(
                    jobNumber: jobNumber,
                    invoiceID: invoice1.InvoiceID,
                    guidModification: invoice1.GuidModification,
                    invoiceFeeIDs: new List<int>() { invoiceFee2.InvoiceFeeID }
                );
                var responseSaveInvoiceAuthorization = await _taskrowClient.InvoiceAuthorizationSaveAsync(requestSaveInvoiceAuthorization);

                invoice1 = (await _taskrowClient.InvoiceDetailGetAsync(invoiceFee1.InvoiceID)).InvoiceDetail;


                //-- req 400 - Adicionar informacoes de faturamento (data, numero previsto, etc)
                var saveInvoiceRequest = new InvoiceSaveRequest(
                    invoiceID: invoice1.InvoiceID,
                    invoiceTypeID: invoice1.InvoiceTypeID,
                    guidModification: invoice1.GuidModification,
                    clientAddressID: invoice1.ClientAddressID,
                    fromClientAddressID: invoice1.FromClientAddressID)
                {
                    InvoiceMemo = invoice1.InvoiceMemo,
                    //InvoiceNumber = invoiceNumber,
                    InvoiceIssueDate = issueDate,
                    InvoiceServiceCodeID = invoiceServiceCodeID,

                    InvoiceGrossValue = invoiceTotal,
                    InvoiceNetValue = invoiceTotal,

                    InvoiceDeductionValue = 0,

                    InvoiceTaxValue = 0,
                    InvoiceTaxValueID = null,

                    DirectTax1AliquotValue = 0.07M,

                    DirectTax1Value = 0,

                    InvoiceTax1Value = 0,
                    InvoiceTax1ValueID = null,

                    InvoiceTax2Value = 0,
                    InvoiceTax2ValueID = null
                };

                var saveInvoiceResponse = await _taskrowClient.InvoiceSaveAsync(saveInvoiceRequest);
                

                //-- req 500 - Gerar cobrança 1
                var saveInvoiceBill1Request = new InvoiceBillSaveRequest(
                    invoiceID: invoice1.InvoiceID,
                    billTypeID: 2,
                    billValue: bill1Value,
                    financialAccountID: financialAccountID,
                    clientAddressID: toClientAddressID,
                    dueDate: bill1DueDate)
                {
                    GuidModification = null,
                    Memo = null,
                    MemoDueDate = null
                };

                var saveInvoiceBill1Response = await _taskrowClient.InvoiceBillSaveAsync(saveInvoiceBill1Request);
                Debug.WriteLine($"InvoiceBill inserted (invoiceID={invoice1.InvoiceID})");


                //-- req 500 - Gerar cobrança 2
                var saveInvoiceBill2Request = new InvoiceBillSaveRequest(
                    invoiceID: invoice1.InvoiceID,
                    billTypeID: 2,
                    billValue: bill2Value,
                    financialAccountID: financialAccountID,
                    clientAddressID: toClientAddressID,
                    dueDate: bill2DueDate)
                {
                    GuidModification = null,
                    Memo = null,
                    MemoDueDate = null
                };

                var saveInvoiceBill2Response = await _taskrowClient.InvoiceBillSaveAsync(saveInvoiceBill2Request);                
                Debug.WriteLine($"InvoiceBill inserted (invoiceID={invoice1.InvoiceID})");

                invoice1 = (await _taskrowClient.InvoiceDetailGetAsync(invoiceFee1.InvoiceID)).InvoiceDetail;
                Debug.WriteLine($"Invoice inserted successfully (invoiceID={invoice1.InvoiceID})");

                //-- Update invoiceStatus

                var updateInvoiceStatus = new InvoiceStatusUpdateRequest(invoice1.InvoiceID, IntegrationStatusEnum.Error, "TaskrowSharp Test", invoice1.GuidModification);
                var responseUpdate = await _taskrowClient.InvoiceStatusUpdateAsync(updateInvoiceStatus);
                
                //-- Cancel Invoice Bills
                foreach (var invoiceBill in invoice1.InvoiceBill)
                {
                    var responseCancelInvoiceBill = await _taskrowClient.InvoiceBillCancelAsync(invoiceBill.InvoiceBillID);                    
                    Debug.WriteLine($"InvoiceBill Canceled (invoiceBillID={invoiceBill.InvoiceBillID}, invoiceID={invoice1.InvoiceID})");
                }

                //-- Cancel Invoice
                var requestCancelInvoice = new InvoiceCancelRequest(invoiceFee1.InvoiceID, "Inserted and canceled by TaskrowSharp", invoice1.GuidModification);
                var responseCancelInvoice = await _taskrowClient.InvoiceCancelAsync(requestCancelInvoice);
                Debug.WriteLine($"Invoice Canceled (invoiceID={invoice1.InvoiceID})");

                invoice1 = (await _taskrowClient.InvoiceDetailGetAsync(invoiceFee1.InvoiceID)).InvoiceDetail;

                //-- Delete Invoice
                var requestDeleteInvoice = new InvoiceDeleteRequest(invoiceFee1.InvoiceID, "Inserted and deleted by TaskrowSharp", invoice1.GuidModification);
                var response = await _taskrowClient.InvoiceDeleteAsync(requestDeleteInvoice);
                Debug.WriteLine($"Invoice Deleted (invoiceID={invoice1.InvoiceID})");

                Assert.NotNull(response);
                Assert.True(response.IsCancelled);
                Assert.True(response.IsDeleted);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($">> ERROR: {ex.Message}");
                throw;
            }
        }
    }
}