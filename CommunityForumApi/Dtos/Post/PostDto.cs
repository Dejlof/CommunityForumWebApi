using CommunityForumApi.Models;

namespace CommunityForumApi.Dtos.Post
{
    public class PostDto
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public string Content { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;


        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
    }
}
