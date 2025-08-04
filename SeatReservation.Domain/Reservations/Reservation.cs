using CSharpFunctionalExtensions;

namespace SeatReservation.Domain.Reservations;

public class Reservation
{

    //EF Core
    private Reservation()
    {
        
    }
    public Reservation(ReservationId id, Guid eventId, Guid userId, IEnumerable<Guid> seatIds)
    {
        Id = id;
        EventId = eventId;
        UserId = userId;
        Status = ReservationStatus.Pending;
        CreatedAt = DateTime.UtcNow;

        var reservedSeats = seatIds
            .Select(seatId => new ReservationSeat(new ReservationSeatId(Guid.NewGuid()), this, seatId)).ToList();
    }

    private readonly List<ReservationSeat> _reservedSeats;

    public ReservationId Id { get; private set; }

    public Guid EventId { get; private set;}

    public Guid UserId { get; private set; }

    public ReservationStatus Status { get; private set; }

    public DateTime CreatedAt { get; private set; }

    public IReadOnlyList<ReservationSeat> Seats => _reservedSeats;
}

public record ReservationId(Guid Value);
