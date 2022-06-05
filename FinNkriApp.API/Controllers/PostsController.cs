using FinNkriApp.API.Features.Posts.Queries;
using FinNkriApp.API.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FinNkriApp.API.Controllers
{

    public class PostsController : ApiControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<PaginatedList<PostDto>>> GetTodoItemsWithPagination([FromQuery] GetPostsWithPaginationQuery query)
        {
            return await Mediator.Send(query);
        }
    }
}
