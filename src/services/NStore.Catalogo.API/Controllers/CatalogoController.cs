using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NStore.Catalogo.API.Models;
using NStore.WebApi.Core.Controllers;
using NStore.WebApi.Core.Identidade;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NStore.Catalogo.API.Controllers
{

    [Authorize]
    public class CatalogoController : MainController
    {
        private readonly IProdutoRepository produtoRepository;

        public CatalogoController(IProdutoRepository produtoRepository)
        {
            this.produtoRepository = produtoRepository;
        }
        [AllowAnonymous]
        [HttpGet("catalogo/produtos")]
        public async Task<IEnumerable<Produto>> Produtos() 
        {
            return await produtoRepository.ObterTodos();
        }

        [ClaimsAuthorize("Catalogo","Ler")]
        [HttpGet("catalogo/produtos/{id}")]
        public async Task<Produto> ProdutoDetalhe(Guid id)
        {
            return await produtoRepository.ObterPorId(id);
        }
    }
}
