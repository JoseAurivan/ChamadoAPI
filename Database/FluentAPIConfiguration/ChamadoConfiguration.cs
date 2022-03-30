using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models;

namespace Database.FluentAPIConfiguration
{
    public class ChamadoConfiguration:IEntityTypeConfiguration<Chamado>
    {
        public void Configure(EntityTypeBuilder<Chamado> builder)
        {
            builder.ToTable("Chamados")
                .HasIndex(c => c.NumeroProtocolo)
                .IsUnique();
            builder.Property(c => c.NumeroProtocolo)
                .IsRequired();
        }
    }
}