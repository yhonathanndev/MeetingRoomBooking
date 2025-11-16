using MediatR;
using MeetingRoomBooking.Domain.Interfaces;

namespace MeetingRoomBooking.Application.Features.Rooms.Queries.GetRoomById;

public sealed class GetRoomByIdHandler(IRoomRepository _roomRepository) : IRequestHandler<GetRoomByIdQuery, RoomDto?>
{
    public async Task<RoomDto?> Handle(GetRoomByIdQuery request, CancellationToken cancellationToken)
    {
       
       var room = await _roomRepository.GetByIdAsync(request.RoomId, cancellationToken);

        if(room is null)
            return null;

        return new RoomDto(
            room.Id,
            room.Name.ToString(),
            room.Enabled
        );
       
    }
}
