using CommunityForumApi.Dtos.Post;
using CommunityForumApi.Models;

namespace CommunityForumApi.Interface
{
    public interface IPostRepository
    {
        Task<List<Post>> GetAllPostsAsync(); 

        Task <Post?> GetByIdAsync(int id);

        Task<Post?> CreatePostAsync (Post postModel);

        Task<Post?> UpdatePostAsync(int id, UpdatePostDto updateDto);
        Task<Post?> DeletePostAsync(int id);
    }
}
