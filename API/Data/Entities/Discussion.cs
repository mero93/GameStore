namespace API.Data.Entities
{
    public class Discussion
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }

        public DateTime DateCreated { get; set; }
        public DateTime? DateUpdated { get; set; }
        public DateTime LastActivity { get; set; }

        //Navigation
        public int AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        public int GameId { get; set; }
        public Game Game { get; set; }

        public IEnumerable<Comment> Comments { get; set; } = new List<Comment>();
    }
}
