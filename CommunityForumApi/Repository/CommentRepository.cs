using CommunityForumApi.Data;
using CommunityForumApi.Dtos.Comment;
using CommunityForumApi.Interface;
using CommunityForumApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CommunityForumApi.Repository
{
    public class CommentRepository : IcommentRepository
    {
        private readonly ApplicationDbContext _context;
        public CommentRepository(ApplicationDbContext context)
        {
            _context = context;
            
        }
        public async Task<Comment?> CreateAsync(Comment comentModel)
        {
            await _context.Comments.AddAsync(comentModel);
            await _context.SaveChangesAsync();
            return comentModel;

        }

        public async Task<Comment?> DeleteAsync(int id)
        {
           var comment = await _context.Comments.FirstOrDefaultAsync(c => c.Id == id);
            if (comment == null)
            {
                return null;
            }

            _context.Comments.Remove(comment);
            await _context.SaveChangesAsync();
            return comment;
        }

        public async Task<List<Comment>> GetAllAsync()
        {
            return await _context.Comments.Include(a=> a.AppUser).ToListAsync();
        }




        public async Task<Comment?> GetByIdAsync(int id)
        {
            return await _context.Comments.Include(a=>a.Id).FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<List<Comment>> GetUserCommentAsync(string userName)
        {
            return await _context.Comments.Include(a=> a.AppUser).
                Where(a=>a.AppUser.UserName == userName).
                ToListAsync();
        }

        public async Task<Comment?> UpdateAsync(int id, UpdateCommentDto updateDto)
        {
           var existingComment = await _context.Comments.FirstOrDefaultAsync(x=>x.Id == id);
            if (existingComment == null)
            {
                return null;
            }
            existingComment.Content = updateDto.Content;

            await _context.SaveChangesAsync();
           
            return existingComment;
        }
    }
}
