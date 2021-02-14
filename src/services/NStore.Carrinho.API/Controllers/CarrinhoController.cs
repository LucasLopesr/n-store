using Microsoft.AspNetCore.Authorization;
using NStore.WebApi.Core.Controllers;


namespace NStore.Carrinho.API.Controllers
{
    [Authorize]
    public class CarrinhoController : MainController
    {
    }
}
