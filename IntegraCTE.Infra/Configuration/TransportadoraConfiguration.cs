using IntegraCTE.Core.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegraCTE.Infra.Configuration
{
    public class TransportadoraConfiguration : IEntityTypeConfiguration<TransportadoraModel>
    {
        public void Configure(EntityTypeBuilder<TransportadoraModel> builder)
        {
            builder.ToTable("Transportadoras");
            builder.HasKey(p => p.Id);

            builder.Property(x => x.Cnpj).HasMaxLength(14).IsRequired();
            builder.Property(x => x.Nome).HasMaxLength(60).IsRequired();
            builder.HasMany(x => x.CTEs).WithOne(x => x.Transportadora);
            builder.Navigation(b => b.CTEs)
                .UsePropertyAccessMode(PropertyAccessMode.Property);

        }
    }
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
    public class CTEConfiguration : IEntityTypeConfiguration<CTEModel>
    {
        public void Configure(EntityTypeBuilder<CTEModel> builder)
        {
            builder.ToTable("CTEs");
            builder.HasKey(p => p.Id);

            builder.HasOne(x => x.Transportadora).WithMany(x => x.CTEs);
            

        }
    }
}
