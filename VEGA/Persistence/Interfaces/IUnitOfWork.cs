using Microsoft.EntityFrameworkCore;

namespace Vega.Persistence.Interfaces
{
    public interface IUnitOfWork
    {
        Task CompleteAsync();
    }
}
