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
            .HasColumnName("event_id")
            .HasColumnOrder(0);

        builder.Property(ed => ed.Capacity)
            .HasColumnName("capacity")
            .HasColumnOrder(1);

        builder.Property(ed => ed.Description)
            .HasColumnName("description")
            .HasColumnOrder(2);

        builder
            .HasOne<Event>()
            .WithOne(e => e.Details)
            .HasForeignKey<EventDetails>(ed => ed.EventId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
    }
}