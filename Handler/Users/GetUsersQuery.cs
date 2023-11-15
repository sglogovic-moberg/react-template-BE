using MediatR;
using ReactAppBackend.Database;
using ReactAppBackend.Helper;
using ReactAppBackend.Helpers;
using System.Threading;

namespace ReactAppBackend.Handler.Users
{
    public class GetUsersQuery : IRequestHandler<GetUsersQueryRequest, GetUsersQueryResponse>
    {
        private readonly DatabaseContext _context;

        public GetUsersQuery(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<GetUsersQueryResponse> Handle(GetUsersQueryRequest request, CancellationToken cancellationToken)
        {
            var result = await _context.Users
                .WhereIf(request.Name.HasValue(), 
                    x => x.Name.ToLower().Contains(request.Name!.ToLower()))
                .Where(x => x.Id > 0)
                .Select(x => new GetUsersResponseListData
                {
                    Id = x.Id,
                    Email = x.Email,
                    Name = x.Name,
                    CreatedTime = x.CreatedTime,
                })
            .ToPagedListAsync(request, cancellationToken);

            return new GetUsersQueryResponse
            {
                Data = result.Items,
                TotalCount = result.TotalCount
            };
        }
    }
    public class GetUsersQueryRequest : SortedListRequest, IRequest<GetUsersQueryResponse>
    {
        public string? Name { get; set; }
    }

    public class GetUsersQueryResponse : IPagedListResponse<GetUsersResponseListData>
    {
        public required List<GetUsersResponseListData> Data { get; set; }

        public int TotalCount { get; set; }
    }

    public class GetUsersResponseListData
    {
        public required int Id { get; set; }

        public required string Name { get; set; }

        public required string Email { get; set; }

        public DateTime CreatedTime { get; set; }
    }
}
