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
            var notasDTO = _mapper.Map<List<NotaDTO>>(dadosNotas.value);
            cte.AdicionarDadosNotas(notasDTO);

            dynamic transportadora = new { };
            var transportadoraModel = await _repository.BuscarTransportadoraPorCNPJ(cte.Transportadora.Cnpj);
            if (transportadoraModel == null)
            {
                var transportadoraResponse = await _service.BuscarDadosTrasnportadoraPorCNPJ(cte.Transportadora.Cnpj);
                // VALIDAR TRANSPORTADORA EXISTE
                transportadora = transportadoraResponse;
                transportadoraModel = new TransportadoraModel(Guid.NewGuid(), transportadoraResponse.CNPJ, transportadoraResponse.Nome);
                await _repository.Adicionar(transportadoraModel);
            }
            else
            {
                transportadora = transportadoraModel;
            }

            cte.AdicionarDadosTransportadora(transportadora);
            var cteModel = _mapper.Map<CTEModel>(cte);
            arquivoModel.Processado = true;
            await _repository.Adicionar(cteModel);
            await _repository.SaveChangesAsync();
        }
    }
}