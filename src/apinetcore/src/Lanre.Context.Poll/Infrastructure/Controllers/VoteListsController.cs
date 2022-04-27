
using Lanre.Context.Poll.Application.VoteList.Crud;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lanre.Context.Poll.Infrastructure.Controllers;

[Authorize]
[ApiController]
[ApiVersion("1")]
[ApiExplorerSettings(GroupName = "VoteLists")]
[Produces("application/json")]
[Route("api/v{version:apiVersion}/[Controller]")]
public class VoteListsController : ControllerBase
{
    private readonly IMediator mediator;

    public VoteListsController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await this.mediator.Send(new Application.VoteList.Crud.GetAllRequest { });

        return this.Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(Guid? id)
    {
        var result = await this.mediator.Send(new Application.VoteList.Crud.GetByIdRequest { Id = id });

        return this.Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] VoteListDto book)
    {
        var result = await this.mediator.Send(new Application.VoteList.Crud.CreateRequest { Name = book.Name });

        return this.Ok(new { id = result });
    }
}
