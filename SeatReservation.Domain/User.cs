namespace SeatReservation.Domain
{
    public record UserId(Guid Value);

    public class User
    {
        //EF Core
        public User()
        {
            
        }

        public UserId Id { get; set; }

        public Details Details { get; set; }

    }

    public record Details
    {
        public Details()
        {
            
        }

        public string FIO { get; set; }

        public string Description { get; set;  }

        public IReadOnlyList<SocialNetwork> Socials { get; }
    }

    public record SocialNetwork
    {
        public SocialNetwork()
        {
            
        }

        public string Name { get; init; }

        public string Link { get; init; }
    }
}
