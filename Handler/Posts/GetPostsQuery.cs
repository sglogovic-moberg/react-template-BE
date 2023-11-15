using MediatR;
using ReactAppBackend.Database;
using ReactAppBackend.Helper;
using ReactAppBackend.Helpers;
using System.Threading;

namespace ReactAppBackend.Handler.Posts
{
    public class GetPostsQuery : IRequestHandler<GetPostsQueryRequest, GetPostsQueryResponse>
    {
        private readonly DatabaseContext _context;

        public GetPostsQuery(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<GetPostsQueryResponse> Handle(GetPostsQueryRequest request, CancellationToken cancellationToken)
        {
            var searchText = request.Search?.ToLower();

            var result = await _context.Posts
                .WhereIf(searchText.HasValue(), 
                    x => x.User.Name.ToLower().Contains(searchText!) ||
                    x.Title.ToLower().Contains(searchText!) ||
                    x.Description.ToLower().Contains(searchText!))
                .Select(x => new GetPostsResponseListData
                {
                    Id = x.Id,
                    CreatedTime = x.CreatedTime,
                    Description = x.Description,
                    Title = x.Title,
                    UserId = x.UserId,
                    UserName = x.User.Name
                })
            .ToPagedListAsync(request, cancellationToken);

            return new GetPostsQueryResponse
            {
                Data = result.Items,
                TotalCount = result.TotalCount
            };
        }
    }
    public class GetPostsQueryRequest : SortedListRequest, IRequest<GetPostsQueryResponse>
    {
        public string? Search { get; set; }
    }

    public class GetPostsQueryResponse : IPagedListResponse<GetPostsResponseListData>
    {
        public required List<GetPostsResponseListData> Data { get; set; }

        public int TotalCount { get; set; }
    }

    public class GetPostsResponseListData
    {
        public required int Id { get; set; }
        public required string Title { get; set; }

        public required string Description { get; set; }

        public required int UserId { get; set; }

        public required string UserName { get; set; }

        public DateTime CreatedTime { get; set; }
    }
}
