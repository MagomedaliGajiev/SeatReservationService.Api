﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
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
            .HasColumnName("id");

        builder.Property(r => r.EventId)
            .HasConversion(r => r.Value, event_id => new EventId(event_id))
            .HasColumnName("event_id");



    }
}
