﻿using static NStore.Identidade.API.Utils.MessagesUtils;
using System.ComponentModel.DataAnnotations;

namespace NStore.Identidade.API.Models
{
    public class UsuarioRegistroModel
    {
        [Required(ErrorMessage = CAMPO_OBRIGATORIO)]
        public string Nome { get; set; }

        [Required(ErrorMessage = CAMPO_OBRIGATORIO)]
        public string Cpf { get; set; }

        [Required(ErrorMessage = CAMPO_OBRIGATORIO)]
        [EmailAddress(ErrorMessage = CAMPO_INVALIDO)]
        public string Email { get; set; }

        [Required(ErrorMessage = CAMPO_OBRIGATORIO)]
        [StringLength(100, ErrorMessage = CAMPO_DEVE_CONTER_X_CARACERES, MinimumLength = 6)]
        public string Senha { get; set; }

        [Compare("Senha", ErrorMessage = "As senhas não conferem.")]
        public string SenhaConfirmacao { get; set; }
    }


}
