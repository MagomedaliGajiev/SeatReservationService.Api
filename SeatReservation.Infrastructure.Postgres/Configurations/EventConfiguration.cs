using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SeatReservation.Domain.Events;
using SeatReservation.Domain.Venues;

namespace SeatReservation.Infrastructure.Postgres.Configurations;

public class EventConfiguration : IEntityTypeConfiguration<Event>
{
    public void Configure(EntityTypeBuilder<Event> builder)
    {
        builder.ToTable("events");

        builder.HasKey(e => e.Id).HasName("pk_events");

        builder.Property(e => e.Id)
            .HasConversion(e => e.Value, id => new EventId(id))
            .HasColumnName("id");

        builder.Property(e => e.VenueId)
            .HasConversion(v => v.Value, venue_id => new VenueId(venue_id))
            .HasColumnName("venue_id");


        builder.Property(e => e.Name)
           .HasColumnName("name");

        builder.Property(e => e.EventDate)
            .HasColumnName("event_date");

    }
}