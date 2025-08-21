using SeatReservation.Domain.Venues;
using System.Collections.Generic;

namespace SeatReservation.Application;

public interface IResrvationServiceDbContext
{
    DbSet<Venue> Venues
}
