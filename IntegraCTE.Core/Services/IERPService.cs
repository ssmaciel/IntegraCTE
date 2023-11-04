using IntegraCTE.Core.DTO;
using IntegraCTE.Core.Entity;
using IntegraCTE.Core.Services.Model;
using IntegraCTE.Core.Services.Responses;

namespace IntegraCTE.Core.Services
{
    public interface IERPService
    {
        Task<ListFiscalDocumentEntity_PTR> BuscarDadosNotasPorChavesIN(List<Nota> notas);
        Task<TransportadoraResponse> BuscarDadosTrasnportadoraPorCNPJ(string cNPJTransportadora);
        Task<string?> EnviarCTE(CTERequest cte);
        Task<ListCTEParameters_PTR> BuscarParametrosIntegracaoCTE();
        Task<ListOperationTypes> BuscarTipoOperacao();
        Task<ListFiscalEstablishments> BuscarEstabelecimentoFiscal(string cnpjEntidadeLegal);
    }
}