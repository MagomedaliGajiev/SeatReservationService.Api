namespace SeatReservation.Domain.Events;

public class Event
{
    //EF Core
    public Event()
    {
        
    }

    public Event(EventId id, Guid venueId, EventDetails eventDetails, string name, DateTime eventDate)
    {
        Id = id;
        Details = eventDetails;
        VenueId = venueId;
        Name = name;
        EventDate = eventDate;
    }

    public EventId Id { get; private set; }

    public EventDetails Details { get; private set; }

    public Guid VenueId { get; private set; } 

    public string Name { get; private set; }

    public DateTime EventDate { get; private set; }
}

public record EventId(Guid Value);
