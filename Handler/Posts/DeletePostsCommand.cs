using MediatR;
using ReactAppBackend.Database;

namespace ReactAppBackend.Handler.Posts
{
    public class DeleteUsersCommand : IRequestHandler<DeletePostsCommandRequest, DeletePostsCommandResponse>
    {
        private readonly DatabaseContext _context;

        public DeleteUsersCommand(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<DeletePostsCommandResponse> Handle(DeletePostsCommandRequest request, CancellationToken cancellationToken)
        {
            var post = _context.Posts
                .Where(x => x.Id == request.Id)
                .FirstOrDefault();

            if (post == null)
            {
                throw new Exception();
            }

            _context.Remove(post);

            await _context.SaveChangesAsync();

            return new DeletePostsCommandResponse();
        }
    }
    public class DeletePostsCommandRequest : IRequest<DeletePostsCommandResponse>
    {
        public required int Id { get; set; }
    }

    public class DeletePostsCommandResponse
    {
    }
}
