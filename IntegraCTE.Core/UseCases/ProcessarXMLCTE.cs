using AutoMapper;
using IntegraCTE.Core.DTO;
using IntegraCTE.Core.Entity;
using IntegraCTE.Core.Model;
using IntegraCTE.Core.Repository;
using IntegraCTE.Core.Services;
using IntegraCTE.Core.ValidationMessages;
using System.Linq;

namespace IntegraCTE.Core.UseCases
{
    public class ProcessarXMLCTE
    {
        private readonly IIntegraCTERepository _repository;
        private readonly IMapper _mapper;
        private readonly IERPService _service;
        protected readonly IValidationMessage _validationMessage;

        public ProcessarXMLCTE(IIntegraCTERepository repository, IMapper mapper, IERPService service, IValidationMessage validationMessage)
        {
            _repository = repository;
            _mapper = mapper;
            _service = service;
            _validationMessage = validationMessage;
        }

        public async Task Execute(Guid id)
        {
            var arquivoModel = await _repository.BuscarArquivoCTE(id);
            var cte = _mapper.Map<CTE>(arquivoModel);
            var config = await _service.BuscarParametrosIntegracaoCTE();
            cte.ProcessarXML(config.value[0].DataArea);
            if (cte.HasValidation())
            {
                GerarValidacoes(cte);
                return;
            }

            cte.PreencherLinha(config.value[0].ItemId, config.value[0].DataArea);
            var chave = $"'{cte.ChaveNotaFiscal.Remove(cte.ChaveNotaFiscal.Length-1).Replace(",", "','")}'";
            var dadosNotas = await _service.BuscarDadosNotasPorChavesIN(cte.Notas);
            var notasDTO = _mapper.Map<List<NotaDTO>>(dadosNotas.value);
            cte.AdicionarDadosNotas(notasDTO);

            TransportadoraDTO transportadoraDTO;
            var transportadoraModel = await _repository.BuscarTransportadoraPorCNPJ(Convert.ToUInt64(cte.Transportadora.Cnpj).ToString("000000000000-00"));
            if (transportadoraModel == null)
            {
                var transportadoraResponse = await _service.BuscarDadosTrasnportadoraPorCNPJ(cte.Transportadora.Cnpj);
                transportadoraDTO = _mapper.Map<TransportadoraDTO>(transportadoraResponse);
                transportadoraDTO.Id = Guid.NewGuid();

                transportadoraModel = new TransportadoraModel(transportadoraDTO.Id, transportadoraDTO.Cnpj, transportadoraDTO.Nome, transportadoraDTO.CodigoExterno, transportadoraDTO.MetodoPagamento, transportadoraDTO.EspecificacaoMetodoPagamento, transportadoraDTO.CalendarioPagamento);
                await _repository.Adicionar(transportadoraModel);
            }
            else
            {
                transportadoraDTO = _mapper.Map<TransportadoraDTO>(transportadoraModel);
            }

            cte.AdicionarDadosTransportadora(transportadoraDTO);
            var cteModel = _mapper.Map<CTEModel>(cte);
            arquivoModel.Processado = true;
            await _repository.Adicionar(cteModel);
            await _repository.SaveChangesAsync();
            if (_validationMessage.HasValidation())
                await GerarValidacoes(cte.Id);
        }

        private async void GerarValidacoes(CTE cte)
        {
            foreach (var item in cte.GetMessages())
                _validationMessage.AddMessage(item, ValidationType.Negocio);

            await GerarValidacoes(cte.Id);
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