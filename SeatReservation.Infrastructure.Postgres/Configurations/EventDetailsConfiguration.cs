using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SeatReservation.Domain.Events;

namespace SeatReservation.Infrastructure.Postgres.Configurations;

public class EventDetailsConfiguration : IEntityTypeConfiguration<EventDetails>
{
    public void Configure(EntityTypeBuilder<EventDetails> builder)
    {
        builder.ToTable("event_details");

        builder.HasKey(ed => ed.EventId).HasName("pk_event_details");

        builder.Property(ed => ed.EventId)
            .HasConversion(ed => ed.Value, id => new EventId(id))
            .HasColumnName("event_id");

        builder.Property(ed => ed.Capacity)
            .HasColumnName("capacity");

        builder.Property(ed => ed.Description)
            .HasColumnName("description");

    }
}