namespace CRUD.Application.Todos.GetByIdTodo
{
    public sealed class GetByIdTodoQuery : IRequest<GetByIdTodoResult>
    {
        public GetByIdTodoQuery()
        {
        }

        public GetByIdTodoQuery(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }
    }
}
