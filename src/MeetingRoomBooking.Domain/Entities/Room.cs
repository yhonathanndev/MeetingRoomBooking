using MeetingRoomBooking.Domain.ValueObjects;

namespace MeetingRoomBooking.Domain.Entities;
public sealed class Room
{
    public Guid Id { get; private set; }
    public RoomName Name { get; private set; }
    private readonly List<Booking> _bookings = [];
    public IReadOnlyList<Booking> Bookings => _bookings;

    public Room(Guid id, RoomName name)
    {
        ArgumentNullException.ThrowIfNull(name);
        Id = id;
        Name = name;
    }
    private Room() { } //Required by EF Core

    public bool TryAddBooking(TimeRange newRange)
    {
        bool hasConflict = _bookings.Any(b => b.TimeRange.Overlaps(newRange));

        if (hasConflict)
            return false;

        var newBooking = new Booking(newRange, Id);
        _bookings.Add(newBooking);
        return true;
    }

    

}