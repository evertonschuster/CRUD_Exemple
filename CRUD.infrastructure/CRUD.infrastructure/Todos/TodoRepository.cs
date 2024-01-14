using CRUD.Domain.Todos.Models;
using CRUD.Domain.Todos.Repositories;

namespace CRUD.infrastructure.Todos
{
    internal sealed class TodoRepository(CRUDContext context) : Repository<Todo>(context), ITodoRepository
    {
    }
}
