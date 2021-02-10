using Microsoft.AspNetCore.Mvc;
using NStore.Cliente.API.Application.Commands;
using NStore.Core.Mediator;
using NStore.WebApi.Core.Controllers;
using System;
using System.Threading.Tasks;

namespace NStore.Cliente.API.Controllers
{
    public class ClientesController : MainController
    {
        private readonly IMediatorHandler mediatorHandler;

        public ClientesController(IMediatorHandler mediatorHandler)
        {
            this.mediatorHandler = mediatorHandler;
        }

        [HttpGet("clientes")]
        public async Task<IActionResult> Index() 
        {

            var response = await mediatorHandler.EnviarComando(new RegistrarClienteCommand(Guid.NewGuid(), "Lucas", "lucas@Lucas.com", "08116258931"));

            return CustomResponse(response);
        }
    }
}
