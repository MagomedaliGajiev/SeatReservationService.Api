namespace SeatReservation.Domain.Events;

public class Event
{
    public Event(Guid id, Guid venueId, EventDetails eventDetails, string name, DateTime eventDate)
    {
        Id = id;
        Details = eventDetails;
        VenueId = venueId;
        Name = name;
        EventDate = eventDate;
    }

    public Guid Id { get; private set; }

    public EventDetails Details { get; private set; }

    public Guid VenueId { get; private set; } 

    public string Name { get; private set; }

    public DateTime EventDate { get; private set; }
}
