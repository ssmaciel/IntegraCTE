using IntegraCTE.Core.DTO;
using IntegraCTE.Core.Entity;
using IntegraCTE.Core.Model;
using IntegraCTE.Core.Services;

namespace IntegraCTE.Core.UseCases
{
    public class IntegrarCTE
    {
        private dynamic _queryRepository;
        private dynamic _commandRepository;
        private dynamic _mapper;
        private IERPService _erpService;

        public IntegrarCTE(dynamic queryRepository, dynamic commandRepository, dynamic mapper, IERPService erpService)
        {
            _queryRepository = queryRepository;
            _commandRepository = commandRepository;
            _mapper = mapper;
            _erpService = erpService;
        }

        public async Task Execute(Guid id)
        {
            var cteModel = await _queryRepository.BuscarCTE(id);
            var cte = _mapper.Map<CTE>(cteModel);
            await _erpService.EnviarCTE(cte);
        }
    }
}