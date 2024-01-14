using CRUD.Domain.Commoms;
using CRUD.Domain.Todos.Exceptions;
using CRUD.Domain.Todos.Repositories;

namespace CRUD.Application.Todos.DeleteTodo
{
    public sealed class DeleteTodoHandler(ITodoRepository todoRepository, IUnitOfWork unitOfWork) : IRequestHandler<DeleteTodoCommand>
    {
        private readonly ITodoRepository todoRepository = todoRepository;
        private readonly IUnitOfWork unitOfWork = unitOfWork;

        public Task Handle(DeleteTodoCommand request, CancellationToken cancellationToken)
        {
            var model = todoRepository.GetById(request.Id) ?? throw new TodoNotFoundException();

            model.Delete();
            todoRepository.Update(model);
            cancellationToken.ThrowIfCancellationRequested();
            unitOfWork.SaveChanges();

            return Task.CompletedTask;
        }
    }
}
