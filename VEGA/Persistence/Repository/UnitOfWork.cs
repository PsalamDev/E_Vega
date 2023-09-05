using Vega.Persistence.Interfaces;

namespace Vega.Persistence.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly VegaDbContext _vegaDbContext;
        public UnitOfWork(VegaDbContext vegaDbContext)
        {
            _vegaDbContext = vegaDbContext;
        }


        public async Task CompleteAsync()
        {
            await _vegaDbContext.SaveChangesAsync();
        }
    }
}
