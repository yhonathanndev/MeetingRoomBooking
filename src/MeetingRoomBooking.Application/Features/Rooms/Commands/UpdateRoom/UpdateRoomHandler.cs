using MediatR;
using MeetingRoomBooking.Domain.Interfaces;
using MeetingRoomBooking.Domain.ValueObjects;

namespace MeetingRoomBooking.Application.Features.Rooms.Commands.UpdateRoom;

public sealed class UpdateRoomHandler(IRoomRepository _roomRepository) : IRequestHandler<UpdateRoomCommand>
{
    public async Task Handle(UpdateRoomCommand request, CancellationToken cancellationToken)
    {
        var room = await _roomRepository.GetByIdAsync(request.RoomId, cancellationToken) 
        ?? throw new KeyNotFoundException($"Room with id {request.RoomId} not found.");

        var newName = new RoomName(request.Name);
        room.Rename(newName);
        if(request.Enabled)
            room.Enable();
        else
            room.Disable();

        await _roomRepository.UpdateAsync(room, cancellationToken);
    }
}