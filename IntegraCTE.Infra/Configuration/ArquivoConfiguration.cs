using IntegraCTE.Core.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IntegraCTE.Infra.Configuration
{
    public class ArquivoConfiguration : IEntityTypeConfiguration<ArquivoModel>
    {
        public void Configure(EntityTypeBuilder<ArquivoModel> builder)
        {
            builder.ToTable("Arquivos");
            builder.HasKey(p => p.Id);

            builder.Property(x => x.XML).HasColumnType("XML").IsRequired();
            builder.Property(x => x.Processado).HasDefaultValue(false);

        }
    }
}
