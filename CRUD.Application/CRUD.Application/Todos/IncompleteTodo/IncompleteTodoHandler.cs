using CRUD.Domain.Commoms;
using CRUD.Domain.Todos.Exceptions;
using CRUD.Domain.Todos.Repositories;

namespace CRUD.Application.Todos.IncompleteTodo
{
    public sealed class IncompleteTodoHandler(ITodoRepository todoRepository, IUnitOfWork unitOfWork) : IRequestHandler<IncompleteTodoCommand>
    {
        private readonly ITodoRepository todoRepository = todoRepository;
        private readonly IUnitOfWork unitOfWork = unitOfWork;

        public Task Handle(IncompleteTodoCommand request, CancellationToken cancellationToken)
        {
            var model = todoRepository.GetById(request.Id) ?? throw new TodoNotFoundException();
            model.Incomplete();

            todoRepository.Update(model);
            cancellationToken.ThrowIfCancellationRequested();
            unitOfWork.SaveChanges();

            return Task.CompletedTask;
        }
    }
}
