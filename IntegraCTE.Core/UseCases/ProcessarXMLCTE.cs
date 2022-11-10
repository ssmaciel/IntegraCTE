using IntegraCTE.Core.DTO;
using IntegraCTE.Core.Entity;
using IntegraCTE.Core.Model;

namespace IntegraCTE.Core.UseCases
{
    public class ProcessarXMLCTE
    {
        private dynamic _queryRepository;
        private dynamic _commandRepository;
        private dynamic _mapper;

        public ProcessarXMLCTE(dynamic queryRepository, dynamic commandRepository, dynamic mapper)
        {
            _queryRepository = queryRepository;
            _commandRepository = commandRepository;
            _mapper = mapper;
        }

        public async Task Execute(Guid id)
        {
            var arquivoModel = await _queryRepository.BuscarArquivoCTE(id);
            var arquivoDTO = _mapper.Map<ArquivoDTO>(arquivoModel);
            arquivoDTO.ProcessarXML();
            var cte = _mapper.Map<CTE>(arquivoDTO);
            var cteModel = _mapper.Map<CTEModel>(cte);
            await _commandRepository.Adicionar(cteModel);
            await _commandRepository.SaveChangeAsync();
        }

    }
}