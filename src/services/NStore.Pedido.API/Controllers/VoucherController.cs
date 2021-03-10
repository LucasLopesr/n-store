using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NStore.Pedidos.API.Application.DTO;
using NStore.Pedidos.API.Application.Queries;
using NStore.WebApi.Core.Controllers;

namespace NStore.Pedidos.API.Controllers
{
    [Authorize]
    public class VoucherController : MainController
    {
        private readonly IVoucherQueries voucherQueries;

        public VoucherController(IVoucherQueries voucherQueries)
        {
            this.voucherQueries = voucherQueries;
        }

        [HttpGet("vouchers/{codigo}")]
        [ProducesResponseType(typeof(VoucherDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> ObterPorCodigo(string codigo)
        {
            if (string.IsNullOrWhiteSpace(codigo)) return NotFound();

            var voucher = await voucherQueries.ObterVoucherPorCodigo(codigo);

            return voucher == null ? NotFound() : CustomResponse(voucher);
        }
    }
}
