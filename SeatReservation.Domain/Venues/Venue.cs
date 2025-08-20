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

    private Venue(VenueId id, VenueName name, int maxSeatsCount, IEnumerable<Seat> seats)
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

    public static Result<Venue, Error> Create(
        string prefix,
        string name,
        int seatsLimit,
        IEnumerable<Seat> seats)
    {
        if (seatsLimit <= 0)
        {
            return Error.Validation("venue.seatsLimit", "seats limit must be greater than zero");
        }

        var venueNameResult = VenueName.Create(prefix, name);

        if (venueNameResult.IsFailure)
        {
            return venueNameResult.Error;
        }

        var venueSeats = seats.ToList();

        if (venueSeats.Count < 1)
        {
            return Error.Validation("venue.seats", "Number of seats can not be zero");
        }

        if (venueSeats.Count > seatsLimit)
        {
            return Error.Validation("venue.seats", "Number of seats exceeds the venue's seats limit");
        }

        return new Venue(new VenueId(Guid.NewGuid()), venueNameResult.Value, seatsLimit, venueSeats);
    }
}
