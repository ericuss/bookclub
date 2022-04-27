using MediatR;

namespace Lanre.Context.Poll.Application.VoteList.Crud;

public class GetAllRequest : IRequest<IEnumerable<VoteListDto>>
{
}
