using MediatR;
using MeetingRoomBooking.Domain.Interfaces;

namespace MeetingRoomBooking.Application.Features.Rooms.Commands.DisableRoom;

public class DisableRoomHandler(IRoomRepository _roomRepository) : IRequestHandler<DisableRoomCommand>
{
    public async Task Handle(DisableRoomCommand request, CancellationToken cancellationToken)
    {
        var room = await _roomRepository.GetByIdAsync(request.RoomId, cancellationToken) ??
           throw new ($"Room with Id {request.RoomId} not found.");
        
        room.Disable();

        await _roomRepository.UpdateAsync(room, cancellationToken);
    }
}
