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
            var cteRequest = _mapper.Map<CTERequest>(cteModel);
            cteRequest.PreencherPropriedades();
            var tipoOperacao = await _service.BuscarTipoOperacao();
            if (_validationMessage.HasValidation())
            {
                await GerarValidacoes(cteModel.Id);
                return;
            }
            var tipo = tipoOperacao.value[0].RecId_PTR;
            var dataAreaId = tipoOperacao.value[0].dataAreaId;
            cteRequest.AdicionarTipoOperacao(tipo);
            cteRequest.AdicionarEmpresa(dataAreaId);
            var numeroOrdemCompra = await _service.EnviarCTE(cteRequest);
            if (!string.IsNullOrEmpty(numeroOrdemCompra))
                cteModel.AddOrdemCompra(numeroOrdemCompra);

            if (!_validationMessage.HasValidation())
            {
                cteModel.Integrado = true;
                cteModel.DataIntegracao = DateTime.Now;
            }
            else
            {
                await GerarValidacoes(cteModel.Id);
            }

            await _repository.SaveChangesAsync();
        }

        private async Task GerarValidacoes(Guid id)
        {
            var validacoesModel = _validationMessage.GetValidations().Select(s => new ValidacaoModel(id, s.Message, s.Type == ValidationType.Negocio ? "Negocio" : s.Type == ValidationType.ERP ? "ERP" : "Geral"));

            foreach (var validacaoModel in validacoesModel)
            {
                await _repository.Adicionar(validacaoModel);
            }
            await _repository.SaveChangesAsync();
        }
    }
}