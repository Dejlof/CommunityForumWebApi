using CommunityForumApi.Dtos.Comment;
using CommunityForumApi.Models;

namespace CommunityForumApi.Interface
{
    public interface IcommentRepository
    {
        Task<List<Comment>> GetAllAsync();

        Task<Comment?> GetByIdAsync (int id);

        Task<Comment?> CreateAsync(Comment comentModel);

        Task<Comment?> UpdateAsync(int id, UpdateCommentDto updateDto);
        
        Task<Comment?> DeleteAsync(int id); 

    }
}
