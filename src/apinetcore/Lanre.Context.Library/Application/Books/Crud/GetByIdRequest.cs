using MediatR;

namespace Lanre.Context.Library.Application.Books.Crud;

public class GetByIdRequest : IRequest<BookDto>
{
    public Guid? Id { get; set; }
}
