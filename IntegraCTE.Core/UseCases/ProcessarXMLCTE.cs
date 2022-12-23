using AutoMapper;
using IntegraCTE.Core.DTO;
using IntegraCTE.Core.Entity;
using IntegraCTE.Core.Model;
using IntegraCTE.Core.Repository;
using IntegraCTE.Core.Services;

namespace IntegraCTE.Core.UseCases
{
    public class ProcessarXMLCTE
    {
        private readonly IIntegraCTERepository _repository;
        private readonly IMapper _mapper;
        private readonly IERPService _service;

        public ProcessarXMLCTE(IIntegraCTERepository repository, IMapper mapper, IERPService service)
        {
            _repository = repository;
            _mapper = mapper;
            _service = service;
        }

        public async Task Execute(Guid id)
        {
            var arquivoModel = await _repository.BuscarArquivoCTE(id);
            var cte = _mapper.Map<CTE>(arquivoModel);
            cte.ProcessarXML();
            var dadosNotas = await _service.BuscarDadosNotasPorChavesIN(cte.ChaveNotaFiscal);
            var transportadoraResponse = await _service.BuscarDadosTrasnportadoraPorCNPJ(cte.Transportadora.Cnpj);
            cte.AdicionarDadosNotas(dadosNotas);
            cte.AdicionarDadosTransportadora(transportadoraResponse);
            var cteModel = _mapper.Map<CTEModel>(cte);
            arquivoModel.Processado = true;
            await _repository.Adicionar(cteModel);
            await _repository.SaveChangesAsync();
        }
    }
}