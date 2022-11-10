using IntegraCTE.Core.EnumsAndConsts;
using IntegraCTE.Core.Services;

namespace IntegraCTE.Core.Factories
{
    public static class TransportadoraFactory
    {
        public  static Task<ITransportadoraService?> GetTransportadoraService(TransportadoraEnum transportadora)
        {
            switch (transportadora)
            {
                case TransportadoraEnum.BRASPRESS: return null;
                default: return null;
            }
        }
    }
}