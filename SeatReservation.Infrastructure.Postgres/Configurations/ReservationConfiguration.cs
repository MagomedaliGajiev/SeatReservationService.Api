using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SeatReservation.Domain;
using SeatReservation.Domain.Events;
using SeatReservation.Domain.Reservations;


namespace SeatReservation.Infrastructure.Postgres.Configurations;

public class ReservationConfiguration : IEntityTypeConfiguration<Reservation>
{
    public void Configure(EntityTypeBuilder<Reservation> builder)
    {
        builder.ToTable("reservations");

        builder.HasKey(r => r.Id).HasName("pk_reservations");

        builder.Property(r => r.Id)
            .HasConversion(r => r.Value, id => new ReservationId(id))
            .HasColumnName("id")
            .HasColumnOrder(0);

        builder.Property(r => r.EventId)
            .HasConversion(r => r.Value, event_id => new EventId(event_id))
            .HasColumnName("event_id")
            .HasColumnOrder(1);
        
        builder.Property(r => r.UserId)
            .HasConversion(r => r.Value, uId => new UserId(uId))
            .HasColumnName("user_id")
            .HasColumnOrder(2);

        builder.Property(r => r.Status)
            .HasConversion<string>()
            .HasColumnName("status")
            .HasColumnOrder(3);

        builder.Property(r => r.CreatedAt)
            .HasColumnName("created_at")
            .HasColumnOrder(4);
    }
}
