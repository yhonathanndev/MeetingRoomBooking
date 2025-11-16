using MediatR;

namespace MeetingRoomBooking.Application.Features.Rooms.Commands.DisableRoom;

public sealed class DisableRoomCommand : IRequest
{
    public Guid RoomId {get; set; }
}
