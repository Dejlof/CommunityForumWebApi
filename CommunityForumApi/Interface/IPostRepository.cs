using CommunityForumApi.Dtos.Post;
using CommunityForumApi.Models;

namespace CommunityForumApi.Interface
{
    public interface IPostRepository
    {
        Task<List<Post>> GetAllPostsAsync(); 
        Task<List<Post>> GetUserPostsAsync(string userName);
        Task <Post?> GetByIdAsync(int id);

        Task<Post?> CreatePostAsync (Post postModel);

        Task<Post?> UpdatePostAsync(int id, UpdatePostDto updateDto);
        Task<Post?> DeletePostAsync(int id);

        Task<bool> PostExists(int id);
       
    }
}
