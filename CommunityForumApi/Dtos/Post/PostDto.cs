using CommunityForumApi.Dtos.Comment;
using CommunityForumApi.Models;

namespace CommunityForumApi.Dtos.Post
{
    public class PostDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;

        public string Content { get; set; } = string.Empty;

        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public string CreatedBy { get; set; } = string.Empty;


        public List<CommentDto> Comments { get; set; }
    }
}
