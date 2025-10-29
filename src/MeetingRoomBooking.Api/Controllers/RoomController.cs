using MediatR;
using MeetingRoomBooking.Application.Features.BookRoom;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MeetingRoomBooking.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomController(IMediator _mediator) : ControllerBase
    {

        public async Task<IActionResult> Book(Guid roomId,[FromBody] BookRequest request , CancellationToken cancellationToken)
        {
            var command = new BookRoomCommand
            {
                RoomId = roomId,
                Start = request.start,
                End = request.end
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
    
    public sealed record BookRequest(DateTimeOffset start,DateTimeOffset end);
}
