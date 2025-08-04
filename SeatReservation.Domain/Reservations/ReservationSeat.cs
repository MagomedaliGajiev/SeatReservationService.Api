namespace SeatReservation.Domain.Reservations;

public class ReservationSeat
{
    //EF Core
    private ReservationSeat()
    {

    }

    public ReservationSeat(ReservationSeatId id, Reservation reservation, Guid seatId)
    {
        Id = id;
        Reservation = reservation;
        SeatId = seatId;
        ReservedAt = DateTime.UtcNow;
    }

    public ReservationSeatId Id { get; }

    public Reservation Reservation { get; private set; }

    public Guid SeatId { get; private set; }

    public DateTime ReservedAt { get; }
}

public record ReservationSeatId(Guid Value);
