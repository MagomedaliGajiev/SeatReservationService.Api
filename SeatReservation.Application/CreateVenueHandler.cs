using CSharpFunctionalExtensions;
using SeatReservation.Domain;
using SeatReservation.Domain.Venues;

namespace SeatReservation.Application;

public class CreateVenueHandler
{
    public CreateVenueHandler(ReservationServiceDbContext )
    {
        
    }
    /// <summary>
    /// Этот метод создает плащадку со всеми местами
    /// </summary>
    /// <returns></returns>
    public async Task<Result<Guid, Error> Handle(CreateVenueRequest request, CancellationToken cancellationToken)
    {
        // валидация бизнес логики


        // создание доменных моделей
        // var seats = request.Seats.Select(s => Seat.Create(s.RowNumber, s.SeatNumber).Value).ToList();

        var venue = Venue.Create(request.Prefix, request.Name, request.SeatsLimit);

        if (venue.IsFailure)
        {
            return venue.Error;
        }

        List<Seat> seats = [];

        foreach (var seatRequest in request.Seats)
        {
            var seat = Seat.Create(venue.Value,seatRequest.RowNumber, seatRequest.SeatNumber);

            if (seat.IsFailure)
            {
                return seat.Error;
            }
            seats.Add(seat.Value);
        }

        // сохранениедоменных моделей в БД
    }
}