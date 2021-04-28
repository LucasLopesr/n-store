using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NStore.Core.Data;
using NStore.Core.Mediator;
using NStore.Core.Messages;
using NStore.Pedidos.Domain.Pedidos;
using NStore.Pedidos.Domain.Vouchers;
using NStore.Pedidos.Infra.Extensions;

namespace NStore.Pedidos.Infra.Data
{
    public class PedidosContext : DbContext, IUnitOfWork
    {
        private readonly IMediatorHandler mediatorHandler;
        public PedidosContext(
            DbContextOptions<PedidosContext> options
            , IMediatorHandler mediatorHandler
            ) : base(options)
        {
            this.mediatorHandler = mediatorHandler;
        }
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<PedidoItem> PedidoItems { get; set; }
        public DbSet<Voucher> Vouchers { get; set; }


        /// <summary>
        /// Métodos para evitar varchar max, caso esqueça de configurar o columntype
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            Ignores(modelBuilder);
            foreach (var property in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetProperties().Where(p => p.ClrType == typeof(string))))
                property.SetColumnType("varchar(100)");

           
            Ignores(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(PedidosContext).Assembly);

            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;
            }

            modelBuilder.HasSequence<int>("MinhaSequencia").StartsAt(1000).IncrementsBy(1);
            base.OnModelCreating(modelBuilder);

        }

        public async Task<bool> Commit()
        {

            foreach (var entry in ChangeTracker.Entries().Where(entry => entry.Entity.GetType().GetProperty("DataCadastro") != null))
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Property("DataCadastro").CurrentValue = DateTime.Now;
                }

                if (entry.State == EntityState.Modified)
                {
                    entry.Property("DataCadastro").IsModified = false;
                }
            }

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

