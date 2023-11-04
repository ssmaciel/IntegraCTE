using IntegraCTE.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace IntegraCTE.Core.Services
{
    public class CTEResponse
    {
        public string PurchaseOrderNumber { get; set; } = "";
    }
    public class CTERequest
    {

        [JsonPropertyName("dataAreaId")]
        public string Empresa { get; private set; }//= cteParameter.DataArea.ToLower()

        [JsonPropertyName("EFDocAcKey_BR_PTR")]
        public string ChaveAcessoCte { get; private set; }

        [JsonPropertyName("FiscalDocDate_PTR")]
        public DateTime DataEmissao { get; private set; }

        [JsonPropertyName("FiscalDocModel_BR_PTR")]
        public string ModeloCte { get; private set; }

        [JsonPropertyName("FiscalDocNum_PTR")]
        public string NumeroCte { get; private set; }

        [JsonPropertyName("FiscalDocSeries_BR_PTR")]
        public string SerieCte { get; private set; }

        [JsonPropertyName("VendorOrderReference")]
        public string Justificativa { get; private set; }

        [JsonPropertyName("InformationCTE_PTR")]
        public string NotaFiscal { get; private set; }

        [JsonIgnore]
        public CTELinhaRequest Linha { get; private set; }


        public string DefaultLedgerDimensionDisplayValue { get; private set; }// = ConfigurationManager.AppSettings["DimensaoFinanceira"].Replace("Estabelecimento", fiscalEstablishmentId),
        public string DefaultReceivingSiteId { get; private set; }//= fiscalEstablishmentId,

        public string InvoiceVendorAccountNumber { get; private set; }//= vendTableEntityResponse.value[0].AccountNum,
        public string OrderVendorAccountNumber { get; private set; }// = vendVendorV2.value[0].VendorAccountNumber,
        public long SalesPurchOperationType_BR { get; private set; }//= operationTypeResponse.value[0].RecId_PTR,

        public string VendorPaymentMethodName { get; private set; }// = vendVendorV2.value.ToList().Where(f => !string.IsNullOrEmpty(f.DefaultVendorPaymentMethodName)).FirstOrDefault()?.DefaultVendorPaymentMethodName,
        public string VendorPaymentMethodSpecificationName { get; private set; }//= vendVendorV2.value.ToList().Where(f => !string.IsNullOrEmpty(f.PaymentSpecificationId)).FirstOrDefault()?.PaymentSpecificationId,
        [JsonIgnore]
        public string PaymentScheduleName { get; private set; }//= "",
        [JsonIgnore]
        public decimal CashDiscountPercentage { get; private set; }//= 0,
        [JsonIgnore]
        public DateTime AccountingDate { get; private set; }//= DateTime.Now.ToString(),
        [JsonIgnore]
        public DateTime ConfirmedDeliveryDate { get; private set; }//= DateTime.Now.ToString(),
        [JsonIgnore]
        public DateTime ExpectedCrossDockingDate { get; private set; }//= DateTime.Now.ToString(),
        [JsonIgnore]
        public DateTime ExpectedStoreAvailableSalesDate { get; private set; }//= DateTime.Now.ToString(),
        public string LanguageId { get; private set; }//= 0,

        public CTERequest() { }
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
            AccountingDate = DateTime.Now;
            ConfirmedDeliveryDate = DateTime.Now;
            ExpectedCrossDockingDate = DateTime.Now;
            ExpectedStoreAvailableSalesDate = DateTime.Now;
            LanguageId = "pt-BR";
        }

        internal void AdicionarTipoOperacao(long tipo)
        {
            SalesPurchOperationType_BR = tipo;
        }

        internal void AdicionarEmpresa(string dataAreaId)
        {
            Empresa = dataAreaId;
        }
    }

    public class CTELinhaRequest
    {
        public string PurchaseOrderNumber { get; set; }

        [JsonPropertyName("DeliveryAddressName")]
        [JsonIgnore]
        public string DestinatarioNome { get; set; }

        [JsonPropertyName("DeliveryAddressStreet")]
        [JsonIgnore]
        public string DestinatarioLogradouro { get; set; }

        [JsonPropertyName("DeliveryAddressStreetNumber")]
        [JsonIgnore]
        public string DestinatarioNro { get; set; }

        [JsonPropertyName("DeliveryAddressDistrictName")]
        [JsonIgnore]
        public string DestinatarioBairro { get; set; }

        [JsonPropertyName("DeliveryAddressCity")]
        [JsonIgnore]
        public string DestinatarioMunicipio { get; set; }

        [JsonPropertyName("DeliveryAddressZipCode")]
        [JsonIgnore]
        public string DestinatarioCEP { get; set; }

        [JsonPropertyName("DeliveryAddressStateId")]
        [JsonIgnore]
        public string DestinatarioUF { get; set; }

        [JsonPropertyName("DeliveryAddressCountyId")]
        [JsonIgnore]
        public string DestinatarioCodigoPais { get; set; }

        public string ItemNumber { get; set; }
        public int LineNumber { get; set; }
        public decimal PurchasePrice { get; set; }
        public int PurchasePriceQuantity { get; set; }
        public string CFOPCode { get; set; }
        public string dataAreaId { get; set; }

    }
}
