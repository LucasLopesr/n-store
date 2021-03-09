using static NStore.Identidade.API.Utils.MessagesUtils;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using NStore.WebApp.MVC.Extensions.Attributes;

namespace NStore.WebApp.MVC.Models
{
    public class UsuarioRegistroViewModel
    {
        [Required(ErrorMessage = CAMPO_OBRIGATORIO)]
        [DisplayName("Nome Completo")]
        public string Nome { get; set; }

        [Required(ErrorMessage = CAMPO_OBRIGATORIO)]
        [DisplayName("CPF")]
        [Cpf]
        public string Cpf { get; set; }

        [Required(ErrorMessage = CAMPO_OBRIGATORIO)]
        [EmailAddress(ErrorMessage = CAMPO_INVALIDO)]
        [DisplayName("E-mail")]
        public string Email { get; set; }

        [Required(ErrorMessage = CAMPO_OBRIGATORIO)]
        [StringLength(100, ErrorMessage = CAMPO_DEVE_CONTER_X_CARACERES, MinimumLength = 6)]
        [DisplayName("Senha")]
        public string Senha { get; set; }

        [Compare(nameof(Senha), ErrorMessage = "As senhas não conferem.")]
        [DisplayName("Repita sua senha")]
        public string SenhaConfirmacao { get; set; }
    }


}
