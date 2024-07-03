using System.ComponentModel.DataAnnotations;

namespace CommunityForumApi.Dtos.Account
{
    public class LoginDto
    {
        [Required]
        public string? Login { get; set; }



        [Required]
        public string? Password { get; set; }
    }
}

