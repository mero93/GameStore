namespace API.Data.Entities
{
    public class Review
    {
        public decimal Rating { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateUpdated { get; set; }

        public int AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        public int GameId { get; set; }
        public Game Game { get; set; }
    }
}