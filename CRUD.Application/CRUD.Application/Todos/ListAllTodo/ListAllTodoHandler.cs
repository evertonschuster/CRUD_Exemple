using CRUD.Domain.Todos.Repositories;

namespace CRUD.Application.Todos.ListAllTodo
{
    public sealed class ListAllTodoHandler(ITodoRepository todoRepository) : IRequestHandler<ListAllTodoQuery, List<ListAllTodoResult>>
    {
        private readonly ITodoRepository todoRepository = todoRepository;

        public Task<List<ListAllTodoResult>> Handle(ListAllTodoQuery request, CancellationToken cancellationToken)
        {
            var models = todoRepository.GetAll();
            var result = models
                .Select(model => new ListAllTodoResult(model))
                .ToList();

            return Task.FromResult(result);
        }
    }
}
