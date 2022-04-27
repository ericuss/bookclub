using MediatR;

namespace Lanre.Context.Library.Application.Books.Crud;

public class UpdateRequest : BookDto, IRequest<Guid>
{
}
