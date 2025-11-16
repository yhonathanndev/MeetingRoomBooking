using MediatR;

namespace MeetingRoomBooking.Application.Features.Rooms.Commands.UpdateRoom;
public sealed class UpdateRoomCommand:IRequest
{
    public Guid RoomId { get; set; }
    public string Name { get; set; } = String.Empty;
    public bool Enabled { get; set; }
}