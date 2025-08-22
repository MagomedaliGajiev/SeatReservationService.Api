using SeatReservation.Application.Database;
using SeatReservation.Domain.Venues;

namespace SeatReservation.Infrastructure.Postgres.Repositories;

public class EfCoreVenuesRepositiry : IVenuesRepository
{
    private readonly ReservationServiceDbContext _dbContext;

    public EfCoreVenuesRepositiry(ReservationServiceDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Guid> Add(Venue venue, CancellationToken cancellationToken)
    {
        await _dbContext.AddAsync(venue, cancellationToken);

        await _dbContext.SaveChangesAsync();

        return venue.Id.Value;
    }
}
