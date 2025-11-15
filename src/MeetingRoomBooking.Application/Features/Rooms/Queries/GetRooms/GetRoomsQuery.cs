using MediatR;

namespace MeetingRoomBooking.Application.Features.Rooms.Queries.GetRooms;
public sealed record GetRoomsQuery : IRequest<IReadOnlyList<RoomDto>>;