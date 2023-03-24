using IntegraCTE.Core.DTO;
using IntegraCTE.Core.Entity;
using IntegraCTE.Core.Services;
using IntegraCTE.Core.Services.Model;
using IntegraCTE.Core.Services.Responses;
using IntegraCTE.Infra.Services.Model;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegraCTE.Infra.Services
{
    public class ERPService : IERPService
    {
        private readonly ILogger<ODataJson> _logger;
        private readonly ODataJson _oData;

        public ERPService(ILogger<ODataJson> logger, ODataJson oData)
        {
            _logger = logger;
            _oData = oData;
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
            ODataRequest oDataRequest = new ODataRequest { EntityName = "FiscalDocumentEntity_PTR", Params = $"&$filter=dataAreaId eq 'CNX' and ({chaves})" };
            var cte = await _oData.Lookup<ListFiscalDocumentEntity_PTR>(oDataRequest.EntityName, oDataRequest.Params);
            return cte;
            //throw new NotImplementedException();
        }

        public async Task<TransportadoraResponse> BuscarDadosTrasnportadoraPorCNPJ(string cNPJTransportadora)
        {
            ODataRequest oDataRequest = new ODataRequest { EntityName = "VendorsV2", Params = $"&$filter=dataAreaId eq 'CNX' and BrazilianCNPJOrCPF eq '{Convert.ToUInt64(cNPJTransportadora).ToString("000000000000-00")}'" };
            var cte = await _oData.Lookup<ListVendVendorV2Entity>(oDataRequest.EntityName, oDataRequest.Params);
            if (cte.value != null && cte.value.Length > 0)
            {
                var transportadora = new TransportadoraResponse()
                {
                    Cnpj = Convert.ToUInt64(cNPJTransportadora).ToString("000000000000-00"),
                    CodigoExterno = cte.value[0].VendorAccountNumber,
                    Nome = cte.value[0].VendorOrganizationName
                };
                return transportadora;
            }
            return null;
        }

        public Task EnviarCTE(CTE cte)
        {
            throw new NotImplementedException();
        }

        public async Task<ListCTEParameters_PTR> BuscarParametrosIntegracaoCTE()
        {
            ODataRequest oDataRequest = new ODataRequest { EntityName = "CTEParameters_PTR", Params = $"&$filter=dataAreaId eq 'CNX'" };
            var cte = await _oData.Lookup<ListCTEParameters_PTR>(oDataRequest.EntityName, oDataRequest.Params);
            return cte;
        }
    }
}
