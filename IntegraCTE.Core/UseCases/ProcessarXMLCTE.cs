using AutoMapper;
using IntegraCTE.Core.DTO;
using IntegraCTE.Core.Entity;
using IntegraCTE.Core.Model;
using IntegraCTE.Core.Repository;

namespace IntegraCTE.Core.UseCases
{
    public class ProcessarXMLCTE
    {
        private readonly IIntegraCTERepository _repository;
        private readonly IMapper _mapper;

        public ProcessarXMLCTE(IIntegraCTERepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task Execute(Guid id)
        {
            var arquivoModel = await _repository.BuscarArquivoCTE(id);
            var cte = _mapper.Map<CTE>(arquivoModel);
            cte.ProcessarXML();
            var cteModel = _mapper.Map<CTEModel>(cte);
            arquivoModel.Processado = true;
            await _repository.Adicionar(cteModel);
            await _repository.SaveChangesAsync();
        }
    }
}