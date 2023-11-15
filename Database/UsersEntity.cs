using ReactAppBackend.Models;

namespace ReactAppBackend.Database
{
    public class UsersEntity : BaseEntity
    {
        public required string Name { get; set; }

        public required string Email { get; set; }

        public required string Password { get; set; }

        public List<PostsEntity> Posts { get; set; } = new();

        public required UserRoleEnum UserRole { get; set; }
    }
}
