using IntegraCTE.Core.Entity;
using IntegraCTE.Core.Services;
using IntegraCTE.Core.Services.Model;
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

        public Task<List<dynamic>> BuscarDadosNotasPorChavesIN(string chaveNotaFiscal)
        {
            throw new NotImplementedException();
        }

        public Task<dynamic> BuscarDadosTrasnportadoraPorCNPJ(string cNPJTransportadora)
        {
            throw new NotImplementedException();
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
