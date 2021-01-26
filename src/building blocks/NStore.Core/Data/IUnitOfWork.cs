using System.Threading.Tasks;

namespace NStore.Core.Data
{
    public interface IUnitOfWork
    {
        Task<bool> Commit();
    }
}
