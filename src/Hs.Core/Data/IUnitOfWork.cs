using System.Threading.Tasks;

namespace Hs.Core.Data
{
    public interface IUnitOfWork
    {
        Task<bool> Commit();
    }
}
