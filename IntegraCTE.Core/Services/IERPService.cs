using IntegraCTE.Core.Entity;
using IntegraCTE.Core.Services.Model;

namespace IntegraCTE.Core.Services
{
    public interface IERPService
    {
        Task<ListFiscalDocumentEntity_PTR> BuscarDadosNotasPorChavesIN(string chaveNotaFiscal);
        Task<dynamic> BuscarDadosTrasnportadoraPorCNPJ(string cNPJTransportadora);
        Task EnviarCTE(CTE cte);
        Task<ListCTEParameters_PTR> BuscarParametrosIntegracaoCTE();
    }
}