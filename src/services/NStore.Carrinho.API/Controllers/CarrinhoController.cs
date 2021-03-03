using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NStore.Carrinho.API.Data;
using NStore.Carrinho.API.Model;
using NStore.WebApi.Core.Controllers;
using NStore.WebApi.Core.Usuario;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace NStore.Carrinho.API.Controllers
{
    [Authorize]
    public class CarrinhoController : MainController
    {
        private readonly IAspNetUser user;
        private readonly CarrinhoContext context;

        public CarrinhoController(IAspNetUser user, CarrinhoContext context)
        {
            this.user = user;
            this.context = context;
        }

        [HttpGet("carrinho")]
        public async Task<CarrinhoCliente> ObterCarrinho()
        {
            return await ObterCarrinhoCliente() ?? new CarrinhoCliente();
        }

        [HttpPost("carrinho")]
        public async Task<IActionResult> AdicionarItemCarrinho(CarrinhoItem item)
        {
            var carrinho = await ObterCarrinhoCliente();

            if (carrinho == null)
                ManipularNovoCarrinho(item);
            else 
                ManipularCarrinhoExistente(carrinho, item);

            ValidarCarrinho(carrinho);
            if (!IsOperacaoValida()) return CustomResponse();

            await PersistirDados();

            return CustomResponse();
        }


        [HttpPut("carrinho/{produtoId}")]
        public async Task<IActionResult> AtualizarItemCarrinho(Guid produtoId, CarrinhoItem item)
        {
            var carrinho = await ObterCarrinhoCliente();
            var itemCarrinho = await ObterItemCarrinhoValidado(produtoId, carrinho, item);

            if (itemCarrinho == null) return CustomResponse();

            carrinho.AtualizarUnidades(item, item.Quantidade);

            ValidarCarrinho(carrinho);
            if (!IsOperacaoValida()) return CustomResponse();

            context.CarrinhoItens.Update(itemCarrinho);
            context.CarrinhoCliente.Update(carrinho);

            await PersistirDados();

            return CustomResponse();
        }

        [HttpDelete("carrinho/{produtoId}")]
        public async Task<IActionResult> RemoverItemCarrinho(Guid produtoId)
        {
            var carrinho = await ObterCarrinhoCliente();

            var itemCarrinho = await ObterItemCarrinhoValidado(produtoId, carrinho);
            if (itemCarrinho == null) return CustomResponse();

            ValidarCarrinho(carrinho);
            if (!IsOperacaoValida()) return CustomResponse();

            carrinho.RemoverItem(itemCarrinho);
            context.CarrinhoItens.Remove(itemCarrinho);
            context.CarrinhoCliente.Update(carrinho);

            await PersistirDados();
            return CustomResponse();

        }

        private void ManipularNovoCarrinho(CarrinhoItem item)
        {
            var carrinho = new CarrinhoCliente(user.ObterUserId());
            carrinho.AdicionarItem(item);
            ValidarCarrinho(carrinho);
            context.CarrinhoCliente.Add(carrinho);
        }

        private void ManipularCarrinhoExistente(CarrinhoCliente carrinho, CarrinhoItem item)
        {
            var produtoItemExistente = carrinho.CarrinhoItemExistente(item);

            carrinho.AdicionarItem(item);
            ValidarCarrinho(carrinho);
            if (produtoItemExistente)
            {
                context.CarrinhoItens.Update(carrinho.ObterPorProdutoId(item.ProdutoId));
            } else
            {
                context.CarrinhoItens.Add(item);
            }
            context.CarrinhoCliente.Update(carrinho);
        }

        private async Task<CarrinhoCliente> ObterCarrinhoCliente()
        {
            return await context.CarrinhoCliente.Include(c => c.Itens)
                .FirstOrDefaultAsync(c => c.ClienteId == user.ObterUserId());
        }

        private async Task<CarrinhoItem> ObterItemCarrinhoValidado(Guid produtoId, CarrinhoCliente carrinho, CarrinhoItem item = null)
        {
            if (item!= null && produtoId != item.ProdutoId)
            {
                AddErroProcessamento("O item não corresponde ao informado");
                return null;
            }

            if (carrinho == null)
            {
                AddErroProcessamento("Carrinho não encontrado");
                return null;
            }

            var itemCarrinho = await context.CarrinhoItens.FirstOrDefaultAsync(i => i.CarrinhoId == carrinho.Id && i.ProdutoId == produtoId);
            if (itemCarrinho == null || !carrinho.CarrinhoItemExistente(itemCarrinho))
            {
                AddErroProcessamento("O item não está no carrinho");
            }

            return itemCarrinho;
        }

        private async Task PersistirDados()
        {
            var result = await context.SaveChangesAsync();
            if (result <= 0) AddErroProcessamento("Não foi possível persistir os dados no banco");
        }

        private bool ValidarCarrinho(CarrinhoCliente carrinho)
        {
            if (carrinho.EhValido()) return true;

            carrinho.ValidationResult.Errors.ToList()
                .ForEach(erro => AddErroProcessamento(erro.ErrorMessage));

            return false;
        }
    }
}
