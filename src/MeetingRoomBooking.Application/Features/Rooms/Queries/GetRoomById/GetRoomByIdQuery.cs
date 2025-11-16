using System;
using MediatR;

namespace MeetingRoomBooking.Application.Features.Rooms.Queries.GetRoomById;

public sealed class GetRoomByIdQuery: IRequest<RoomDto?>
{
    public Guid RoomId {get; set;}
}
