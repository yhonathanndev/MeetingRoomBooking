using System;

namespace MeetingRoomBooking.Api.Contracts.Rooms;

public class UpdateRoomRequest
{
    public string Name { get; set; } = string.Empty;
    public bool Enabled { get; set; }
}
