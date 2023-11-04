using AutoMapper;
using IntegraCTE.Core.DTO;
using IntegraCTE.Core.Entity;
using IntegraCTE.Core.Model;
using IntegraCTE.Core.Repository;
using IntegraCTE.Core.Services;
using IntegraCTE.Core.Services.Model;
using IntegraCTE.Core.ValidationMessages;

namespace IntegraCTE.Core.UseCases
{
    public class IntegrarCTE
    {
        private readonly IIntegraCTERepository _repository;
        private readonly IMapper _mapper;
        private readonly IERPService _service;
        protected readonly IValidationMessage _validationMessage;

        public IntegrarCTE(IIntegraCTERepository queryRepository, IMapper mapper, IERPService erpService, IValidationMessage validationMessage)
        {
            _repository = queryRepository;
            _mapper = mapper;
            _service = erpService;
            _validationMessage = validationMessage;
        }

        public async Task Execute(Guid id)
        {
            var cteModel = await _repository.BuscarCTE(id);
            //var cte = _mapper.Map<CTE>(cteModel);
            
            var cteRequest = _mapper.Map<CTERequest>(cteModel);
            cteRequest.PreencherPropriedades();
            var tipoOperacao = await _service.BuscarTipoOperacao();
            var tipo = tipoOperacao.value[0].RecId_PTR;
            var dataAreaId = tipoOperacao.value[0].dataAreaId;
            cteRequest.AdicionarTipoOperacao(tipo);
            cteRequest.AdicionarEmpresa(dataAreaId);
            await _service.EnviarCTE(cteRequest);
            var a = _validationMessage.GetValidations();
        }
    }
}