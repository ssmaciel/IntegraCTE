using IntegraCTE.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace IntegraCTE.Core.Services
{
    public class CTERequest
    {

        [JsonPropertyName("dataAreaId")]
        public string Empresa { get; private set; }//= cteParameter.DataArea.ToLower()

        [JsonPropertyName("VendorOrderReference")]
        public string ChaveAcessoCte { get; private set; }

        [JsonPropertyName("VendorOrderReference")]
        public DateTime DataEmissao { get; private set; }

        [JsonPropertyName("VendorOrderReference")]
        public string ModeloCte { get; private set; }

        [JsonPropertyName("VendorOrderReference")]
        public string NumeroCte { get; private set; }

        [JsonPropertyName("VendorOrderReference")]
        public string SerieCte { get; private set; }

        [JsonPropertyName("VendorOrderReference")]
        public string Justificativa { get; private set; }

        [JsonPropertyName("InformationCTE_PTR")]
        public string NotaFiscal { get; private set; }



        public string DefaultLedgerDimensionDisplayValue { get; private set; }// = ConfigurationManager.AppSettings["DimensaoFinanceira"].Replace("Estabelecimento", fiscalEstablishmentId),
        public string DefaultReceivingSiteId { get; private set; }//= fiscalEstablishmentId,

        public string InvoiceVendorAccountNumber { get; private set; }//= vendTableEntityResponse.value[0].AccountNum,
        public string OrderVendorAccountNumber { get; private set; }// = vendVendorV2.value[0].VendorAccountNumber,
        public string SalesPurchOperationType_BR { get; private set; }//= operationTypeResponse.value[0].RecId_PTR,

        public string VendorPaymentMethodName { get; private set; }// = vendVendorV2.value.ToList().Where(f => !string.IsNullOrEmpty(f.DefaultVendorPaymentMethodName)).FirstOrDefault()?.DefaultVendorPaymentMethodName,
        public string VendorPaymentMethodSpecificationName { get; private set; }//= vendVendorV2.value.ToList().Where(f => !string.IsNullOrEmpty(f.PaymentSpecificationId)).FirstOrDefault()?.PaymentSpecificationId,
        public string PaymentScheduleName { get; private set; }//= "",
        public decimal CashDiscountPercentage { get; private set; }//= 0,
        public string AccountingDate { get; private set; }//= DateTime.Now.ToString(),
        public string ConfirmedDeliveryDate { get; private set; }//= DateTime.Now.ToString(),
        public string PurchaseOrderNumber { get; private set; }//= "0",
        public string ExpectedCrossDockingDate { get; private set; }//= DateTime.Now.ToString(),
        public string ExpectedStoreAvailableSalesDate { get; private set; }//= DateTime.Now.ToString(),


        public CTERequest(string empresa, string chaveAcessoCte, DateTime dataEmissao, string modeloCte, string numeroCte, string serieCte, string justificativa, string notaFiscal)
        {
            Empresa = empresa;
            ChaveAcessoCte = chaveAcessoCte;
            DataEmissao = dataEmissao;
            ModeloCte = modeloCte;
            NumeroCte = numeroCte;
            SerieCte = serieCte;
            Justificativa = justificativa;
            NotaFiscal = notaFiscal;
        }

        public void PreencherPropriedades()
        {
            PaymentScheduleName = "";
            CashDiscountPercentage = 0;
            AccountingDate = DateTime.Now.ToString();
            ConfirmedDeliveryDate = DateTime.Now.ToString();
            PurchaseOrderNumber = "0";
            ExpectedCrossDockingDate = DateTime.Now.ToString();
            ExpectedStoreAvailableSalesDate = DateTime.Now.ToString();
}
    }
}
