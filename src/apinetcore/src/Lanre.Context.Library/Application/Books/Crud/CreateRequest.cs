using MediatR;

namespace Lanre.Context.Library.Application.Books.Crud;

public class CreateRequest : BookDto, IRequest<Guid>
{

}
