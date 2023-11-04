using IntegraCTE.Core.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IntegraCTE.Infra.Configuration
{
    public class ValidacaoConfiguration : IEntityTypeConfiguration<ValidacaoModel>
    {
        public void Configure(EntityTypeBuilder<ValidacaoModel> builder)
        {
            builder.ToTable("Validacao");
            builder.HasKey(p => p.Id);

            builder.Property(x => x.Mensagem).HasColumnType("TEXT");

            builder.HasOne(x => x.Arquivo)
                .WithMany(x => x.Validacoes)
                .HasPrincipalKey(x => x.Id)
                .HasForeignKey(x => x.IdArquivo);

        }
    }
}
