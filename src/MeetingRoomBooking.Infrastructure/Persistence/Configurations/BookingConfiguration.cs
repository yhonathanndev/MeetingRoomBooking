using MeetingRoomBooking.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MeetingRoomBooking.Infrastructure.Persistence.Configurations;

public sealed class BookingConfiguration : IEntityTypeConfiguration<Booking>
{
    public void Configure(EntityTypeBuilder<Booking> booking)
    {
        booking.ToTable("bookings");

        booking.HasKey(b => b.Id);
        booking.Property(b => b.Id)
            .HasColumnName("id")
            .ValueGeneratedOnAdd();

        booking.Property(b => b.RoomId)
            .HasColumnName("room_id")
            .IsRequired();

        booking.OwnsOne(x => x.TimeRange, tr =>
        {
            tr.Property(p => p.Start)
                .HasColumnName("start")
                .IsRequired();
            tr.Property(p => p.End)
                .HasColumnName("end")
                .IsRequired();
        });

       
    }
}
