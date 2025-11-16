using MediatR;
using MeetingRoomBooking.Application.Features.Rooms.Commands.BookRoom;
using MeetingRoomBooking.Application.Features.Rooms;
using MeetingRoomBooking.Application.Features.Rooms.Queries.GetRooms;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MeetingRoomBooking.Domain.Entities;
using MeetingRoomBooking.Application.Features.Rooms.Queries.GetRoomById;
using MeetingRoomBooking.Api.Contracts.Rooms;
using MeetingRoomBooking.Application.Features.Rooms.Commands.CreateRoom;
using MeetingRoomBooking.Application.Features.Rooms.Commands.UpdateRoom;
using MeetingRoomBooking.Application.Features.Rooms.Commands.EnableRoom;
using MeetingRoomBooking.Application.Features.Rooms.Commands.DisableRoom;

namespace MeetingRoomBooking.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomController(IMediator _mediator) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<RoomDto>>> GetRooms(CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetRoomsQuery(), cancellationToken);
            return Ok(result);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<RoomDto>> GetRoomById(Guid id,CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetRoomByIdQuery { RoomId = id }, cancellationToken);
            if(result is null)
                return NotFound();
            
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult> CreateRoom([FromBody]CreateRoomRequest request, CancellationToken cancellationToken)
        {
            var command = new CreateRoomCommand
            {
                Name = request.Name
            };
            var id = await _mediator.Send(command, cancellationToken);

            return CreatedAtAction(nameof(GetRoomById),new {id},new {id = id});
        }
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateRoom(Guid id,[FromBody] UpdateRoomRequest request, CancellationToken cancellationToken)
        {
            var command = new UpdateRoomCommand
            {
                RoomId = id,
                Name = request.Name,
                Enabled = request.Enabled
            };

            await _mediator.Send(command, cancellationToken);

            return NoContent();
        }

        [HttpPatch("{id:guid}/enable")]
        public async Task<ActionResult> EnableRoom(Guid id, CancellationToken cancellationToken)
        {
            await _mediator.Send(new EnableRoomCommand { RoomId = id }, cancellationToken);
            return NoContent();
        }

         [HttpPatch("{id:guid}/disable")]
        public async Task<ActionResult> DisableRoom(Guid id, CancellationToken cancellationToken)
        {
            await _mediator.Send(new DisableRoomCommand { RoomId = id }, cancellationToken);
            return NoContent();
        }

        [HttpPost("Book")]
        public async Task<IActionResult> Book(Guid roomId,[FromBody] BookRequest request , CancellationToken cancellationToken)
        {
            var command = new BookRoomCommand
            {
                RoomId = roomId,
                Start = request.Start,
                End = request.End
            };
            try
            {
                bool result = await _mediator.Send(command, cancellationToken);
                if (!result)
                    return Conflict(new { Message = "Room not found." });

                return Ok(new { Message = "Booked successfuly" });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
            catch(Exception ex)
            {
                return NotFound(new { Error = ex.Message });
            }
        }
    }
    public sealed record BookRequest(DateTimeOffset Start,DateTimeOffset End);
}
