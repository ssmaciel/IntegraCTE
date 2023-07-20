namespace IntegraCTE.Core.Model
{
    public class ArquivoModel
    {
        public Guid Id { get; set; }
        public string XML { get; set; }
        public bool Processado { get; set; }
        public string Empresa { get; set; }

        //public string TransportadoraCnpj { get; private set; }
        //public string TransportadoraNome { get; private set; }


        //public string DestinatarioCNPJCPF { get; set; }
        //public string DestinatarioNome { get; set; }
        //public string DestinatarioLogradouro { get; set; }
        //public string DestinatarioNro { get; set; }
        //public string DestinatarioBairro { get; set; }
        //public string DestinatarioCodigoMunicipio { get; set; }
        //public string DestinatarioMunicipio { get; set; }
        //public string DestinatarioCEP { get; set; }
        //public string DestinatarioUF { get; set; }
        //public string DestinatarioCodigoPais { get; set; }
        //public string DestinatarioPais { get; set; }

        //public DateTime DataHoraCriacao { get; private set; }

        //public string Site { get; private set; }

        //public string CNPJRemetente { get; private set; }

        //public string CNPJEntidadeLegal { get; private set; }

        //public string ValorCte { get; private set; }

        //public string ModeloCte { get; private set; }

        //public string SerieCte { get; private set; }

        //public string NumeroCte { get; private set; }

        //public string ChaveAcessoCte { get; private set; }

        //public string Justificativa { get; private set; }

        //public DateTime DataEmissao { get; private set; }

        //public string TomadorServico { get; private set; }

        //public string NotaFiscal { get; private set; }
        //public string ChaveNotaFiscal { get; private set; }

        //public string CFOP { get; private set; }

        //public string UFEnv { get; private set; }

        //public string UFEmitente { get; private set; }

        //public string UFRemetente { get; private set; }
        //public string Notas { get; private set; }

        public DateTime DataArquivo { get; set; }
        public bool Integrado { get; set; }
        public DateTime? DataIntegracao { get; set; }


        protected ArquivoModel()
        {

        }

        public ArquivoModel(Guid id, string xML, DateTime dataArquivo, string empresa)
        {
            Id = id;
            XML = xML;
            DataArquivo = dataArquivo;
            Empresa = empresa;
        }
    }
}