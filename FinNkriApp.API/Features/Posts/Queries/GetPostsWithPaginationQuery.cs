using AutoMapper;
using AutoMapper.QueryableExtensions;
using FinNkriApp.API.Interfaces;
using FinNkriApp.API.Mapping;
using FinNkriApp.API.Models;
using MediatR;

namespace FinNkriApp.API.Features.Posts.Queries
{
    public record GetPostsWithPaginationQuery : IRequest<PaginatedList<PostDto>>
    {
        public int PageNumber { get; init; } = 1;
        public int PageSize { get; init; } = 10;
    }
    public class GetTodoItemsWithPaginationQueryHandler : IRequestHandler<GetPostsWithPaginationQuery, PaginatedList<PostDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetTodoItemsWithPaginationQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PaginatedList<PostDto>> Handle(GetPostsWithPaginationQuery request, CancellationToken cancellationToken)
        {
            return await _context.Posts
                .ProjectTo<PostDto>(_mapper.ConfigurationProvider)
                .PaginatedListAsync(request.PageNumber, request.PageSize);
        }
    }
}
