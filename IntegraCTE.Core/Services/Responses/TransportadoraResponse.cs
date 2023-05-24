using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegraCTE.Core.Services.Responses
{
    public class TransportadoraResponse
    {
        public Guid Id { get; set; }
        public string Cnpj { get; set; }
        public string Nome { get; set; }
        public string CodigoExterno { get; set; }
        public string MetodoPagamento { get; set; }
        public string EspecificacaoMetodoPagamento { get; set; }
        public string CalendarioPagamento { get; set; }
    }
}
