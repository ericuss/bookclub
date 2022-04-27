using MediatR;

namespace Lanre.Context.Poll.Application.VoteList.Crud;

public class CreateRequest : IRequest<Guid>
{
    public string? Name { get; set; }

    public string? UserId { get; set; }
}
