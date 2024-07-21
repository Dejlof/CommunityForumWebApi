using CommunityForumApi.Data;
using CommunityForumApi.Dtos.Post;
using CommunityForumApi.Interface;
using CommunityForumApi.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace CommunityForumApi.Repository
{
    public class PostRepository : IPostRepository
    {
        private readonly ApplicationDbContext _context;
        public PostRepository (ApplicationDbContext context) 
        {
            _context = context;
        }
        public async Task<Post?> CreatePostAsync(Post postModel)
        {
            await _context.Posts.AddAsync(postModel);
            await _context.SaveChangesAsync();
            return postModel;
        }

        public async Task<Post?> DeletePostAsync(int id)
        {
            var post = await _context.Posts.FirstOrDefaultAsync(x => x.Id == id);
            if (post == null) 
            {
                return null;
            }

            _context.Posts.Remove(post);
            await _context.SaveChangesAsync();
            return post;
        }

        public async Task<List<Post>> GetAllPostsAsync()
        {
          var posts = await _context.Posts.Include(s=> s.Comments).
                ThenInclude(s=> s.AppUser)
                .Include(s=>s.AppUser)
                .ToListAsync();
            return posts;
        }

        public async Task<Post?> GetByIdAsync(int id)
        {
          return await _context.Posts.Include(s=> s.Comments).
                ThenInclude(s => s.AppUser)
                .Include(s => s.AppUser).FirstOrDefaultAsync(c=>c.Id == id);
            
        }

        public async Task<bool> PostExists(int id)
        {
            return await _context.Posts.AnyAsync(c=>c.Id == id);
        }

        public async Task<Post?> UpdatePostAsync(int id, UpdatePostDto updateDto)
        {
           var existingPost = await _context.Posts.FirstOrDefaultAsync(i=>i.Id == id);
            if (existingPost == null) 
            {
                return null;
            }
            
            existingPost.Title = updateDto.Title;
            existingPost.Content = updateDto.Content;

            await _context.SaveChangesAsync();
            
            return existingPost;

        }
    }
}
