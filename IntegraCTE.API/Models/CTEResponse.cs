namespace IntegraCTE.API.Models
{
    public class CTEResponse
    {
        public Guid Id { get; set; }
        public string Transportadora { get; set; }
        public string Site { get; set; }
        public string NFe { get; set; }
        public string CTe { get; set; }
        public decimal Valor { get; set; }
        public string DataImportacao { get; set; }
        public string OrdemCompra { get; set; }
        public string Status { get; set; }
    }
}
