namespace IntegraCTE.Core.Model
{
    public class TransportadoraModel
    {
        public Guid Id { get; set; }
        public string Cnpj { get; set; }
        public string Nome { get; set; }
        public string CodigoExterno { get; set; }

        public List<CTEModel> CTEs { get; set; }

        protected TransportadoraModel()
        {

        }
        public TransportadoraModel(Guid id, string cnpj, string nome, string codigoExterno)
        {
            Id = id;
            Cnpj = cnpj;
            Nome = nome;
            CodigoExterno = codigoExterno;
        }

        public TransportadoraModel(string cnpj, string nome, string codigoExterno)
        {
            Cnpj = cnpj;
            Nome = nome;
            CodigoExterno = codigoExterno;
        }
    }
}