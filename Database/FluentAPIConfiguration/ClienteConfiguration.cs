using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models;

namespace Database.FluentAPIConfiguration
{
    public class ClienteConfiguration:IEntityTypeConfiguration<Cliente>
    {
        public void Configure(EntityTypeBuilder<Cliente> builder)
        {
            builder.ToTable("Clientes")
                .HasIndex(c => c.Cpf)
                .IsUnique();
            builder
                .Property(p => p.Cpf)
                .IsRequired();
        }
    }
}