﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections.Generic;
using System.Linq;

namespace NStore.Identidade.API.Controllers
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
                { "messages", Erros.ToArray() }
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

        protected void AddErroProcessamento(string erro) => Erros.Add(erro);

        protected void LimparErros() => Erros.Clear();
    }
}
