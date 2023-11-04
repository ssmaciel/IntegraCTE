using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegraCTE.Core.DTO
{
    public class CTEDto
    {
        public Guid Id { get; set; }
        public string? Transportadora { get; set; }
        public string? Site { get; set; }
        public string? NFe { get; set; }
        public string? CTe { get; set; }
        public decimal? Valor { get; set; }
        public DateTime? DataImportacao { get; set; }
        public string? OrdemCompra { get; set; }
        public string Status { get; set; }
    }
}
