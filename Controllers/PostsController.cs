using MediatR;
using Microsoft.AspNetCore.Mvc;
using ReactAppBackend.Configuration;
using ReactAppBackend.Database;
using ReactAppBackend.Handler.Posts;
using System.Threading;

namespace ReactAppBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PostsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PostsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<GetPostsQueryResponse> GetPostsQueryAsync([FromQuery] GetPostsQueryRequest request, CancellationToken cancellationToken)
        {
            return await _mediator.Send(request, cancellationToken);
        }


        [HttpPost("add")]
        public async Task<CreatePostsCommandResponse> CreatePostCommandAsync(CreatePostsCommandRequest request, CancellationToken cancellationToken)
        {
            return await _mediator.Send(request, cancellationToken);
        }

        [HttpPost("edit")]
        public async Task<EditPostsCommandResponse> EditPostCommandAsync(EditPostsCommandRequest request, CancellationToken cancellationToken)
        {
            return await _mediator.Send(request, cancellationToken);
        }

        [HttpPost("delete")]
        [PermissionAuthorize(Models.UserRoleEnum.Admin)]
        public async Task<DeletePostsCommandResponse> DeletePostsCommandAsync(DeletePostsCommandRequest request, CancellationToken cancellationToken)
        {
            return await _mediator.Send(request, cancellationToken);
        }
    }
}