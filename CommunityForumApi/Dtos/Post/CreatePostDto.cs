using System.ComponentModel.DataAnnotations;

namespace CommunityForumApi.Dtos.Post
{
    public class CreatePostDto
    {
        [Required]
        [MaxLength(20, ErrorMessage ="Title can not more than 20 characters")]
        public string Title { get; set; } = string.Empty;

        [Required]
        [MaxLength(250, ErrorMessage ="Content can not more be more than 250 characters")]
        public string Content { get; set; }= string.Empty;
    }
}
