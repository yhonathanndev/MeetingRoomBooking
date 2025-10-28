namespace MeetingRoomBooking.Domain.ValueObjects;
public sealed record RoomName
{
    public string Value { get; init; }

    public RoomName(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Room name cannot be empty");

        if (value.Length > 100)
            throw new ArgumentException("Room name is too long");

        Value = value.Trim();
    }
    private RoomName(){} //EF Core
    public override string ToString() => Value;

}