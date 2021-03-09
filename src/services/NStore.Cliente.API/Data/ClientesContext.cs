using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using NStore.Cliente.API.Extensions;
using NStore.Cliente.API.Models;
using NStore.Core.Data;
using NStore.Core.Mediator;
using NStore.Core.Messages;
using System.Linq;
using System.Threading.Tasks;

namespace NStore.Cliente.API.Data
{
    public class ClientesContext : DbContext, IUnitOfWork
    {
        private readonly IMediatorHandler mediatorHandler;
        public ClientesContext(DbContextOptions<ClientesContext> options, IMediatorHandler mediatorHandler) : base(options)
        {
            this.mediatorHandler = mediatorHandler;
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            ChangeTracker.AutoDetectChangesEnabled = false;
        }

        public DbSet<Models.Cliente> Clientes { get; set; }
        public DbSet<Endereco> Enderecos { get; set; }

        /// <summary>
        /// Métodos para evitar varchar max, caso esqueça de configurar o columntype
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            Ignores(modelBuilder);
            foreach (var property in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetProperties().Where(p => p.ClrType == typeof(string))))
                property.SetColumnType("varchar(100)");

            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys())) 
                relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ClientesContext).Assembly);
        }

        public async Task<bool> Commit()
        {
            var sucesso = await base.SaveChangesAsync() > 0;
            if (sucesso) await mediatorHandler.PublicarEventos(this);

            return sucesso;
        }

        private void Ignores(ModelBuilder modelBuilder) 
        {
            modelBuilder.Ignore<ValidationResult>();
            modelBuilder.Ignore<Event>();
        }
    }
}
