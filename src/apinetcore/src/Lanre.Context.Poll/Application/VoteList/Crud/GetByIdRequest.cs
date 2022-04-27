using MediatR;

namespace Lanre.Context.Poll.Application.VoteList.Crud;

public class GetByIdRequest : IRequest<VoteListDto>
{
    public Guid? Id { get; set; }
}
