namespace IntegraCTE.Core.Entity
{
    public class CTE
    {
        public Guid Id { get; set; }
        public string XML { get; set; }
        public List<Nota> Notas { get; set; }

    }
}