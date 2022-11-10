using IntegraCTE.Core.EnumsAndConsts;
using IntegraCTE.Core.Factories;

namespace IntegraCTE.Core.UseCases
{
    public class ObterNovosCTE
    {
        private dynamic _fileService;
        private dynamic _commandRepository;
        private dynamic _queueService;

        public ObterNovosCTE(dynamic fileService, dynamic commandRepository, dynamic queueService)
        {
            _fileService = fileService;
            _commandRepository = commandRepository;
            _queueService = queueService;
        }

        public async Task Execute(TransportadoraEnum transportadora)
        {
            var transportadoraService = await TransportadoraFactory.GetTransportadoraService(transportadora);
            var xmls = transportadoraService.BuscarXMLS();
            var listaArquivos = new List<dynamic>();
            foreach (var xml in xmls)
            {
                await SalvarXML(xml);
            }
        }

        private async Task SalvarXML(string xml)
        {
            var arquivo = await _fileService.UploadFile(xml);
            await _commandRepository.Adicionar(arquivo);
            await _commandRepository.SaveChangeAsync();
            await _queueService.NotificarNovoXMLProcessar(arquivo.Id);
        }
    }
}