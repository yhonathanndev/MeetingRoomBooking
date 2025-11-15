using System;
using MediatR;
using MeetingRoomBooking.Domain.Interfaces;

namespace MeetingRoomBooking.Application.Features.Rooms.Queries.GetRooms;

public class GetRoomsHandler(IRoomRepository _roomRepository) : IRequestHandler<GetRoomsQuery, IReadOnlyList<RoomDto>>
{
    public async Task<IReadOnlyList<RoomDto>> Handle(GetRoomsQuery request, CancellationToken cancellationToken)
    {
        var rooms = await _roomRepository.GetRoomsAsync(cancellationToken);

        return [.. rooms
            .Select(room=> new RoomDto(
            
               Id : room.Id,
               Name : room.Name.ToString(),
               Enabled : room.Enabled 
            ))];
            
    }
}
