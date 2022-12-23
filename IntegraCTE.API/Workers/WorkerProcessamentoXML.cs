using IntegraCTE.Core.Model;
using IntegraCTE.Core.Repository;
using IntegraCTE.Core.UseCases;

namespace IntegraCTE.API.Workers
{
    public class WorkerProcessamentoXML : BackgroundService
    {
        protected readonly ILogger<WorkerProcessamentoXML> _logger;
        private readonly IIntegraCTERepository _repository;
        private readonly ProcessarXMLCTE _uc;

        public WorkerProcessamentoXML(ILogger<WorkerProcessamentoXML> logger, IIntegraCTERepository repository, ProcessarXMLCTE uc)
        {
            _logger = logger;
            _repository = repository;
            _uc = uc;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            IEnumerable<Guid> ids = await _repository.BuscarArquivosCTENProcessados();
            try
            {
                _logger.Log(LogLevel.Information, $"Qtde XML: {ids.Count()}");
                foreach (var id in ids)
                {
                    try
                    {
                        await _uc.Execute(id);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError($"Erro ao processar XML Id: '{id}'", ex);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro ao processar XML", ex);
            }
        }
    }
}
