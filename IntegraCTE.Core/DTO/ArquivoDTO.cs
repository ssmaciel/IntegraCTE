namespace IntegraCTE.Core.DTO
{
    public class ArquivoDTO
    {
        public Guid Id { get; set; }
        public string XML { get; set; }
        public List<NotaDTO> Notas { get; private set; }

        public void ProcessarXML()
        {

        }
    }
}