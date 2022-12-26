﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegraCTE.Core.Entity
{
    public class Transportadora
    {
        public string Id { get; private set; }
        public string Cnpj { get; private set; }
        public string Nome { get; private set; }

        public Transportadora(string id, string cnpj, string nome)
        {
            Id = id;
            Cnpj = cnpj;
            Nome = nome;
        }

        public Transportadora(string cnpj)
        {
            Cnpj = cnpj;
        }
    }
}
