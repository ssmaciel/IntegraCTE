namespace IntegraCTE.Core.Model
{
    public class CTEModel
    {
        public Guid Id { get; private set; }
        public string Notas { get; private set; }
        public string Site { get; private set; }

        public string NumeroCte { get; private set; }

        public string SerieCte { get; private set; }

        public string ChaveAcessoCte { get; private set; }

        public string? OrdemCompra { get; private set; }

        public Guid TransportadoraID { get; set; }
        public TransportadoraModel Transportadora { get; set; }


        public string DestinatarioCNPJCPF { get; set; }
        public string DestinatarioNome { get; set; }
        public string DestinatarioLogradouro { get; set; }
        public string DestinatarioNro { get; set; }
        public string DestinatarioBairro { get; set; }
        public string DestinatarioCodigoMunicipio { get; set; }
        public string DestinatarioMunicipio { get; set; }
        public string DestinatarioCEP { get; set; }
        public string DestinatarioUF { get; set; }
        public string DestinatarioCodigoPais { get; set; }
        public string DestinatarioPais { get; set; }



        public DateTime DataHoraCriacao { get; private set; }


        public string CNPJRemetente { get; private set; }

        public string CNPJEntidadeLegal { get; private set; }

        public string ValorCte { get; private set; }

        public string ModeloCte { get; private set; }

        public string Justificativa { get; private set; }

        public DateTime DataEmissao { get; private set; }

        public string TomadorServico { get; private set; }

        public string NotaFiscal { get; set; }
        public string ChaveNotaFiscal { get; set; }

        public string CFOP { get; set; }

        public string UFEnv { get; set; }

        public string UFEmitente { get; set; }

        public string UFRemetente { get; set; }

        public string ItemNumber { get; set; }
        public int LineNumber { get; set; }
        public decimal PurchasePrice { get; set; }
        public int PurchasePriceQuantity { get; set; }
        public string CFOPCode { get; set; }
        public string dataAreaId { get; set; }


        public DateTime DataArquivo { get; set; }
        public bool Integrado { get; set; }
        public DateTime? DataIntegracao { get; set; }


    }
}