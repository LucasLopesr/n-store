using NStore.Core.Communication;

namespace NStore.WebApp.MVC.Models
{
    public class UsuarioAutenticacaoResponse
    {
        public string AccessToken { get; set; }
        public double ExpiresIn { get; set; }
        public UsuarioToken UsuarioToken { get; set; }
        public ResponseResult ResponseResult { get; set; }
    }
}
