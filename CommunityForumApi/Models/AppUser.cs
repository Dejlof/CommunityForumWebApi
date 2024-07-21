using Microsoft.AspNetCore.Identity;

namespace CommunityForumApi.Models
{
    public class AppUser : IdentityUser
    {
       public ICollection<Post> Posts = new List<Post>();
        public ICollection<Comment> Comments = new List<Comment>();
    }
}
