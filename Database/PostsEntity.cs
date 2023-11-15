namespace ReactAppBackend.Database
{
    public class PostsEntity : BaseEntity
    {
        public required string Title { get; set; }

        public required string Description { get; set; }

        public required int UserId { get; set; }

        public UsersEntity User { get; set; } = null!;
    }
}