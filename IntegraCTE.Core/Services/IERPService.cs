using IntegraCTE.Core.Entity;

namespace IntegraCTE.Core.Services
{
    public interface IERPService
    {
        Task EnviarCTE(CTE cte);
    }
}