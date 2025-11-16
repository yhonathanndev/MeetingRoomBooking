using MediatR;

namespace MeetingRoomBooking.Application.Features.Rooms.Commands.EnableRoom;
public sealed class EnableRoomCommand : IRequest
{
    public Guid RoomId {get; set;}
}