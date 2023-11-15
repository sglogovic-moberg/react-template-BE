using MediatR;
using Microsoft.AspNetCore.Mvc;
using ReactAppBackend.Configuration;
using ReactAppBackend.Handler.Users;

namespace ReactAppBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [PermissionAuthorize(Models.UserRoleEnum.Admin)]
        public async Task<GetUsersQueryResponse> GetUsersQueryAsync([FromQuery] GetUsersQueryRequest request, CancellationToken cancellationToken)
        {
            return await _mediator.Send(request, cancellationToken);
        }

        [HttpPost("add")]
        [PermissionAuthorize(Models.UserRoleEnum.Admin)]
        public async Task<CreateUsersCommandResponse> CreateUsersCommandAsync(CreateUsersCommandRequest request, CancellationToken cancellationToken)
        {
            return await _mediator.Send(request, cancellationToken);
        }

        [HttpPost("delete")]
        [PermissionAuthorize(Models.UserRoleEnum.Admin)]
        public async Task<DeleteUsersCommandResponse> DeleteUserCommandAsync(DeleteUsersCommandRequest request, CancellationToken cancellationToken)
        {
            return await _mediator.Send(request, cancellationToken);
        }
    }
}