using System.ComponentModel.DataAnnotations;

namespace CommunityForumApi.Dtos.Comment
{
    public class UpdateCommentDto
    {
        [Required]
        [MaxLength(100, ErrorMessage = "Comment can not more than 100 characters")]
        public string Content { get; set; } = string.Empty;
    }
}
