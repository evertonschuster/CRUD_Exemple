using CRUD.Domain.Todos.Exceptions;
using CRUD.Domain.Todos.Repositories;

namespace CRUD.Application.Todos.GetByIdTodo
{
    public sealed class GetByIdTodoHandler(ITodoRepository todoRepository) : IRequestHandler<GetByIdTodoQuery, GetByIdTodoResult>
    {
        private readonly ITodoRepository todoRepository = todoRepository;

        public Task<GetByIdTodoResult> Handle(GetByIdTodoQuery request, CancellationToken cancellationToken)
        {
            var model = todoRepository.GetById(request.Id) ?? throw new TodoNotFoundException();
            var result = new GetByIdTodoResult(model);

            return Task.FromResult(result);
        }
    }
}
