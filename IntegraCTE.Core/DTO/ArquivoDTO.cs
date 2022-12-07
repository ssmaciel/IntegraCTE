using System.Xml;

namespace IntegraCTE.Core.DTO
{
    public class ArquivoDTO
    {
        public ArquivoDTO(string xML)
        {
            Id = Guid.NewGuid();
            XML = xML;
        }

        public Guid Id { get; set; }
        public string XML { get; set; }

    }
}