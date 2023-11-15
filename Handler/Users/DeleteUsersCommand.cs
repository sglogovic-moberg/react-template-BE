using MediatR;
using ReactAppBackend.Database;

namespace ReactAppBackend.Handler.Users
{
    public class DeleteUsersCommand : IRequestHandler<DeleteUsersCommandRequest, DeleteUsersCommandResponse>
    {
        private readonly DatabaseContext _context;

        public DeleteUsersCommand(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<DeleteUsersCommandResponse> Handle(DeleteUsersCommandRequest request, CancellationToken cancellationToken)
        {
            var user = _context.Users
                .Where(x => x.Id == request.Id)
                .FirstOrDefault();

            if (user == null)
            {
                throw new Exception();
            }

            _context.Remove(user);

            await _context.SaveChangesAsync();

            return new DeleteUsersCommandResponse();
        }
    }
    public class DeleteUsersCommandRequest : IRequest<DeleteUsersCommandResponse>
    {
        public required int Id { get; set; }
    }

    public class DeleteUsersCommandResponse
    {
    }
}
