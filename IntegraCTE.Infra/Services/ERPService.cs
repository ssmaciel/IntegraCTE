using IntegraCTE.Core.DTO;
using IntegraCTE.Core.Entity;
using IntegraCTE.Core.Services;
using IntegraCTE.Core.Services.Model;
using IntegraCTE.Core.Services.Responses;
using IntegraCTE.Core.ValidationMessages;
using IntegraCTE.Infra.Services.Model;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace IntegraCTE.Infra.Services
{
    public class ERPService : IERPService
    {
        private readonly ILogger<ERPService> _logger;
        private readonly ODataJson _oData;
        private readonly IConfiguration _configuration;
        protected readonly IValidationMessage _validationMessage;

        public ERPService(ILogger<ERPService> logger, ODataJson oData, IConfiguration configuration, IValidationMessage validationMessage)
        {
            _logger = logger;
            _oData = oData;
            _configuration = configuration;
            _validationMessage = validationMessage;
        }

        public async Task<ListFiscalDocumentEntity_PTR> BuscarDadosNotasPorChavesIN(List<Nota> notas)
        {
            var chaves = $"";
            foreach (var chave in notas)
            {
                if (!string.IsNullOrEmpty(chave.ChaveNotaFical))
                    chaves += $"AccessKey eq '{chave.ChaveNotaFical}' or ";
            }
            chaves = chaves.Remove(chaves.Length - 4);
            ODataRequest oDataRequest = new ODataRequest { EntityName = "FiscalDocumentEntity_PTR", Params = $"&$filter=dataAreaId eq '{_configuration.GetSection("ERPService:SiglaEmpresa").Value}' and ({chaves})" };
            var cte = await _oData.Lookup<ListFiscalDocumentEntity_PTR>(oDataRequest.EntityName, oDataRequest.Params);
            return cte;
            //throw new NotImplementedException();
        }

        public async Task<TransportadoraResponse> BuscarDadosTrasnportadoraPorCNPJ(string cNPJTransportadora)
        {
            var cnpjTransportadoraFormat = Convert.ToUInt64(cNPJTransportadora).ToString("000000000000-00");
            ODataRequest oDataRequest = new ODataRequest { EntityName = "VendorsV2", Params = $"&$filter=dataAreaId eq '{_configuration.GetSection("ERPService:SiglaEmpresa").Value}' and BrazilianCNPJOrCPF eq '{cnpjTransportadoraFormat}'" };
            var cte = await _oData.Lookup<ListVendVendorV2Entity>(oDataRequest.EntityName, oDataRequest.Params);
            if (cte.value != null && cte.value.Length > 0)
            {
                var transportadora = new TransportadoraResponse()
                {
                    Cnpj = cnpjTransportadoraFormat,
                    CodigoExterno = cte.value[0].VendorAccountNumber,
                    Nome = cte.value[0].VendorOrganizationName,
                    MetodoPagamento = cte.value.ToList().Where(f => !string.IsNullOrEmpty(f.DefaultVendorPaymentMethodName)).FirstOrDefault()?.DefaultVendorPaymentMethodName,
                    EspecificacaoMetodoPagamento = cte.value.ToList().Where(f => !string.IsNullOrEmpty(f.PaymentSpecificationId)).FirstOrDefault()?.PaymentSpecificationId,
                    CalendarioPagamento = cte.value.ToList().Where(f => !string.IsNullOrEmpty(f.DefaultPaymentScheduleName)).FirstOrDefault()?.DefaultPaymentScheduleName
            };
                return transportadora;
            }
            return null;
        }

        public async Task<string?> EnviarCTE(CTERequest cte)
        {
            var jsonOpt = new JsonSerializerOptions();
            jsonOpt.Converters.Add(new CustomDateTimeConverter());
            dynamic _mediator = new { };
            var jsonHeader = JsonSerializer.Serialize(cte, options: jsonOpt);
            var ret = await _oData.Post<CTEResponse>("PurchaseOrderHeadersV2", jsonHeader);
            if (ret == null) return null;
            cte.Linha.PurchaseOrderNumber = ret.PurchaseOrderNumber;
            var jsonLine = JsonSerializer.Serialize(cte.Linha, options: jsonOpt);
            await _oData.Post<CTELinhaRequest>("PurchaseOrderLines", jsonLine);
            return ret.PurchaseOrderNumber;
        }

        public async Task<ListCTEParameters_PTR> BuscarParametrosIntegracaoCTE()
        {
            ODataRequest oDataRequest = new ODataRequest { EntityName = "CTEParameters_PTR", Params = $"&$filter=dataAreaId eq '{_configuration.GetSection("ERPService:SiglaEmpresa").Value}'" };
            var cte = await _oData.Lookup<ListCTEParameters_PTR>(oDataRequest.EntityName, oDataRequest.Params);
            return cte;
        }

        public async Task<ListOperationTypes> BuscarTipoOperacao()
        {
            var cteParameter = await BuscarParametrosIntegracaoCTE();
            if (cteParameter is null || cteParameter.value is null || cteParameter.value.Count() == 0)
            {
                // setar mensagem de erro
                return null;
            }
            ODataRequest oDataRequest = new ODataRequest { EntityName = "OperationTypes", Params = $"&$filter=dataAreaId eq '{_configuration.GetSection("ERPService:SiglaEmpresa").Value}' and OperationTypeID eq '{cteParameter.value[0].OperationTypeID}'" };
            var cte = await _oData.Lookup<ListOperationTypes>(oDataRequest.EntityName, oDataRequest.Params);
            return cte;
        }

        public async Task<ListFiscalEstablishments> BuscarEstabelecimentoFiscal(string cnpjEntidadeLegal)
        {
            var cteParameter = await BuscarParametrosIntegracaoCTE();
            if (cteParameter is null || cteParameter.value is null || cteParameter.value.Count() == 0)
            {
                // setar mensagem de erro
                return null;
            }
            ODataRequest oDataRequest = new ODataRequest { EntityName = "FiscalEstablishments", Params = $"&$filter=dataAreaId eq '{cteParameter.value[0].DataArea.ToLower()}' and CNPJ eq '{cnpjEntidadeLegal}'" };
                                                 
            var cte = await _oData.Lookup<ListFiscalEstablishments>(oDataRequest.EntityName, oDataRequest.Params);
            return cte;
        }
    }
    public class CustomDateTimeConverter : JsonConverter<DateTime>
    {
        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return DateTime.ParseExact(reader.GetString(),
                "yyyy-MM-dd", CultureInfo.InvariantCulture);
        }

        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString("yyyy-MM-dd"));
        }
    }
}
