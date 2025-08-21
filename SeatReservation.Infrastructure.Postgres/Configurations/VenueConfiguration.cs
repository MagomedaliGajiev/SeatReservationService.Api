using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SeatReservation.Domain;
using SeatReservation.Domain.Venues;

namespace SeatReservation.Infrastructure.Postgres.Configurations;

public class VenueConfiguration : IEntityTypeConfiguration<Venue>
{
    public void Configure(EntityTypeBuilder<Venue> builder)
    {
        builder.ToTable("venues");

        builder.HasKey(v => v.Id).HasName("pk_venues");

        builder.Property(v => v.Id)
            .HasConversion(v => v.Value, id => new VenueId(id))
            .HasColumnName("id")
            .HasColumnOrder(0);

        builder.ComplexProperty(v => v.Name, nb =>
        {
            nb.Property(v => v.Prefix)
            .IsRequired()
            .HasMaxLength(LengthConstants.LENGTH50)
            .HasColumnName("prefix")
            .HasColumnOrder(1);

            nb.Property(v => v.Name)
            .IsRequired()
            .HasMaxLength(LengthConstants.LENGTH500)
            .HasColumnName("name")
            .HasColumnOrder(2);
        });

        builder.Property(v => v.SeatsLimit)
            .IsRequired()
            .HasColumnName("seats_limit")
            .HasColumnOrder(3);

        builder
            .HasMany(v => v.Seats)
            .WithOne(s => s.Venue)
            .HasForeignKey("venue_id")
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
    }
}