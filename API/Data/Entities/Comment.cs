namespace API.Data.Entities
{
    public class Comment
    {
        public int Id { get; set; }
        public string Body { get; set; }

        public DateTime DateCreated { get; set; }
        public DateTime? DateUpdated { get; set; }

        //Navigation
        public int AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        public int DiscussionId { get; set; }
        public Discussion Discussion { get; set; }
        public int? CommentId { get; set; }
    }
}
