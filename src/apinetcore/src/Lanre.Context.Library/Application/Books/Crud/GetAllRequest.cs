using MediatR;

namespace Lanre.Context.Library.Application.Books.Crud;

public class GetAllRequest : IRequest<IEnumerable<BookDto>>
{
}
