using Microsoft.EntityFrameworkCore;
using NStore.Catalogo.API.Models;
using NStore.Core.Data;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NStore.Catalogo.API.Data.Repository
{
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly CatalogoContext catalogoContext;
        public IUnitOfWork UnityOfWork => catalogoContext;
        public ProdutoRepository(CatalogoContext catalogoContext)
        {
            this.catalogoContext = catalogoContext;
        }

        public void Adicionar(Produto produto)
        {
            catalogoContext.Produtos.Add(produto);
        }

        public void Atualizar(Produto produto)
        {
            catalogoContext.Produtos.Update(produto);
        }

        public async Task<Produto> ObterPorId(Guid id)
        {
            return await catalogoContext.Produtos.FindAsync(id);
        }

        public async Task<IEnumerable<Produto>> ObterTodos()
        {
            return await catalogoContext.Produtos.AsNoTracking().ToListAsync();
        }
        public void Dispose()
        {
            catalogoContext?.Dispose();
        }
    }
}
