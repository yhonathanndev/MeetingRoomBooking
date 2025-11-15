using MeetingRoomBooking.Domain.Interfaces;
using MeetingRoomBooking.Infrastructure.Persistence;
using MeetingRoomBooking.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using MediatR;
using MeetingRoomBooking.Application.Features.Rooms.Queries.GetRooms;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("dbCompanyABC"))
    );
// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddScoped<IRoomRepository, RoomRepository>();
builder.Services.AddMediatR(cfg=>
    cfg.RegisterServicesFromAssembly(typeof(GetRoomsQuery).Assembly)
);

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

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
