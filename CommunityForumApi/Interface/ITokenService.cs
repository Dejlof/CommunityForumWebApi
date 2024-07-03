using CommunityForumApi.Models;

namespace CommunityForumApi.Interface
{
    public interface ITokenService
    {
       Task <string> CreateToken (AppUser user);
    }
}
