using MeetingRoomBooking.Domain.Entities;

namespace MeetingRoomBooking.Domain.Interfaces;
public interface IRoomRepository
{
    Task<Room?> GetByIdAsync(Guid RoomId, CancellationToken ct = default);
    Task UpdateAsync(Room room, CancellationToken ct = default);
    Task AddAsync(Room room, CancellationToken ct = default);
}