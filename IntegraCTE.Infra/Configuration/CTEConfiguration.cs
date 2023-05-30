using IntegraCTE.Core.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IntegraCTE.Infra.Configuration
{
    public class CTEConfiguration : IEntityTypeConfiguration<CTEModel>
    {
        public void Configure(EntityTypeBuilder<CTEModel> builder)
        {
            builder.ToTable("CTEs");
            builder.HasKey(p => p.Id);
            builder.Property(x => x.ChaveNotaFiscal).HasColumnType("VARCHAR(MAX)");
            builder.Property(x => x.NotaFiscal).HasColumnType("VARCHAR(MAX)");
            builder.Property(x => x.Notas).HasColumnType("VARCHAR(MAX)");
            builder.Property(x => x.DestinatarioCodigoMunicipio).IsRequired(false);
            builder.Property(x => x.DestinatarioCodigoPais).IsRequired(false);
            builder.Property(x => x.DestinatarioPais).IsRequired(false);

            builder.HasOne(x => x.Transportadora).WithMany(x => x.CTEs)
        .OnDelete(DeleteBehavior.Restrict);
            

        }
    }
}
