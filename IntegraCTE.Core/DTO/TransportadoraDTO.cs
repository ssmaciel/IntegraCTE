﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegraCTE.Core.DTO
{
    public class TransportadoraDTO
    {
        public Guid Id { get; set; }
        public string Cnpj { get; set; }
        public string Nome { get; set; }
        public string CodigoExterno { get; set; }
    }
}