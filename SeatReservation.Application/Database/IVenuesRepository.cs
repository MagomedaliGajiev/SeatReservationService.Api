using SeatReservation.Domain.Venues;

namespace SeatReservation.Application.Database;

public interface IVenuesRepository
{
    Task<Guid> Add(Venue venue, CancellationToken cancellationToken = default);
}
