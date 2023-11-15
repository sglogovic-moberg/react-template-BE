using MediatR;
using ReactAppBackend.Database;

namespace ReactAppBackend.Handler.Posts
{
    public class EditPostsCommand : IRequestHandler<EditPostsCommandRequest, EditPostsCommandResponse>
    {
        private readonly DatabaseContext _context;

        public EditPostsCommand(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<EditPostsCommandResponse> Handle(EditPostsCommandRequest request, CancellationToken cancellationToken)
        {
            var post = _context.Posts
                .Where(x => x.Id == request.Id)
                .FirstOrDefault();

            if (post == null)
            {
                throw new ArgumentException("Post not found");
            }

            post.Title = request.Title;
            post.Description = request.Description;

            await _context.SaveChangesAsync(cancellationToken);

            return new EditPostsCommandResponse
            {
                NewPost = post
            };
        }
    }
    public class EditPostsCommandRequest : IRequest<EditPostsCommandResponse>
    {
        public required int Id { get; set; }

        public required string Title { get; set; }

        public required string Description { get; set; }

    }

    public class EditPostsCommandResponse
    {
        public PostsEntity NewPost { get; set; }
    }
}
