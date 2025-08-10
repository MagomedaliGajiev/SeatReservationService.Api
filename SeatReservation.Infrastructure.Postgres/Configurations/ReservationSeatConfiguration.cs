using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SeatReservation.Domain.Reservations;
using SeatReservation.Domain.Venues;

namespace SeatReservation.Infrastructure.Postgres.Configurations;

public class ReservationSeatConfiguration : IEntityTypeConfiguration<ReservationSeat>
{
    public void Configure(EntityTypeBuilder<ReservationSeat> builder)
    {
        builder.ToTable("reservation_seats");

        builder.HasKey(rs => rs.Id).HasName("pk_reservation_seats");

        builder.Property(rs => rs.Id)
            .HasConversion(rs => rs.Value, id => new ReservationSeatId(id))
            .HasColumnName("id")
            .HasColumnOrder(0);

        builder.Property(rs => rs.SeatId)
            .HasConversion(v => v.Value, seat_id => new SeatId(seat_id))
            .HasColumnName("seat_id")
            .HasColumnOrder(1);

        builder.Property(rs => rs.ReservedAt)
            .HasColumnName("reserve_at")
            .HasColumnOrder(2);

        builder
            .HasOne(rs => rs.Reservation)
            .WithMany(r => r.ReservedSeats)
            .HasForeignKey("reservation_id")
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne<Seat>()
            .WithMany()
            .HasForeignKey(rs =>rs.SeatId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
    }
}
