using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SeatReservation.Domain.Events;
using SeatReservation.Domain.Venues;
using SeatReservation.Infrastructure.Postgres.Configurations.Converters;

namespace SeatReservation.Infrastructure.Postgres.Configurations;

public class EventConfiguration : IEntityTypeConfiguration<Event>
{
    public void Configure(EntityTypeBuilder<Event> builder)
    {
        builder.ToTable("events");

        builder.HasKey(e => e.Id).HasName("pk_events");

        builder.Property(e => e.Id)
            .HasConversion(e => e.Value, id => new EventId(id))
            .HasColumnName("id")
            .HasColumnOrder(0);

        builder.Property(e => e.VenueId)
            .HasConversion(v => v.Value, venue_id => new VenueId(venue_id))
            .HasColumnName("venue_id")
            .HasColumnOrder(1);


        builder.Property(e => e.Name)
           .HasColumnName("name")
           .HasColumnOrder(2);

        builder.Property(e => e.Type)
            .HasConversion<string>()
            .HasColumnName("type")
            .HasColumnOrder(3);

        builder.Property(e => e.EventDate)
            .HasColumnName("event_date")
            .HasColumnOrder(4);

        builder.Property(e => e.Info)
            .HasConversion(new EventInfoConverter())
            .HasColumnName ("info")
            .HasColumnOrder(5);

        builder
            .HasOne<Venue>()
            .WithMany()
            .HasForeignKey(e => e.VenueId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
    }
}