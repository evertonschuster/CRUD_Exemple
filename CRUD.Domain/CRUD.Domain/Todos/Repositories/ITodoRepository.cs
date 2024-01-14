using CRUD.Domain.Commoms;
using CRUD.Domain.Todos.Models;

namespace CRUD.Domain.Todos.Repositories
{
    public interface ITodoRepository : IRepository
    {
        /// <summary>
        /// Get Todo by Id in the repository
        /// </summary>
        /// <param name="id">Todo Id</param>
        /// <returns>Return a Todo when found</returns>
        Todo? GetById(Guid id);

        /// <summary>
        /// Get all Todos in the repository
        /// </summary>
        /// <returns>Return IEnumerable with Todos</returns>
        IEnumerable<Todo> GetAll();

        /// <summary>
        /// Add or create Todo in the repository
        /// </summary>
        /// <param name="model"></param>
        void Add(Todo model);

        /// <summary>
        /// Update all properties of Todo in the repository
        /// </summary>
        /// <param name="model"></param>
        void Update(Todo model);


        /// <summary>
        /// Delete Todo in the repository
        /// </summary>
        /// <param name="model"></param>
        void Delete(Todo model);

    }
}