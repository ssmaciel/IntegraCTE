namespace IntegraCTE.Core.Model
{
    public class ArquivoModel
    {
        public Guid Id { get; set; }
        public string XML { get; set; }
        public bool Processado { get; set; }
        public string Empresa { get; set; }

        public DateTime DataArquivo { get; set; }
        public bool Integrado { get; set; }
        public DateTime? DataIntegracao { get; set; }
        public List<ValidacaoModel> Validacoes { get; set; }


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