using MeetingRoomBooking.Domain.ValueObjects;

namespace MeetingRoomBooking.Domain.Entities;
public sealed class Booking
{
    public int Id { get; private set; }
    public TimeRange TimeRange { get; private set; }
    public Guid RoomId { get; private set; }

    public Booking(TimeRange timeRange, Guid roomId)
    {
        Id = 0;
        TimeRange = timeRange;
        RoomId = roomId;
    }  

    private Booking(){} //Required by EF Core  
}