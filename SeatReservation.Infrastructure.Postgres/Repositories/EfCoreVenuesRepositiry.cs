using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using SeatReservation.Application.Database;
using SeatReservation.Domain;
using SeatReservation.Domain.Venues;

namespace SeatReservation.Infrastructure.Postgres.Repositories;

public class EfCoreVenuesRepositiry : IVenuesRepository
{
    private readonly ReservationServiceDbContext _dbContext;
    private readonly ILogger<EfCoreVenuesRepositiry> _logger;

    public EfCoreVenuesRepositiry(ReservationServiceDbContext dbContext, ILogger<EfCoreVenuesRepositiry> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task<Result<Guid, Error>> Add(Venue venue, CancellationToken cancellationToken)
    {
        try
        {
            await _dbContext.AddAsync(venue, cancellationToken);

            await _dbContext.SaveChangesAsync();

            return venue.Id.Value;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Fail to insert venue");

            return Error.Failure("venue.insert", "Fail to insert venue");
        }
    }
}
