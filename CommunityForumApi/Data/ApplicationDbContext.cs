using CommunityForumApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CommunityForumApi.Data
{
    public class ApplicationDbContext:DbContext
    {

        public ApplicationDbContext(DbContextOptions dbContextOptions):base(dbContextOptions)
        {
            
        }

        public DbSet<Post> Posts { get; set; }

        public DbSet <Comment> Comments { get; set; }
    }
}
