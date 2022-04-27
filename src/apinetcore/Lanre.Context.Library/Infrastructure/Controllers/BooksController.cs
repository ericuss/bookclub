
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Lanre.Context.Library.Infrastructure.Dtos;
using Microsoft.AspNetCore.Authorization;

namespace Lanre.Context.Library.Infrastructure.Controllers;

[Authorize]
[ApiController]
[ApiVersion("1")]
[ApiExplorerSettings(GroupName = "Books")]
[Produces("application/json")]
[Route("api/v{version:apiVersion}/[Controller]")]
public class BooksController : ControllerBase
{
    private readonly IMediator mediator;

    public BooksController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await this.mediator.Send(new Application.Books.Crud.GetAllRequest { });

        return this.Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(Guid? id)
    {
        var result = await this.mediator.Send(new Application.Books.Crud.GetByIdRequest { Id = id });

        return this.Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] BookDto book)
    {
        var result = await this.mediator.Send(new Application.Books.Crud.CreateRequest { Name = book.Name });

        return this.Ok(new { id = result });
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid? id, [FromBody] BookDto book)
    {
        var result = await this.mediator.Send(new Application.Books.Crud.UpdateRequest { Id = id, Name = book.Name });

        return this.Ok(new { id = result });
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid? id)
    {
        var result = await this.mediator.Send(new Application.Books.Crud.DeleteRequest { Id = id });

        return this.Ok(new { id = result });
    }
}
