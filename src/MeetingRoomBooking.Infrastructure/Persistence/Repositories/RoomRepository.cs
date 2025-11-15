using MeetingRoomBooking.Domain.Entities;
using MeetingRoomBooking.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MeetingRoomBooking.Infrastructure.Persistence.Repositories;

public sealed class RoomRepository(AppDbContext _appDbContext) : IRoomRepository
{

    public async Task<IReadOnlyList<Room>> GetRoomsAsync(CancellationToken ct = default)
    {
        return await _appDbContext.Rooms
            .AsNoTracking()
            .ToListAsync(ct);
    }

    public async Task AddAsync(Room room, CancellationToken ct = default)
    {
        _appDbContext.Add(room);
        await _appDbContext.SaveChangesAsync(ct);
    }

    public async Task<Room?> GetByIdAsync(Guid RoomId, CancellationToken ct = default)
    {
        return await _appDbContext.Rooms
            .Include(b => b.Bookings)
            .FirstOrDefaultAsync(r => r.Id == RoomId,ct);
    }
    public async Task UpdateAsync(Room room, CancellationToken ct = default)
    {
        _appDbContext.Attach(room);
        _appDbContext.Entry(room).State = EntityState.Unchanged;

        foreach (var booking in room.Bookings)
            _appDbContext.Entry(booking).State = EntityState.Added;

        await _appDbContext.SaveChangesAsync();
    }
}