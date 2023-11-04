using AutoMapper;
using IntegraCTE.Core.DTO;
using IntegraCTE.Core.Repository;
using IntegraCTE.Core.Services;
using IntegraCTE.Core.ValidationMessages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegraCTE.Core.UseCases
{
    public class BuscarCTEs
    {
        protected readonly IIntegraCTERepository _repository;
        protected readonly IValidationMessage _validationMessage;
        protected readonly IMapper _mapper;

        public BuscarCTEs(IIntegraCTERepository repository, IValidationMessage validationMessage, IMapper mapper)
        {
            _repository = repository;
            _validationMessage = validationMessage;
            _mapper = mapper;
        }

        public async Task<List<CTEDto>> Execute()
        {
            var retorno = new List<CTEDto>();
            var ctesModel = await _repository.BuscarCTEs();

            foreach (var cteModel in ctesModel)
            {
                var validacoesModel = await _repository.BuscarValidacoesCTE(cteModel.Id);
                var cteDTO = new CTEDto()
                {
                    Id = cteModel.Id,
                    Transportadora = cteModel.Transportadora.Nome,
                    CTe = cteModel.NumeroCte,
                    DataImportacao = cteModel.DataIntegracao,
                    NFe = cteModel.Notas,
                    OrdemCompra = cteModel.OrdemCompra,
                    Site = cteModel.Site,
                    Valor = string.IsNullOrEmpty(cteModel.ValorCte) ? null : decimal.Parse(cteModel.ValorCte),
                    Status = (cteModel.Integrado) ? "Integrado" : (validacoesModel.Any() && !cteModel.Integrado) ? "Erro" : (!validacoesModel.Any() && !cteModel.Integrado) ? "Pendente" : "Pendente"
                };
                retorno.Add(cteDTO);
            }
            return retorno;
        }
    }
}
