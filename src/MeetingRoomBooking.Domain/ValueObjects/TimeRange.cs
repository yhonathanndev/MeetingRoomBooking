namespace MeetingRoomBooking.Domain.ValueObjects;
public sealed record TimeRange
{
    public DateTimeOffset Start { get; private set; }
    public DateTimeOffset End { get; private set; }


    public TimeRange(DateTimeOffset start, DateTimeOffset end)
    {
        if (start >= end)
            throw new ArgumentException("End time must be after start time.");
        if ((end - start).TotalHours > 2)
            throw new ArgumentException("A booking cannot last more than 2 hours.");
        Start = start;
        End = end;
    }
    private TimeRange(){} //Required by EF Core
    public bool Overlaps(TimeRange other)
    {
        return Start < other.End && other.Start < End;
    }
}