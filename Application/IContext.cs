using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Models;

namespace Application
{
    public interface IContext
    {
        public DbSet<Atendente> Atendentes { get; }
        public DbSet<Chamado> Chamados { get; }
        public DbSet<Cliente> Clientes { get; }

        Task SaveChangesAsync();
        EntityEntry<TEntity> Entry<TEntity>(TEntity entry) where TEntity: class;
        EntityEntry Entry(object entry);
    }
}