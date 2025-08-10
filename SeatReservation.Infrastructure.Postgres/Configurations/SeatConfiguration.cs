using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SeatReservation.Domain.Venues;

namespace SeatReservation.Infrastructure.Postgres.Configurations;

public class SeatConfiguration : IEntityTypeConfiguration<Seat>
{
    public void Configure(EntityTypeBuilder<Seat> builder)
    {
        builder.ToTable("seats");

        builder.HasKey(s => s.Id).HasName("pk_seats");

        builder.Property(s => s.Id)
            .HasConversion(s => s.Value, id => new SeatId(id))
            .HasColumnName("id")
            .HasColumnOrder(0);

        builder.Property(s => s.VenueId)
            .HasConversion(s => s.Value, vId => new VenueId(vId))
            .HasColumnName("venue_id")
            .HasColumnOrder(1);

        builder.Property(s => s.RowNumber)
           .HasColumnName("row_number")
           .HasColumnOrder(2);

        builder.Property(s => s.SeatNumber)
            .HasColumnName("seat_number")
            .HasColumnOrder(3);
    }
}