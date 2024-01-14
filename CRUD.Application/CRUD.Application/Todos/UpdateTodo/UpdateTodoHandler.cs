using CRUD.Domain.Commoms;
using CRUD.Domain.Todos.Exceptions;
using CRUD.Domain.Todos.Repositories;

namespace CRUD.Application.Todos.UpdateTodo
{
    public sealed class UpdateTodoHandler(ITodoRepository todoRepository, IUnitOfWork unitOfWork) : IRequestHandler<UpdateTodoCommand, UpdateTodoResult>
    {
        private readonly ITodoRepository todoRepository = todoRepository;
        private readonly IUnitOfWork unitOfWork = unitOfWork;

        public Task<UpdateTodoResult> Handle(UpdateTodoCommand request, CancellationToken cancellationToken)
        {
            var model = todoRepository.GetById(request.Id) ?? throw new TodoNotFoundException();

            model.Update(request.Title, request.Description, request.ClientId);
            todoRepository.Update(model);
            cancellationToken.ThrowIfCancellationRequested();
            unitOfWork.SaveChanges();

            return Task.FromResult(new UpdateTodoResult(model));
        }
    }
}
