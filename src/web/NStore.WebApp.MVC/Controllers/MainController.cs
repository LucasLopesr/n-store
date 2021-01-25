using Microsoft.AspNetCore.Mvc;
using NStore.WebApp.MVC.Models.Errors;
using System.Linq;

namespace NStore.WebApp.MVC.Controllers
{
    public class MainController : Controller
    {
        protected bool ResponsePossuiErros(ResponseResult response) 
        {
            if (response != null && response.Errors.Mensagens.Any()) 
            {
                foreach (var mensagem in response.Errors.Mensagens)
                    ModelState.AddModelError(string.Empty, mensagem);

                return true;
            }

            return false;
        }
    }
}
