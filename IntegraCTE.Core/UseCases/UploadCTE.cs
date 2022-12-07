using AutoMapper;
using IntegraCTE.Core.DTO;
using IntegraCTE.Core.Entity;
using IntegraCTE.Core.Model;
using IntegraCTE.Core.Repository;
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

        public UploadCTE(IIntegraCTERepository commandRepository, IMapper mapper)
        {
            _commandRepository = commandRepository;
            _mapper = mapper;
        }

        public async Task Execute(ArquivoDTO arquivoDTO)
        {
            var xmlModel = _mapper.Map<CTEModel>(arquivoDTO);
            await _commandRepository.AdicionarXML(xmlModel);
            await _commandRepository.SaveChangesAsync();
            await _commandRepository.SaveChangesAsync();
        }
    }
}
