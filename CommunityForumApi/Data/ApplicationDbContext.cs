using CommunityForumApi.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CommunityForumApi.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {

        public ApplicationDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {

        }

        public DbSet<Post> Posts { get; set; }

        public DbSet<Comment> Comments { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<AppUser>()
              .HasIndex(u => u.PhoneNumber)
              .IsUnique();

            builder.Entity<AppUser>()
                .HasMany(u => u.Posts)
                .WithOne(p => p.AppUser)
                .HasForeignKey(p => p.AppUserId)
             .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<AppUser>()
                .HasMany(u => u.Comments)
                .WithOne(p => p.AppUser)
                .HasForeignKey(p => p.AppUserId)
             .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Post>()
                .HasMany(u => u.Comments)
                .WithOne(p => p.Posts)
                .HasForeignKey(p => p.PostId)
                 .OnDelete(DeleteBehavior.Cascade);



            List<IdentityRole> roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Id = "1",
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                },
                new IdentityRole
                {
                    Id ="2",
                    Name = "User",
                    NormalizedName = "USER"
                }
            };

            var adminUser = new AppUser
            {
                Id = "1",
                UserName = "dejlof",
                NormalizedUserName = "DEJLOF",
                Email = "dejlof@example.com",
                NormalizedEmail = "DEJLOF@EXAMPLE.COM",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                SecurityStamp = new Guid().ToString("D")
            };


            var passwordHasher = new PasswordHasher<AppUser>();
            adminUser.PasswordHash = passwordHasher.HashPassword(adminUser, "Dejlof@87654321");



            builder.Entity<IdentityRole>().HasData(roles);
            builder.Entity<AppUser>().HasData(adminUser);

            var adminUserRole = new IdentityUserRole<string>
            {
                RoleId = "1",
                UserId = "1",
            };

        builder.Entity<IdentityUserRole<string>>().HasData(adminUserRole);
        }
    }
}
