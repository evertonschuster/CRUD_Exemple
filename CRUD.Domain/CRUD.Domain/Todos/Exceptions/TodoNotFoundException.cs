using CRUD.Domain.Commoms;

namespace CRUD.Domain.Todos.Exceptions
{
    public sealed class TodoNotFoundException : BusinessExeption
    {
        public TodoNotFoundException() : base("Todo not found.", string.Empty)
        {
        }
    }
}
