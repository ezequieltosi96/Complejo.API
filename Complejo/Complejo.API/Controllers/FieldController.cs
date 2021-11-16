using Complejo.API.Controllers.Base;
using Complejo.Application.Commands.Field;
using Complejo.Application.Dtos.Field;
using Complejo.Application.Dtos.UiControls;
using Complejo.Application.Queries.Field;
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
    [Route("api/field")]
    [ApiController]
    public class FieldController : ApiControllerBase
    {
        private readonly IMediator mediator;

        public FieldController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost(Name = "Create Field")]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> CreateField([FromBody] CreateFieldCommand command)
        {
            var response = await mediator.Send(command);
            return CreatedAtAction(nameof(CreateField), response);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut(Name = "Update Field")]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> UpdateField([FromBody] UpdateFieldCommand command)
        {
            var response = await mediator.Send(command);
            return Ok(response);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}", Name = "Delete Field")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> DeleteField(Guid? id)
        {
            var response = await mediator.Send(new DeleteFieldCommand { IdField = id });
            return Ok(response);
        }

        [HttpGet("by-filter", Name = "Get All Field By Filter")]
        [ProducesResponseType(typeof(PagedListResponse<List<FieldByFilterDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetFieldByFilter([FromQuery] GetAllFieldByFilterQuery query)
        {
            var response = await mediator.Send(query);
            return this.OkIfNotNullNotFoundOtherwise(response);
        }

        [HttpGet("{id}", Name = "Get Field By Id")]
        [ProducesResponseType(typeof(FieldByIdDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetFieldById(Guid id)
        {
            var response = await mediator.Send(new GetFieldByIdQuery { IdField = id });
            return this.OkIfNotNullNotFoundOtherwise(response);
        }

        [HttpGet("all-field-status", Name = "Get All Field Statuses By Field")]
        [ProducesResponseType(typeof(List<ComboBoxDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetAllFieldStatus([FromQuery] GetAllFieldStatusQuery query)
        {
            var response = await mediator.Send(query);
            return this.OkIfNotNullNotFoundOtherwise(response);
        }

        [HttpGet("all-field-type", Name = "Get All Field Types By Field")]
        [ProducesResponseType(typeof(List<ComboBoxDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetAllFieldType([FromQuery] GetAllFieldTypeQuery query)
        {
            var response = await mediator.Send(query);
            return this.OkIfNotNullNotFoundOtherwise(response);
        }

        [HttpGet("for-new-reservation", Name = "Get All Fields For Reservarion")]
        [ProducesResponseType(typeof(List<FieldByIdDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAllFieldForNewReservation([FromQuery] GetAllFieldForNewReservationQuery query)
        {
            var response = await mediator.Send(query);
            return this.OkIfNotNullNotFoundOtherwise(response);
        }
    }
}
