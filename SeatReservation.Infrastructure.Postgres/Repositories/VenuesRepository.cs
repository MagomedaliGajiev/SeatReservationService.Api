using Dapper;
using SeatReservation.Domain.Venues;
using SeatReservation.Infrastructure.Postgres.Database;
using System.Data;

namespace SeatReservation.Infrastructure.Postgres.Repositories;

public class VenuesRepository
{
    private readonly NpgsqlConnectionFactory _connectionFactory;

    public VenuesRepository(NpgsqlConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }
    public async Task<Guid> Add(Venue venue)
    {
        using var connection = await _connectionFactory.CreateConnectionAsync();

        const string venueInsertSql = """
                                        INSERT INTO venues (id, prefix, name, seatslimit)
                                        VALUES (@Id, @Prefix, @Name, @SeatsLimit)
                                      """;

        var venueInsertParams = new
        {
            Id = venue.Id.Value,
            Prefix = venue.Name.Prefix,
            Name = venue.Name.Name,
            venue = venue.SeatsLimit
        };

        await connection.ExecuteAsync(venueInsertSql, venueInsertParams);

        const string seatsInsertSql = """
                                        INSERT INTO seats (id, row_number, seat_number, venue_id)
                                        VALUES (@Id, @RowNumber, @SeatNumber, @VenueId)
                                      """;

        var seatsInsertParams = venue.Seats.Select(s => new
        {
            Id = s.Id.Value,
            RowNumber = s.RowNumber,
            SeatNumber = s.SeatNumber,
            VenueId = venue.Id
        });

        await connection.ExecuteAsync(seatsInsertSql, seatsInsertParams);

        return venue.Id.Value;
    }
}
