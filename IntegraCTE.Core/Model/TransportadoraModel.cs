namespace IntegraCTE.Core.Model
{
    public class TransportadoraModel
    {
        public Guid Id { get; set; }
        public string Cnpj { get; set; }
        public string Nome { get; set; }
        public string CodigoExterno { get; set; }
        public string MetodoPagamento { get; set; }
        public string EspecificacaoMetodoPagamento { get; set; }
        public string CalendarioPagamento { get; set; }

        public List<CTEModel> CTEs { get; set; }

        protected TransportadoraModel()
        {

        }
        public TransportadoraModel(Guid id, string cnpj, string nome, string codigoExterno, string metodoPagamento, string especificacaoMetodoPagamento, string calendarioPagamento)
        {
            Id = id;
            Cnpj = cnpj;
            Nome = nome;
            CodigoExterno = codigoExterno;
            MetodoPagamento = metodoPagamento;
            EspecificacaoMetodoPagamento = especificacaoMetodoPagamento;
            CalendarioPagamento = calendarioPagamento;
        }

        public TransportadoraModel(string cnpj, string nome, string codigoExterno)
        {
            Cnpj = cnpj;
            Nome = nome;
            CodigoExterno = codigoExterno;
        }
    }
}