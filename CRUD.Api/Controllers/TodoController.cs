using MediatR;
using Microsoft.AspNetCore.Mvc;
using CRUD.Application.Todos.CloseTodo;
using CRUD.Application.Todos.CompleteTodo;
using CRUD.Application.Todos.CreateTodo;
using CRUD.Application.Todos.DeleteTodo;
using CRUD.Application.Todos.GetByIdTodo;
using CRUD.Application.Todos.IncompleteTodo;
using CRUD.Application.Todos.ListAllTodo;
using CRUD.Application.Todos.UpdateTodo;

namespace CRUD.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public sealed class TodoController(IMediator mediator) : ControllerBase
    {
        public IMediator Mediator { get; set; } = mediator;


        /// <summary>
        /// Get a created todo
        /// </summary>
        /// <param name="id">Todo object</param>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(GetByIdTodoResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAsync([FromRoute] Guid id)
        {
            var query = new GetByIdTodoQuery(id);
            var result = await Mediator.Send(query);
            return Ok(result);
        }

        /// <summary>
        /// Create a new todo
        /// </summary>
        /// <param name="command">Todo object</param>
        [HttpPost]
        [ProducesResponseType(typeof(CreateTodoResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PostAsync([FromBody] CreateTodoCommand command)
        {
            var result = await Mediator.Send(command);
            return Ok(result);
        }

        /// <summary>
        /// Update an existing todo
        /// </summary>
        /// <param name="id">Todo id</param>
        /// <param name="command">Todo object</param>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(UpdateTodoResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PutAsync(Guid id, [FromBody] UpdateTodoCommand command)
        {
            command.Id = id;
            var result = await Mediator.Send(command);
            return Ok(result);
        }

        /// <summary>
        /// Delete a todo
        /// </summary>
        /// <param name="id">Todo id</param>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var command = new DeleteTodoCommand(id);
            await Mediator.Send(command);
            return Ok();
        }

        /// <summary>
        /// Close a todo
        /// </summary>
        /// <param name="id">Todo id</param>
        [HttpPut("{id}/close")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CloseAsync(Guid id)
        {
            var command = new CloseTodoCommand(id);
            await Mediator.Send(command);
            return Ok();
        }

        /// <summary>
        /// Complete a todo
        /// </summary>
        /// <param name="id">Todo id</param>
        [HttpPut("{id}/complete")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CompleteAsync(Guid id)
        {
            var command = new CompleteTodoCommand(id);
            await Mediator.Send(command);
            return Ok();
        }

        /// <summary>
        /// Incomplete a todo
        /// </summary>
        /// <param name="id">Todo id</param>
        [HttpPut("{id}/incomplete")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> IncompleteAsync(Guid id)
        {
            var command = new IncompleteTodoCommand(id);
            await Mediator.Send(command);
            return Ok();
        }

        /// <summary>
        /// List All todo
        /// </summary>
        [HttpGet("list-all")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ListAllAsync()
        {
            var results = await Mediator.Send(new ListAllTodoQuery());
            return Ok(results);
        }
    }
}