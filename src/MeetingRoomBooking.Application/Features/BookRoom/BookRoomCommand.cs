using MediatR;

namespace MeetingRoomBooking.Application.Features.BookRoom;
public sealed record BookRoomCommand : IRequest<bool>
{
    public Guid RoomId { get; set; }
    public DateTimeOffset Start { get; set; }
    public DateTimeOffset End { get; set; }
}