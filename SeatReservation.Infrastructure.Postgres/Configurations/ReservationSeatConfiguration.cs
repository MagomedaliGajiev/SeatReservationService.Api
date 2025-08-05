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
            .HasColumnName("Id");

        builder.Property(rs => rs.SeatId)
            .HasConversion(v => v.Value, seat_id => new SeatId(seat_id))
            .HasColumnName("seat_id");

    }
}
