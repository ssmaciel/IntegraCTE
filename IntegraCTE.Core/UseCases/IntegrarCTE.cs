using AutoMapper;
using IntegraCTE.Core.DTO;
using IntegraCTE.Core.Entity;
using IntegraCTE.Core.Model;
using IntegraCTE.Core.Repository;
using IntegraCTE.Core.Services;

namespace IntegraCTE.Core.UseCases
{
    public class IntegrarCTE
    {
        private readonly IIntegraCTERepository _repository;
        private readonly IMapper _mapper;
        private readonly IERPService _service;

        public IntegrarCTE(IIntegraCTERepository queryRepository, IMapper mapper, IERPService erpService)
        {
            _repository = queryRepository;
            _mapper = mapper;
            _service = erpService;
        }

        public async Task Execute(Guid id)
        {
            var cteModel = await _repository.BuscarCTE(id);
            //var cte = _mapper.Map<CTE>(cteModel);
            
            var cteRequest = _mapper.Map<CTERequest>(cteModel);
            cteRequest.PreencherPropriedades();
            await _service.EnviarCTE(cteRequest);
        }
    }
}