using AutoMapper;
using IntegraCTE.Core.DTO;
using IntegraCTE.Core.Entity;
using IntegraCTE.Core.Model;
using IntegraCTE.Core.Repository;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegraCTE.Core.UseCases
{
    public class UploadCTE
    {
        private IIntegraCTERepository _commandRepository;
        private IMapper _mapper;
        private readonly IConfiguration _configuration;

        public UploadCTE(IIntegraCTERepository commandRepository, IMapper mapper, IConfiguration configuration)
        {
            _commandRepository = commandRepository;
            _mapper = mapper;
            _configuration = configuration;
        }

        public async Task Execute(ArquivoDTO arquivoDTO)
        {
            var xmlModel = _mapper.Map<ArquivoModel>(arquivoDTO);
            xmlModel.Empresa = _configuration.GetSection("ERPService:SiglaEmpresa").Value;
            await _commandRepository.Adicionar(xmlModel);
            await _commandRepository.SaveChangesAsync();
        }
    }
}
