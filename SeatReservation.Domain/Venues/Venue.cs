using CSharpFunctionalExtensions;

namespace SeatReservation.Domain.Venues;

public record VenueId(Guid Value);

public class Venue
{
    private List<Seat> _seats = [];

    //EF Core
    private Venue()
    {
        
    }

    public Venue(VenueId id, VenueName name, int maxSeatsCount, IEnumerable<Seat> seats)
    {
        Id = id;
        Name = name;
        SeatLimit = maxSeatsCount;
        _seats =seats.ToList();
    }

    public VenueId Id { get; } = null!;

    public VenueName Name { get; private set; }

    public int SeatLimit { get; private set; }

    public int SeatsCount => _seats.Count;

    public IReadOnlyList<Seat> Seats => _seats;

    public UnitResult<Error> AddSeat(Seat seat)
    {
        if (SeatsCount >= SeatLimit)
        {
            return Error.Conflict("venue.seats.limit", "");
        }

        return UnitResult.Success<Error>();
    }

    public void ExpandSeatsLimit(int newSeatsLimit) => SeatLimit = newSeatsLimit;
}
