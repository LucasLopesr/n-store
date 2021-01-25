using System.ComponentModel.DataAnnotations;

using static NStore.Identidade.API.Utils.MessagesUtils;

namespace NStore.Identidade.API.Models
{
    public class UsuarioLoginModel
    {
        [Required(ErrorMessage = CAMPO_OBRIGATORIO)]
        [EmailAddress(ErrorMessage = CAMPO_INVALIDO)]
        public string Email { get; set; }
        [Required(ErrorMessage = CAMPO_OBRIGATORIO)]
        [StringLength(100, ErrorMessage = CAMPO_DEVE_CONTER_X_CARACERES, MinimumLength = 6)]
        public string Senha { get; set; }
    }
}
