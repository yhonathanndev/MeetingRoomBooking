using MediatR;
using MeetingRoomBooking.Domain.Entities;
using MeetingRoomBooking.Domain.Interfaces;
using MeetingRoomBooking.Domain.ValueObjects;
namespace MeetingRoomBooking.Application.Features.Rooms.Commands.CreateRoom;

public sealed class CreateRoomHandler(IRoomRepository _roomRepository) : IRequestHandler<CreateRoomCommand, Guid>
{
    public async Task<Guid> Handle(CreateRoomCommand request, CancellationToken cancellationToken)
    {
        var id = Guid.NewGuid();
        var roomName = new RoomName(request.Name);
        var room = new Room(id,roomName);

        await  _roomRepository.AddAsync(room, cancellationToken);
       return id;
    }
}