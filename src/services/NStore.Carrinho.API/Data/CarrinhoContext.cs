using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using NStore.Carrinho.API.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NStore.Carrinho.API.Data
{
    public sealed class CarrinhoContext : DbContext
    {
        public DbSet<CarrinhoItem> CarrinhoItens { get; set; }
        public DbSet<CarrinhoCliente> CarrinhoCliente { get; set; }

        public CarrinhoContext(DbContextOptions<CarrinhoContext> options) : base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            ChangeTracker.AutoDetectChangesEnabled = false;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var property in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetProperties().Where(p => p.ClrType == typeof(string))))
                property.SetColumnType("varchar(100)");

            modelBuilder.Ignore<ValidationResult>();

            modelBuilder.Entity<CarrinhoCliente>()
                .HasIndex(c => c.ClienteId)
                .HasDatabaseName("IDX_Cliente");

            modelBuilder.Entity<CarrinhoCliente>()
                .Property(c => c.ValorTotal).HasPrecision(18,2);

            modelBuilder.Entity<CarrinhoCliente>()
                .HasMany(c => c.Itens)
                .WithOne(i => i.CarrinhoCliente);

            modelBuilder.Entity<CarrinhoItem>()
                .Property(c => c.Valor).HasPrecision(18, 2);

            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys())) relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;

        }
    }
}
