using Complejo.API.Controllers.Base;
using Complejo.Application.Commands.User;
using Complejo.Application.Dtos.User;
using Complejo.Application.Queries.User;
using Complejo.Application.Responses;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Complejo.API.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ApiControllerBase
    {
        private readonly IMediator mediator;

        public UserController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost(Name = "Create User")]
        [ProducesResponseType(typeof(string), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserCommand command)
        {
            var response = await mediator.Send(command);
            return CreatedAtAction(nameof(CreateUser), response);
        }

        [HttpPut(Name = "Update User")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> UpdateUser([FromBody] UpdateUserCommand command)
        {
            var response = await mediator.Send(command);
            return Ok(response);
        }

        [HttpDelete("{id}", Name = "Delete User")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var response = await mediator.Send(new DeleteUserCommand { IdUser = id });
            return Ok(response);
        }


        [HttpGet("by-filter", Name = "Get All User By Filter")]
        [ProducesResponseType(typeof(PagedListResponse<List<UserByFilterDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetAllUserByFilter([FromQuery] GetAllUserByFilterQuery query)
        {
            var response = await mediator.Send(query);
            return this.OkIfNotNullNotFoundOtherwise(response);
        }

        [HttpGet("{id}", Name = "Get User By Id")]
        [ProducesResponseType(typeof(UserByIdDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetUserById(string id)
        {
            var response = await mediator.Send(new GetUserByIdQuery { IdUser = id });
            return this.OkIfNotNullNotFoundOtherwise(response);
        }
    }
}
