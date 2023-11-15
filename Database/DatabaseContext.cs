using Microsoft.EntityFrameworkCore;
using ReactAppBackend.Models;

namespace ReactAppBackend.Database
{
    public class DatabaseContext : DbContext
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UsersEntity>().HasData(
                new UsersEntity
                {
                    Id = 1,
                    Email = "silvio.glogovic@tvz.hr",
                    Name = "Silvio",
                    Password = "password",
                    UserRole = Models.UserRoleEnum.Admin
                },
                new UsersEntity
                {
                    Id = 2,
                    Email = "Test@tvz.hr",
                    Name = "Test User",
                    Password = "password",
                    UserRole = Models.UserRoleEnum.User
                }
            );

            modelBuilder.Entity<PostsEntity>().HasData(
                new PostsEntity
                {
                    Id = 1,
                    Description = "Sample description text 1",
                    Title = "Sample Title 1",
                    UserId = 1,
                },
                new PostsEntity
                {
                    Id = 2,
                    Description = "Sample description text 2",
                    Title = "Sample Title 2",
                    UserId = 1,
                }
            );
        }

        public DatabaseContext(DbContextOptions options) : base(options) { }

        public DbSet<PostsEntity> Posts => Set<PostsEntity>();

        public DbSet<UsersEntity> Users => Set<UsersEntity>();
    }
}
