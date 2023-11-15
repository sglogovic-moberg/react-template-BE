using MediatR;
using ReactAppBackend.Database;
using ReactAppBackend.Models;

namespace ReactAppBackend.Handler.Users
{
    public class CreateUsersCommand : IRequestHandler<CreateUsersCommandRequest, CreateUsersCommandResponse>
    {
        private readonly DatabaseContext _context;

        public CreateUsersCommand(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<CreateUsersCommandResponse> Handle(CreateUsersCommandRequest request, CancellationToken cancellationToken)
        {
            var newUsers = new UsersEntity
            {
                Name = request.Name,
                Email = request.Email,
                Password = request.Password,
                UserRole = request.UserRole
            };

            await _context.Users.AddAsync(newUsers);

            await _context.SaveChangesAsync(cancellationToken);

            return new CreateUsersCommandResponse
            {
                NewUsers = newUsers
            };
        }
    }
    public class CreateUsersCommandRequest : IRequest<CreateUsersCommandResponse>
    {
        public required string Name { get; set; }

        public required string Email { get; set; }

        public required string Password { get; set; }

        public required UserRoleEnum UserRole { get; set; }

    }

    public class CreateUsersCommandResponse
    {
        public required UsersEntity NewUsers { get; set; }
    }
}
