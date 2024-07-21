namespace CommunityForumApi.Models
{
    public class Comment
    {
        public int Id { get; set; }

        public string Content { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public string CreatedBy { get; set; } = string.Empty;

        public string AppUserId { get; set; }

        public AppUser AppUser { get; set; }



        public int PostId { get; set; }

        public Post Posts { get; set; }
    }
}
