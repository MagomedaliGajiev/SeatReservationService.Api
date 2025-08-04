﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SeatReservation.Domain.Events;

namespace SeatReservation.Infrastructure.Postgres.Configurations;

public class EventConfiguration : IEntityTypeConfiguration<Event>
{
    public void Configure(EntityTypeBuilder<Event> builder)
    {
        builder.ToTable("events");

        builder.HasKey(e => e.Id).HasName("pk_events");

        builder.Property(e => e.Id)
            .HasConversion(e => e.Value, id => new EventId(id));

    }
}