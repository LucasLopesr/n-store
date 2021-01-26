using Microsoft.EntityFrameworkCore;
using NStore.Catalogo.API.Models;
using NStore.Core.Data;
using System.Linq;
using System.Threading.Tasks;

namespace NStore.Catalogo.API.Data
{
    public class CatalogoContext : DbContext, IUnitOfWork
    {
        public DbSet<Produto> Produtos { get; set; }
        public CatalogoContext(DbContextOptions<CatalogoContext> options)
            :base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) 
        {
            SetarDefaulTypeVarchar(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CatalogoContext).Assembly);
        }

        /// <summary>
        /// Métodos para evitar varchar max, caso esqueça de configurar o columntype
        /// </summary>
        /// <param name="modelBuilder"></param>
        private void SetarDefaulTypeVarchar(ModelBuilder modelBuilder) 
        {
            foreach (var property in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetProperties().Where(p => p.ClrType == typeof(string))))
                property.SetColumnType("varchar(100)");
        }

        public async Task<bool> Commit()
        {
            return await base.SaveChangesAsync() > 0;
        }
    }
}
