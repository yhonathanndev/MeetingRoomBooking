using MediatR;
using MeetingRoomBooking.Domain.Interfaces;

namespace MeetingRoomBooking.Application.Features.Rooms.Commands.EnableRoom;

public sealed class EnableRoomHandler(IRoomRepository _roomRepository) : IRequestHandler<EnableRoomCommand>
{
    public async Task Handle(EnableRoomCommand request, CancellationToken cancellationToken)
    {
        var room =await _roomRepository.GetByIdAsync(request.RoomId, cancellationToken) ?? 
            throw new ($"Room with Id {request.RoomId} not found.");
    
        room.Enable();

        await _roomRepository.UpdateAsync(room, cancellationToken);
    }
}