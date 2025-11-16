using MediatR;

namespace MeetingRoomBooking.Application.Features.Rooms.Commands.BookRoom;
public sealed class BookRoomCommand : IRequest<bool>
{
    public Guid RoomId { get; set; }
    public DateTimeOffset Start { get; set; }
    public DateTimeOffset End { get; set; }
}