using MeetingRoomBooking.Domain.Interfaces;
using MeetingRoomBooking.Infrastructure.Persistence;
using MeetingRoomBooking.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using MediatR;
using MeetingRoomBooking.Application.Features.Rooms.Queries.GetRooms;
using MeetingRoomBooking.Application.Features.Rooms.Queries.GetRoomById;
using MeetingRoomBooking.Application.Features.Rooms.Commands.CreateRoom;
using MeetingRoomBooking.Application.Features.Rooms.Commands.UpdateRoom;
using MeetingRoomBooking.Application.Features.Rooms.Commands.DisableRoom;
using MeetingRoomBooking.Application.Features.Rooms.Commands.EnableRoom;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("dbCompanyABC"))
    );
// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddScoped<IRoomRepository, RoomRepository>();
builder.Services.AddMediatR(cfg=>
    cfg.RegisterServicesFromAssembly(typeof(GetRoomsQuery).Assembly)
    .RegisterServicesFromAssembly(typeof(GetRoomByIdQuery).Assembly)
    .RegisterServicesFromAssembly(typeof(CreateRoomCommand).Assembly)
    .RegisterServicesFromAssembly(typeof(UpdateRoomCommand).Assembly)
    .RegisterServicesFromAssembly(typeof(DisableRoomCommand).Assembly)
    .RegisterServicesFromAssembly(typeof(EnableRoomCommand).Assembly)

);

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",policy =>
    {
        policy.WithOrigins("http://localhost:5173") // Frontend URL vite
        .AllowAnyHeader()
        .AllowAnyMethod()
        .AllowCredentials();
    });
});

var app = builder.Build();

app.UseCors("AllowFrontend");
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
