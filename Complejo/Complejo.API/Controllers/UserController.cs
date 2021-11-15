using Complejo.API.Controllers.Base;
using Complejo.Application.Commands.User;
using Complejo.Application.Dtos.User;
using Complejo.Application.Queries.User;
using Complejo.Application.Responses;
using MediatR;
using Microsoft.AspNetCore.Authorization;
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

        [Authorize(Roles = "Admin")]
        [HttpPost(Name = "Create User")]
        [ProducesResponseType(typeof(string), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> CreateUser([FromBody] CreateAdminUserCommand command)
        {
            var response = await mediator.Send(command);
            return CreatedAtAction(nameof(CreateUser), response);
        }

        [Authorize(Roles = "Admin")]
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

        [Authorize(Roles = "Admin")]
        [HttpGet("by-filter", Name = "Get All User By Filter")]
        [ProducesResponseType(typeof(PagedListResponse<List<UserByFilterDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetAllUserByFilter([FromQuery] GetAllAdminUserByFilterQuery query)
        {
            var response = await mediator.Send(query);
            return this.OkIfNotNullNotFoundOtherwise(response);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("reset-password")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordCommand command)
        {
            var response = await mediator.Send(command);
            return Ok();
        }
    }
}
