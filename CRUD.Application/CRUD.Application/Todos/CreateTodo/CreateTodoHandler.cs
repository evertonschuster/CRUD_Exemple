using CRUD.Domain.Commoms;
using CRUD.Domain.Todos.Repositories;

namespace CRUD.Application.Todos.CreateTodo
{
    public sealed class CreateTodoHandler(ITodoRepository todoRepository, IUnitOfWork unitOfWork) : IRequestHandler<CreateTodoCommand, CreateTodoResult>
    {
        private readonly ITodoRepository todoRepository = todoRepository;
        private readonly IUnitOfWork unitOfWork = unitOfWork;

        public Task<CreateTodoResult> Handle(CreateTodoCommand request, CancellationToken cancellationToken)
        {
            var model = request.ToModel();

            todoRepository.Add(model);
            cancellationToken.ThrowIfCancellationRequested();
            unitOfWork.SaveChanges();

            return Task.FromResult(new CreateTodoResult(model.Id));
        }
    }
}
