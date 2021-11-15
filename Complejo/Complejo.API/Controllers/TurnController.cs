using Complejo.API.Controllers.Base;
using Complejo.Application.Commands.Turn;
using Complejo.Application.Dtos.Turn;
using Complejo.Application.Dtos.UiControls;
using Complejo.Application.Queries.Turn;
using Complejo.Application.Responses;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Complejo.API.Controllers
{
    [Route("api/turn")]
    [ApiController]
    public class TurnController : ApiControllerBase
    {
        private readonly IMediator mediator;

        public TurnController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost(Name = "Create Turn")]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> CreateTurn([FromBody] CreateTurnCommand command)
        {
            var response = await mediator.Send(command);
            return CreatedAtAction(nameof(CreateTurn), response);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}", Name = "Delete Turn")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> DeleteTurn(Guid? id)
        {
            var response = await mediator.Send(new DeleteTurnCommand { IdTurn = id });
            return Ok(response);
        }



        [HttpGet("by-filter", Name = "Get All Turn By Filter")]
        [ProducesResponseType(typeof(PagedListResponse<List<TurnByFilterDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetTurnByFilter([FromQuery] GetAllTurnByFilterQuery query)
        {
            var response = await mediator.Send(query);
            return this.OkIfNotNullNotFoundOtherwise(response);
        }

        [HttpGet("available-field-by-date-time", Name = "Get All Available Field By Date And Time")]
        [ProducesResponseType(typeof(List<ComboBoxDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetAllAvailableFieldByDateTime([FromQuery] GetAllAvailableFieldByDateTimeQuery query)
        {
            var response = await mediator.Send(query);
            return this.OkIfNotNullNotFoundOtherwise(response);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("today", Name = "Get All Turns For Today")]
        [ProducesResponseType(typeof(PagedListResponse<List<TurnByFilterDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetTurnsForToday([FromQuery] GetAllTurnsForTodayQuery query)
        {
            var response = await mediator.Send(query);
            return this.OkIfNotNullNotFoundOtherwise(response);
        }

        [HttpGet("by-client", Name = "Get All Turns By Client")]
        [ProducesResponseType(typeof(PagedListResponse<List<TurnByFilterDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetTurnsByClient([FromQuery] GetAllTurnsByClientQuery query)
        {
            var response = await mediator.Send(query);
            return this.OkIfNotNullNotFoundOtherwise(response);
        }

        [HttpGet("{id}", Name = "Get Turn By Id")]
        [ProducesResponseType(typeof(TurnByIdDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetTurndById(Guid id)
        {
            var response = await mediator.Send(new GetTurnByIdQuery { IdTurn = id });
            return this.OkIfNotNullNotFoundOtherwise(response);
        }
    }
}
