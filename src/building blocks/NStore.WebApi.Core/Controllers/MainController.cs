using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using NStore.Core.Communication;
using System.Collections.Generic;
using System.Linq;

namespace NStore.WebApi.Core.Controllers
{
    [ApiController]
    public abstract class MainController : Controller
    {
        protected ICollection<string> Erros = new List<string>();
        protected ActionResult CustomResponse(object result = null)
        {
            if (IsOperacaoValida())
                return Ok(result);

            return BadRequest(new ValidationProblemDetails(new Dictionary<string, string[]>
            {
                { "Mensagens", Erros.ToArray() }
            }));
        }

        protected bool IsOperacaoValida()
        {
            return !Erros.Any();
        }

        protected ActionResult CustomResponse(ModelStateDictionary modelState)
        {
            var erros = modelState.Values.SelectMany(erro => erro.Errors);
            foreach (var erro in erros)
                AddErroProcessamento(erro.ErrorMessage);

            return CustomResponse();
        }

        protected ActionResult CustomResponse(ValidationResult validationResult)
        {
            foreach (var erro in validationResult.Errors)
                AddErroProcessamento(erro.ErrorMessage);

            return CustomResponse();
        }


        protected ActionResult CustomResponse(ResponseResult resposta)
        {
            ResponsePossuiErros(resposta);

            return CustomResponse();
        }

        protected bool ResponsePossuiErros(ResponseResult resposta)
        {
            if (resposta == null || !resposta.AnyErrors()) return false;

            resposta.Errors.Mensagens.ForEach(mensagem => AddErroProcessamento(mensagem));
            return true;
        }

        protected void AddErroProcessamento(string erro) => Erros.Add(erro);

        protected void LimparErros() => Erros.Clear();
    }
}
