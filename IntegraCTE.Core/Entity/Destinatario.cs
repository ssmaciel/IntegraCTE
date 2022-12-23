using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegraCTE.Core.Entity
{
    public class Destinatario
    {
        public string CNPJCPF { get; set; }
        public string Nome { get; set; }
        public string Logradouro { get; set; }
        public string Nro { get; set; }
        public string Bairro { get; set; }
        public string CodigoMunicipio { get; set; }
        public string Municipio { get; set; }
        public string CEP { get; set; }
        public string UF { get; set; }
        public string CodigoPais { get; set; }
        public string Pais { get; set; }
    }
}
