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

            builder.HasOne(x => x.Transportadora).WithMany(x => x.CTEs)
        .OnDelete(DeleteBehavior.Restrict);
            

        }
    }
}
