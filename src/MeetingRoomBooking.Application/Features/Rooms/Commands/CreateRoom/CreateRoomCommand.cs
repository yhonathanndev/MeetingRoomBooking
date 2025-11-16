using MediatR;

namespace MeetingRoomBooking.Application.Features.Rooms.Commands.CreateRoom;
public sealed class CreateRoomCommand : IRequest<Guid>
{
    public string Name {get;set;} = string.Empty;
}