using IntegraCTE.Core.Entity;

namespace IntegraCTE.Core.Services
{
    public interface IERPService
    {
        Task<List<dynamic>> BuscarDadosNotasPorChavesIN(string chaveNotaFiscal);
        Task<dynamic> BuscarDadosTrasnportadoraPorCNPJ(string cNPJTransportadora);
        Task EnviarCTE(CTE cte);
    }
}