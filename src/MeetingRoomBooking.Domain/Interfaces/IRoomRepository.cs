using MeetingRoomBooking.Domain.Entities;

namespace MeetingRoomBooking.Domain.Interfaces;
public interface IRoomRepository
{
    Task<IReadOnlyList<Room>>GetRoomsAsync(CancellationToken ct = default);
    Task<Room?> GetByIdAsync(Guid RoomId, CancellationToken ct = default);
    Task UpdateAsync(Room room, CancellationToken ct = default);
    Task AddAsync(Room room, CancellationToken ct = default);
    Task<Room?> GetByIdWithBookingsAsync(Guid RoomId, CancellationToken ct = default);
}