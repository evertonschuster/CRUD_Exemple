using Microsoft.EntityFrameworkCore;
using CRUD.Domain.Commoms;

namespace CRUD.infrastructure
{
    internal class Repository<T>(CRUDContext context) where T : Entity
    {
        private readonly CRUDContext _context = context;

        public T? GetById(Guid id)
        {
            return _context.Set<T>().Find(id);
        }

        public IEnumerable<T> GetAll()
        {
            return _context.Set<T>().AsNoTracking();
        }

        public void Add(T entity)
        {
            _context.Set<T>().Add(entity);
        }

        public void Update(T entity)
        {
            _context.Set<T>().Update(entity);
        }

        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }
    }
}