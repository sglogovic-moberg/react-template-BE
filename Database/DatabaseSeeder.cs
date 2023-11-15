using FizzWare.NBuilder;
using Microsoft.EntityFrameworkCore;

namespace ReactAppBackend.Database
{
    public static class DatabaseSeeder
    {
        public static void SeedData(WebApplication app)
        {
            var scope = app.Services.CreateScope();
            var db = scope.ServiceProvider.GetService<DatabaseContext>();

            var user1 = new UsersEntity
            {
                Id = 1,
                Email = "silvio.glogovic@tvz.hr",
                Name = "Silvio",
                Password = "password",
                UserRole = Models.UserRoleEnum.Admin
            };
            var user2 = new UsersEntity
            {
                Id = 2,
                Email = "Test@tvz.hr",
                Name = "Test User",
                Password = "password",
                UserRole = Models.UserRoleEnum.User
            };

            var posts = FizzWare.NBuilder.Builder<PostsEntity>.CreateListOfSize(100)
                .TheFirst(50).With(x => x.UserId = user1!.Id)
                .TheLast(50).With(x => x.UserId = user2!.Id)
                .Build();

            db.Users.Add(user1);
            db.Users.Add(user2);
            db.Posts.AddRange(posts);

            db.SaveChanges();

        }
    }
}
