using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Net.Http;
using System.Net;
using System.Threading.Tasks;
using TaskrowSharp.IntegrationTests.TestModels;
using TaskrowSharp.Models;
using Xunit;
using System.Diagnostics;

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
                var invoiceFeeRequest1 = new InsertInvoiceFeeRequest(
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

                var invoiceFeeResponse1 = await _taskrowClient.InsertInvoiceFeeAsync(invoiceFeeRequest1);
                if (!invoiceFeeResponse1.Success)
                    throw new InvalidOperationException($"InsertInvoiceFeeAsync returned success=false -- {invoiceFeeResponse1.Message}");

                var invoiceFee1 = invoiceFeeResponse1.Entities!.OrderByDescending(a => a.InvoiceFeeID).First();

                Debug.WriteLine($"InvoiceFee inserted (invoiceFeeID={invoiceFee1.InvoiceFeeID}, invoiceID={invoiceFee1.InvoiceID})");


                //-- Obtém invoice1
                var invoice1 = (await _taskrowClient.GetInvoiceDetailAsync(invoiceFee1.InvoiceID)).InvoiceDetail;


                //-- req 200 - Cria faturamento 2
                var invoiceFeeRequest2 = new InsertInvoiceFeeRequest(
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

                var invoiceFeeResponse2 = await _taskrowClient.InsertInvoiceFeeAsync(invoiceFeeRequest2);
                if (!invoiceFeeResponse2.Success)
                    throw new InvalidOperationException($"InsertInvoiceFeeAsync returned success=false -- {invoiceFeeResponse2.Message}");

                var invoiceFee2 = invoiceFeeResponse2.Entities!.OrderByDescending(a => a.InvoiceFeeID).First();


                //-- Obtém invoice2
                var invoice2 = (await _taskrowClient.GetInvoiceDetailAsync(invoiceFee2.InvoiceID)).InvoiceDetail;

                Debug.WriteLine($"InvoiceFee inserted (invoiceFeeID={invoiceFee2.InvoiceFeeID}, invoiceID={invoiceFee2.InvoiceID})");


                //-- req 300 - Adicionar autorização do faturamento do faturamento 1 no faturamento 2
                var responseSaveInvoiceAuthorization = await _taskrowClient.SaveInvoiceAuthorizationAsync(
                    jobNumber: jobNumber,
                    invoiceID: invoice1.InvoiceID,
                    guidModification: invoice1.GuidModification,
                    invoiceFeeIDs: new List<int>() { invoiceFee2.InvoiceFeeID });

                if (!responseSaveInvoiceAuthorization.Success)
                    throw new InvalidOperationException($"SaveInvoiceAuthorizationAsync returned success=false -- {responseSaveInvoiceAuthorization.Message}");

                invoice1 = (await _taskrowClient.GetInvoiceDetailAsync(invoiceFee1.InvoiceID)).InvoiceDetail;


                //-- req 400 - Adicionar informacoes de faturamento (data, numero previsto, etc)
                var saveInvoiceRequest = new SaveInvoiceRequest(
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

                var saveInvoiceResponse = await _taskrowClient.SaveInvoiceAsync(saveInvoiceRequest);
                if (!saveInvoiceResponse.Success)
                    throw new InvalidOperationException($"SaveInvoiceAsync returned success=false -- {saveInvoiceResponse.Message}");


                //-- req 500 - Gerar cobrança 1
                var saveInvoiceBill1Request = new SaveInvoiceBillRequest(
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

                var saveInvoiceBill1Response = await _taskrowClient.SaveInvoiceBillAsync(saveInvoiceBill1Request);
                if (!saveInvoiceBill1Response.Success)
                    throw new InvalidOperationException($"SaveInvoiceBillAsync returned success=false -- {saveInvoiceBill1Response.Message}");

                Debug.WriteLine($"InvoiceBill inserted (invoiceID={invoice1.InvoiceID})");


                //-- req 500 - Gerar cobrança 2
                var saveInvoiceBill2Request = new SaveInvoiceBillRequest(
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

                var saveInvoiceBill2Response = await _taskrowClient.SaveInvoiceBillAsync(saveInvoiceBill2Request);
                if (!saveInvoiceBill2Response.Success)
                    throw new InvalidOperationException($"SaveInvoiceBillAsync returned success=false -- {saveInvoiceBill2Response.Message}");

                Debug.WriteLine($"InvoiceBill inserted (invoiceID={invoice1.InvoiceID})");


                invoice1 = (await _taskrowClient.GetInvoiceDetailAsync(invoiceFee1.InvoiceID)).InvoiceDetail;

                Debug.WriteLine($"Invoice inserted successfully (invoiceID={invoice1.InvoiceID})");

                //-- Update invoiceStatus

                var updateInvoiceStatus = new UpdateInvoiceStatusRequest(invoice1.InvoiceID, IntegrationStatusEnum.Error, "TaskrowSharp Test");
                var responseUpdate = await _taskrowClient.UpdateInvoiceStatusAsync(updateInvoiceStatus);
                if (!responseUpdate.Success)
                    throw new InvalidOperationException($"UpdateInvoiceStatusAsync returned success=false -- {responseUpdate.Message}");
                
                //-- Cancel Invoice Bills
                foreach (var invoiceBill in invoice1.InvoiceBill)
                {
                    var responseCancelInvoiceBill = await _taskrowClient.CancelInvoiceBillAsync(invoiceBill.InvoiceBillID);
                    if (!responseCancelInvoiceBill.Success)
                        throw new InvalidOperationException($"CancelInvoiceBillAsync returned success=false -- {responseCancelInvoiceBill.Message}");
                    Debug.WriteLine($"InvoiceBill Canceled (invoiceBillID={invoiceBill.InvoiceBillID}, invoiceID={invoice1.InvoiceID})");
                }

                //-- Cancel Invoice
                var responseCancelInvoice = await _taskrowClient.CancelInvoiceAsync(invoiceFee1.InvoiceID, "Inserted and canceled by TaskrowSharp");
                if (!responseCancelInvoice.Success)
                    throw new InvalidOperationException($"CancelInvoiceAsync returned success=false -- {responseCancelInvoice.Message}");
                Debug.WriteLine($"Invoice Canceled (invoiceID={invoice1.InvoiceID})");

                //-- Delete Invoice
                var response = await _taskrowClient.DeleteInvoiceAsync(invoiceFee1.InvoiceID, "Inserted and deleted by TaskrowSharp");
                Debug.WriteLine($"Invoice Deleted (invoiceID={invoice1.InvoiceID})");

                Assert.True(response.Success);
                Assert.True(response.Entity.IsCancelled);
                Assert.True(response.Entity.IsDeleted);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($">> ERROR: {ex.Message}");
                throw;
            }
        }
    }
}