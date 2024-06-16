namespace CommunityForumApi.Models
{
    public class Post
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public string Content { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime UpdatedDate { get; set; } = DateTime.Now;

        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
    }
}
