using MediatR;
using MeetingRoomBooking.Domain.Interfaces;
using MeetingRoomBooking.Domain.ValueObjects;

namespace MeetingRoomBooking.Application.Features.BookRoom;

public sealed class BookRoomHandler(IRoomRepository _roomRepository) : IRequestHandler<BookRoomCommand, bool>
{
    public async Task<bool> Handle(BookRoomCommand request, CancellationToken cancellationToken)
    {
        var room = await _roomRepository.GetByIdAsync(request.RoomId,cancellationToken) ?? throw new("Room not found");
        var timeRange = new TimeRange(request.Start, request.End);;

        bool added = room.TryAddBooking(timeRange);
        if (!added)
            return false;

        await _roomRepository.UpdateAsync(room,cancellationToken);

        return true;
    }
}