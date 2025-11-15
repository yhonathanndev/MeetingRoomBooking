using MeetingRoomBooking.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MeetingRoomBooking.Infrastructure.Persistence.Configurations;

public sealed class RoomConfiguration : IEntityTypeConfiguration<Room>
{
    public void Configure(EntityTypeBuilder<Room> room)
    {
        room.ToTable("rooms");

        room.HasKey(r => r.Id);
        room.Property(r => r.Id)
            .HasColumnName("id")
            .ValueGeneratedNever();

        room.OwnsOne(r => r.Name, nb =>
        {
            nb.Property(n => n.Value)
                .HasColumnName("name")
                .HasMaxLength(100)
                .IsRequired();

            nb.HasIndex(n => n.Value).IsUnique();
        });

        room.Property(r=> r.Enabled)
            .HasColumnName("enabled")
            .IsRequired();

        room.HasMany(r => r.Bookings)
            .WithOne()
            .HasForeignKey(b => b.RoomId)
            .OnDelete(DeleteBehavior.Cascade);

        room.Navigation(b => b.Bookings)
            .HasField("_bookings")
            .UsePropertyAccessMode(PropertyAccessMode.Field);

        room.UsePropertyAccessMode(PropertyAccessMode.PreferFieldDuringConstruction);
    }
}