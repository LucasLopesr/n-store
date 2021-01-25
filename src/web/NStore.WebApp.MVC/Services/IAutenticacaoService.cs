using NStore.WebApp.MVC.Models;
using System.Threading.Tasks;

namespace NStore.WebApp.MVC.Services
{
    public interface IAutenticacaoService
    {
        Task<UsuarioAutenticacaoResponse> Login(UsuarioLoginViewModel usuario);
        Task<UsuarioAutenticacaoResponse> Registrar(UsuarioRegistroViewModel usuario);
    }
}
