using System.Threading.Tasks;
using Application;
using Database.FluentAPIConfiguration;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Database
{
    public class Context:DbContext, IContext
    {
        public Context()
        {
            
        }
        public Context(DbContextOptions<Context> options) : base(options)
        {
            
        }
        public DbSet<Atendente> Atendentes { get; set; }
        public DbSet<Chamado> Chamados { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public Task SaveChangesAsync() => SaveChangesAsync(default);
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ChamadoConfiguration());
            modelBuilder.ApplyConfiguration(new AtendenteConfiguration());
            modelBuilder.ApplyConfiguration(new ClienteConfiguration());
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        { 
            if (optionsBuilder.IsConfigured) return;
            optionsBuilder.UseSqlServer("Server=localhost,1483;Database=db_Chamado;User Id=sa;Password=1Secure*Password1;");
        }
    }
}