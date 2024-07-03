using CommunityForumApi.Models;

namespace CommunityForumApi.Interface
{
    public interface ITokenService
    {
        string CreateToken (AppUser user);
    }
}
