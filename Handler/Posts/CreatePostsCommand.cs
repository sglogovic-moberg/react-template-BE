using MediatR;
using ReactAppBackend.Database;

namespace ReactAppBackend.Handler.Posts
{
    public class CreatePostsCommand : IRequestHandler<CreatePostsCommandRequest, CreatePostsCommandResponse>
    {
        private readonly DatabaseContext _context;

        public CreatePostsCommand(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<CreatePostsCommandResponse> Handle(CreatePostsCommandRequest request, CancellationToken cancellationToken)
        {
            var newPost = new PostsEntity
            {
                Description = request.Description,
                Title = request.Title,
                UserId = request.UserId,
            };

            await _context.Posts.AddAsync(newPost);

            await _context.SaveChangesAsync(cancellationToken);

            return new CreatePostsCommandResponse
            {
                NewPost = newPost
            };
        }
    }
    public class CreatePostsCommandRequest : IRequest<CreatePostsCommandResponse>
    {
        public required string Title { get; set; }

        public required string Description { get; set; }

        public required int UserId { get; set; }

    }

    public class CreatePostsCommandResponse
    {
        public PostsEntity NewPost { get; set; }
    }
}
