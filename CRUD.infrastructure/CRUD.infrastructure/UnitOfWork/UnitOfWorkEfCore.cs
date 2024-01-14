using CRUD.Domain.Commoms;

namespace CRUD.infrastructure.UnitOfWork
{
    internal sealed class UnitOfWorkEfCore(CRUDContext context) : IUnitOfWork
    {
        private readonly CRUDContext _context = context;

        public void Dispose()
        {
        }

        public Task<int> SaveChangesAsync()
        {
            return _context.SaveChangesAsync();
        }

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }
    }
}
