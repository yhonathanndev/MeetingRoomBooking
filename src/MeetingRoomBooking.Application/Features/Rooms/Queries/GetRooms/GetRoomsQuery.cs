using MediatR;

namespace MeetingRoomBooking.Application.Features.Rooms.Queries.GetRooms;
public sealed class GetRoomsQuery : IRequest<IReadOnlyList<RoomDto>>;