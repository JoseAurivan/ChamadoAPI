using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models;

namespace Database.FluentAPIConfiguration
{
    public class AtendenteConfiguration:IEntityTypeConfiguration<Atendente>
    {
        public void Configure(EntityTypeBuilder<Atendente> builder)
        {
            builder.ToTable("Atendente")
                .HasIndex(p => p.Cpf)
                .IsUnique();
            builder
                .Property(p => p.Cpf)
                .IsRequired();
        }
    }
    
}