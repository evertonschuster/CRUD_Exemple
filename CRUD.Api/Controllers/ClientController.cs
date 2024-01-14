using MediatR;
using Microsoft.AspNetCore.Mvc;
using CRUD.Application.Clients.CreateClient;
using CRUD.Application.Clients.DeleteClient;
using CRUD.Application.Clients.GetByIdClient;
using CRUD.Application.Clients.UpdateClient;

namespace CRUD.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public sealed class ClientController(IMediator mediator) : ControllerBase
    {
        public IMediator Mediator { get; set; } = mediator;


        /// <summary>
        /// Get a created client
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(GetByIdClientResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAsync([FromRoute] Guid id)
        {
            var query = new GetByIdClientQuery(id);
            var result = await Mediator.Send(query);
            return Ok(result);
        }

        /// <summary>
        /// Create a new client
        /// </summary>
        /// <param name="command">Client object</param>
        [HttpPost]
        [ProducesResponseType(typeof(CreateClientResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PostAsync([FromBody] CreateClientCommand command)
        {
            var result = await Mediator.Send(command);
            return Ok(result);
        }

        /// <summary>
        /// Update an existing client
        /// </summary>
        /// <param name="id">Client id</param>
        /// <param name="command">Client object</param>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(UpdateClientResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PutAsync(Guid id, [FromBody] UpdateClientCommand command)
        {
            command.Id = id;
            var result = await Mediator.Send(command);
            return Ok(result);
        }

        /// <summary>
        /// Delete a client
        /// </summary>
        /// <param name="id">Client id</param>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var command = new DeleteClientCommand(id);
            await Mediator.Send(command);
            return Ok();
        }
    }
}